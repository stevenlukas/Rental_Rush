using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ZombieController : MonoBehaviour
{
    AIPath aiPath;
    AIDestinationSetter aiDestinationSetter;
    Patrol patrol;
    Animator animator;
    Transform player;
    Transform startPos;
    GameController gameController;

    enum FacingDir { Up, Down, Left, Right }
    [SerializeField]
    FacingDir facingDir;

    enum EnemyType { Patrol, Chase}
    [SerializeField]
    EnemyType enemyType;

    [SerializeField]
    bool hybrid = false;
    [SerializeField]
    bool enabled = false;

    public float timer = 6;
    public float hazDet = 3;
    float distance;
    float angleToPlayer;

    [SerializeField]
    float fovSize;

    Vector2 playerDir;

    RaycastHit2D hit;

    // Start is called before the first frame update
    void Start()
    {
        aiPath = this.gameObject.GetComponent<AIPath>();
        aiDestinationSetter = this.gameObject.GetComponent<AIDestinationSetter>();
        patrol = this.gameObject.GetComponent<Patrol>();
        animator = this.gameObject.GetComponent<Animator>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        startPos = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        switch(enemyType)
        {
            case EnemyType.Chase:
                patrol.enabled = false;
                aiDestinationSetter.enabled = true;
                if (hybrid)
                {
                    Hybrid();
                }
                break;

            case EnemyType.Patrol:
                aiDestinationSetter.enabled = false;
                patrol.enabled = true;
                if(hybrid)
                {
                    Hybrid();
                }
                break;
        }
        if(aiPath.desiredVelocity.y > 0)
        {
            if(aiPath.desiredVelocity.x > 0 )
            {
                facingDir = FacingDir.Right;
            }
            else if(aiPath.desiredVelocity.x < 0)
            {
                facingDir = FacingDir.Left;
            }
            else
            {
                facingDir = FacingDir.Up;
            }
        }
        else
        {
            if (aiPath.desiredVelocity.x > 0)
            {
                facingDir = FacingDir.Right;
            }
            else if (aiPath.desiredVelocity.x < 0)
            {
                facingDir = FacingDir.Left;
            }
            else
            {
                facingDir = FacingDir.Down;
            }
        }

        switch(facingDir)
        {
            case FacingDir.Down:
                animator.SetFloat("MoveDir", 4);
                break;

            case FacingDir.Up:
                animator.SetFloat("MoveDir", 1);
                break;

            case FacingDir.Left:
                animator.SetFloat("MoveDir", 2);
                break;

            case FacingDir.Right:
                animator.SetFloat("MoveDir", 3);
                break;
        }
    }

    void Hybrid()
    {
        if(!enabled)
        {
            enabled = true;
            switch (enemyType)
            {
                case EnemyType.Chase:
                    StartCoroutine(Timer());
                break;

                case EnemyType.Patrol:
                    //detect if player is seen
                    distance = Vector2.Distance(this.transform.position, player.position);
                    playerDir = player.position - this.transform.position;
                    angleToPlayer = (Vector2.Angle(playerDir, this.transform.forward));

                    if(angleToPlayer >= -fovSize && angleToPlayer <= fovSize && distance <= hazDet)
                    {
                        hit = Physics2D.Raycast(this.transform.position, playerDir);
                        if(hit.transform == player)
                        {
                            enemyType = EnemyType.Chase;
                            enabled = true;
                        }
                    }
                    break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            gameController.ImpactPenalty();
            other.gameObject.GetComponent<PlayerController>().sfx[0].Play();
            this.transform.position = startPos.position;
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(timer);
        enemyType = EnemyType.Patrol;
        enabled = false;
    }
}
