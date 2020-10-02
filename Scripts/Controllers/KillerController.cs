using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class KillerController : MonoBehaviour
{
    GameController gameController;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            //checks to see if the player has a rewound movie
            if (gameController.tapeRewound)
            {
                //The tape can be returned
                gameController.Win();
            }
        }
    }
}
