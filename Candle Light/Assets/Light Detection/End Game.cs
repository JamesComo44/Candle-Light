using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //delay ending the game for animation in the scene
        Invoke("EndingGame", 45);
    }

    void EndingGame()
    {
        //exits the player out of the game
        Application.Quit();
    }
}
