using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransistion : MonoBehaviour
{
    public Collider loopingCollider;
    public bool _isLooping = true;
    
    private void Update()
    {
        if(_isLooping == false)
        {
            //loopingCollider.GetComponent<GameObject>().SetActive(false);
            loopingCollider.gameObject.SetActive(false);
        }        
    }
    void CompletedPuzzle()
    {
        _isLooping = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene("Final Scene");
        }
    }
    void OnEnable()
    {
        Puzzle.Complete += CompletedPuzzle;
    }
    void OnDisable()
    {
        Puzzle.Complete -= CompletedPuzzle;
    }

}
