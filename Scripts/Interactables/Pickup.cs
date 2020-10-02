using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    enum PickupType { Screwdriver, Rewinder }
    [SerializeField]
    PickupType pickupType;

    public float rewindTime;
    public float rewindAmount;

    PlayerController player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            player = other.GetComponent<PlayerController>();
            if(player.canPickup)
            {
                player.canPickup = false;

                switch (pickupType)
                {
                    case PickupType.Screwdriver:
                        player.ScrewdriverPickup(rewindTime, rewindAmount);
                        player.sfx[1].Play();
                        break;

                    case PickupType.Rewinder:
                        player.RewinderPickup(rewindTime, rewindAmount);
                        player.sfx[2].Play();
                        break;
                }

                Destroy(this.gameObject);
            }
        }
    }
}
