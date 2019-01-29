using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalLooper : MonoBehaviour
{
    public Transform player;
    public Transform stairway;

    private bool playerOnStairs = false;

    private void Update()
    {
        if (playerOnStairs)
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            if(dotProduct < 0.0f)
            {
                float rotationDiff = -Quaternion.Angle(transform.rotation, stairway.rotation);
                rotationDiff += 180;
                player.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                player.position = stairway.position + positionOffset;
                playerOnStairs = false;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {        
        if(other.tag == "Player")
        {
            playerOnStairs = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerOnStairs = false;
        } 
    }
}
