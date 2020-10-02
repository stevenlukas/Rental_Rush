using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Player Stats
    public float health;
    public float moveSpeed;
    public float sprintMultiplier;

    //Physics Variables
    public bool canPickup = true;
    bool progressBar = false;

    [SerializeField]
    Slider slider;

    Vector2 movement;

    [SerializeField]
    Rigidbody2D playerRB;

    GameController gameController;
    [SerializeField]
    LevelController levelController;

    public AudioSource[] sfx;

    //Animation Variables
    enum FacingDir { Left, Right, Up, Down }
    FacingDir facingDir;
    public Animator animator;

    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        if(!levelController.paused && gameController.gameStart)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);

            if (Input.GetButtonDown("Horizontal"))
            {
                if (Input.GetButtonDown("Vertical"))
                {
                    if (movement.y > 0)
                    {
                        facingDir = FacingDir.Up;
                    }
                    else
                    {
                        facingDir = FacingDir.Down;
                    }
                }
                else
                {
                    if (movement.x > 0)
                    {
                        facingDir = FacingDir.Right;
                    }
                    else
                    {
                        facingDir = FacingDir.Left;
                    }
                }
            }
            else
            {
                if (Input.GetButtonDown("Vertical"))
                {
                    if (movement.y > 0)
                    {
                        facingDir = FacingDir.Up;
                    }
                    else
                    {
                        facingDir = FacingDir.Down;
                    }
                }
            }
        }

        switch (facingDir)
        {
            case FacingDir.Up:
                animator.SetFloat("IdleType", 3);
                break;

            case FacingDir.Down:
                animator.SetFloat("IdleType", 0);
                break;

            case FacingDir.Left:
                animator.SetFloat("IdleType", 1);
                break;

            case FacingDir.Right:
                animator.SetFloat("IdleType", 2);
                break;
        }

        if(progressBar)
        {
            slider.value += Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        playerRB.MovePosition(playerRB.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void ScrewdriverPickup(float rewindTime, float rewindAmount)
    {
        StartCoroutine(Screwdriver(rewindTime, rewindAmount));
    }

    public void RewinderPickup(float rewindTime, float rewindAmount)
    {
        StartCoroutine(Rewinder(rewindTime, rewindAmount));
    }

    IEnumerator Screwdriver(float rewindTime, float rewindAmount)
    {
        moveSpeed = moveSpeed / 2;
        //play progress bar
        slider.maxValue = rewindTime + Time.deltaTime;
        progressBar = true;
        yield return new WaitForSeconds(rewindTime);
        moveSpeed = moveSpeed * 2;
        gameController.tapeProgress += rewindAmount;
        progressBar = false;
        sfx[1].Stop();
        canPickup = true;
        slider.value = 0;
    }

    IEnumerator Rewinder(float rewindTime, float rewindAmount)
    {
        //play progress bar
        slider.maxValue = rewindTime + Time.deltaTime;
        progressBar = true;

        yield return new WaitForSeconds(rewindTime);
        gameController.tapeProgress += rewindAmount;
        progressBar = false;
        sfx[2].Stop();
        canPickup = true;
        slider.value = 0;
    }
}
