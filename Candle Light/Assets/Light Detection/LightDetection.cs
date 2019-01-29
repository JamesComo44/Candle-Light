using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetection : MonoBehaviour
{

    public float PlayerDstObject;         //distance from the player to the object
    public GameObject candleAlpha;        //reference to the candle Alpha
    public GameObject candleAdd;          //reference to the candle Add
    public GameObject candleGlow;         //refernec to the candle Glow
    public CandleSize size;               //states of the candles size

    private GameObject target;             //reference to the target objects position
    private Transform[] Objcets;          //array of objects to target

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            target = GameObject.FindWithTag("PuzzlePiece");

        // calculate the distance between the target object and the candles position
        if (target != null)
            PlayerDstObject = Vector3.Distance(target.transform.position, candleAlpha.transform.position);

        ChangeSize();

        if (transform.position.y < -10.0f)
            transform.position = new Vector3(-5.045f, 1.53f, -35.282f);
    }

    //switch case to swap enums when the state changes
    public void ChangeSize()
    {
        switch (size)
        {
            case CandleSize.low:
                LowFlame();
                break;
            case CandleSize.medium:
                MediumFlame();
                break;
            case CandleSize.high:
                HighFlame();
                break;
            case CandleSize.reallyHigh:
                ReallyHighFlame();
                break;
        }
    }

    //function to change the size for the switch case
    public CandleSize FlameSize(CandleSize newSize)
    {
        size = newSize;
        return size;
    }

    //when the player distance to the target object is less than 30 units then change the flame to medium
    public void LowFlame()
    {
        //change flame to low
        candleAlpha.transform.localScale = new Vector3(1,1,1);
        candleAdd.transform.localScale = new Vector3(1, 1, 1);
        candleGlow.transform.localScale = new Vector3(1, 1, 1);

        if (PlayerDstObject < 13)
        {
            FlameSize(CandleSize.medium);
        }

    }

    public void MediumFlame()
    {
        //change flame to medium
        candleAlpha.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        candleAdd.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        candleGlow.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

        //when the player distance to the target object is less than 20 units then change the flame to high
        if (PlayerDstObject < 8)
        {
            FlameSize(CandleSize.high);
        }
        //when the player distance to the target object is greater than 30 units then change the flame to low
        if (PlayerDstObject > 13)
        {
            FlameSize(CandleSize.low);
        }
    }

    public void HighFlame()
    {
        //change flame to high
        candleAlpha.transform.localScale = new Vector3(2, 2, 2);
        candleAdd.transform.localScale = new Vector3(2, 2, 2);
        candleGlow.transform.localScale = new Vector3(2, 2, 2);

        //when the player distance to the target object is less than 10 units then change the flame to really high
        if (PlayerDstObject < 6)
        {
            FlameSize(CandleSize.reallyHigh);
        }
        //when the player distance to the target object is greater than 20 units then change the flame to medium
        if (PlayerDstObject > 12)
        {
            FlameSize(CandleSize.medium);
        }
    }

    public void ReallyHighFlame()
    {
        //change flame to really high
        candleAlpha.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        candleAdd.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        candleGlow.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);

        //when the player distance to the target object is greater than 10 units then change the flame to high
        if (PlayerDstObject > 9)
        {
            FlameSize(CandleSize.high);
        }
    }


    public enum CandleSize { low, medium, high, reallyHigh }

}
