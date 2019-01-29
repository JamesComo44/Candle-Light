using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selected_Object : MonoBehaviour
{
    public Material mat2;
    Shader shader1;
    Shader shader2;
    Renderer rend;
    void Start()
    {
        //mat = GetComponent<Renderer>().sharedMaterial;
        rend = GetComponent<Renderer>();
        
    }
    void matswap()
    {
        RaycastHit raycastHit;// instantiate a variable called raycast hit
        Ray forwardRay = new Ray(transform.position, transform.forward); //the direction of the raycast

        //Physics.Raycast(transform.position, Vector3.forward, 20);
        if (Physics.Raycast(forwardRay, out raycastHit))
        {
            //Debug.Log(raycastHit.collider.gameObject.name.ToString());
            // raycastHit.collider.gameObject.GetComponentsInChildren<Materials>().shader = shader2;

            // An ArrayList to store all the object's materials.
            ArrayList mats = new ArrayList();

            // Find and store all the renderers attached to the children of our raycastHit target.
            Renderer[] renderers = raycastHit.collider.GetComponentsInChildren<Renderer>();
            Renderer[] rendarr = raycastHit.collider.GetComponentsInChildren<Renderer>();
            // Loop though each renderer and add the material to an ArrayList.
            foreach (Renderer renderer in renderers)
            {
                mats.Add(renderer.material);
            }
            
            if (raycastHit.collider.gameObject.tag.Equals("PuzzlePiece") && Input.GetMouseButtonDown(0))
            {
                    Destroy(raycastHit.collider.gameObject);
               
            }
            else if (raycastHit.collider.gameObject.tag.Equals("PuzzlePiece"))
              {
                
                
                for (int i = 0; i < rendarr.Length; i++)
                {
                    rendarr[i].material = mat2;
                  
                }
              }
            
        }
    }
    void FixedUpdate()
    {
        matswap();
    }
}

