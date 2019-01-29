using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour
{
    public bool _isCollected;
    //public Image image;

    //_________________EVENTS__________________
    //collection delegate
    public delegate void PuzzleEventHandler();
    public static event PuzzleEventHandler Collect;
    //_________________EVENTS__________________

    void Update()
    {
        if(_isCollected == true){
            Destroy(gameObject);
            OnCollect();
        }
    }
    /// <summary>
	/// Publishes the Collected event. Subscriber: Puzzle.cs
	/// 
	/// Alerts any subscribers to the value change
	/// </summary>
    void OnCollect()
    {
        if (Collect != null)
        {
            Collect();
        }
    }
}

