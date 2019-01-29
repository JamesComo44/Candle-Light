using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public GameObject[] _puzzlePieces;
    public Inventory playerInventory;
    public int _currentPiece;

    //_________________EVENTS__________________
    //puzzle completion delegate
    public delegate void PuzzleCompletionEventHandler();
    public static event PuzzleCompletionEventHandler Complete;
    //_________________EVENTS__________________

    void Start()
    {
        _puzzlePieces = GameObject.FindGameObjectsWithTag("PuzzlePiece");
        foreach (var piece in _puzzlePieces){
            piece.SetActive(false);
        } 
        _puzzlePieces[0].SetActive(true);
        _currentPiece = 0;
    }

    /// <summary>
	/// Raises the Collected event. Publisher: PuzzlePieces.cs
	/// 
	/// After a piece is collected, it is added to the player inventory and the next piece in the array is enabled.
    /// If at the end of the array, alert ghost to move to next level.
	/// </summary>
    void CollectedPiece()
    {
        if(_currentPiece != _puzzlePieces.Length - 1)
        {
            _currentPiece++;
            _puzzlePieces[_currentPiece].SetActive(true);
            playerInventory.AddPiece(_puzzlePieces[_currentPiece].GetComponent<PuzzlePiece>());
        }
        //unlock door
        else
        {
            Complete();
        }
    }

    void OnEnable()
    {
        PuzzlePiece.Collect += CollectedPiece;
    }
    void OnDisable()
    {
        PuzzlePiece.Collect -= CollectedPiece;
    }

}
