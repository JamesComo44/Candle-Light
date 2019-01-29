using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //public Image[] PieceImages = new Image[numPieceSlots];
    public PuzzlePiece[] playerPuzzle = new PuzzlePiece[numPieceSlots];
    public const int numPieceSlots = 16;
    public void AddPiece(PuzzlePiece PieceToAdd)
    {
        for (int i = 0; i < playerPuzzle.Length; i++)
        {
            if (playerPuzzle[i] == null)
            {
                playerPuzzle[i] = PieceToAdd;
                //PieceImages[i] = PieceToAdd.image;
                //PieceImages[i].enabled = true;
                return;
            }
        }
    }
}
