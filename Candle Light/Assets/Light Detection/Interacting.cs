using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacting : MonoBehaviour
{
    public float range;
    public Camera fps;

    GhostAI ghostAI;
    


    // Start is called before the first frame update
    void Start()
    {
        ghostAI = GameObject.FindWithTag("Ghost").GetComponent<GhostAI>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(fps.transform.position, fps.transform.forward, out hit, range))
        {
            if (hit.collider.tag == "PuzzlePiece" && Input.GetButtonDown("Fire1"))
            {
                hit.collider.GetComponent<PuzzlePiece>()._isCollected = true;
                ghostAI._lookForWaypoint = true;
            }
        }
    }




}
