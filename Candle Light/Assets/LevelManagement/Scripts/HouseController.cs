using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseController : MonoBehaviour
{
    public static HouseController myHouseController = null;


    void Awake()
    {
        if(myHouseController == null)
        {
            myHouseController = this;
        }
        else {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }       
}
