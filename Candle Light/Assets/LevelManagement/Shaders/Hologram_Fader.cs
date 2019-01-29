using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hologram_Fader : MonoBehaviour
{
    //Renderer rend;
    Material mat;
    public float rimGlow = 3.5f;
    float maxRimGlow = 7;
    float minRimGlow = .5f;
    float glowIntensity = .01f;
    public int materialSelect = 1;
    int time_controller = 1;
    // Start is called before the first frame update
    void Start()
    {
        //rend = GetComponent<Renderer>();
        mat = GetComponent<Renderer>().materials[materialSelect];
    }
    void Holofade()
    {
        mat.SetFloat("_Outline", rimGlow);
        switch (time_controller)
        {
            case 1:
                rimGlow -= glowIntensity;
                mat.SetFloat("_Outline", rimGlow);
                if (rimGlow <= minRimGlow)
                    time_controller = 2;
                break;
            case 2:
                rimGlow += glowIntensity;
                mat.SetFloat("_Outline", rimGlow);
                if (rimGlow >= maxRimGlow)
                    time_controller = 1;
                break;
            case 3:
                rimGlow += glowIntensity;
                mat.SetFloat("_Outline", rimGlow);
                if (rimGlow >= maxRimGlow)
                    time_controller = 1;
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Holofade();
        //Debug.Log("The case is " + time_controller);
    }
}
