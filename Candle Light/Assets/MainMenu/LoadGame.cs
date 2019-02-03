using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadGame : MonoBehaviour
{
    public GameObject fireAlpha;        //reference to the alpha particle effect
    public GameObject fireAdd;          //reference to the add particle effect
    public GameObject fireGlow;         //reference to the Glow particle effect
    public float timer;                 //public value to change in the inspector for how long loading the scene is delayed
    private Jutebox juktebox;

    void Start()
    {
        
    }

    //when the button is clicked the flame's scale will get bigger
    public void OnClick()
    {
        fireAlpha.gameObject.transform.localScale = new Vector3(4, 4, 4);
        fireAdd.gameObject.transform.localScale = new Vector3(4, 4, 4);
        fireGlow.gameObject.transform.localScale = new Vector3(4, 4, 4);
        //delays calling the function for the duration of the timer
        Invoke("Delay", timer);
    }

    //Loads the game scene
    void Delay()
    {
        SceneManager.LoadScene("Level 1");
    }
}
