using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{

    public int waterFreq;
    // Start is called before the first frame update
    void Start()
    {
        waterFreq = 2;
        string year = "yyyy";
        string time = System.DateTime.UtcNow.ToLocalTime().ToString("HH:mm | dd.MM." + year);
        Debug.Log("Current time: " + time);
        print(time);
    }

    // Update is called once per frame
    void Update()
    {
        //string time = System.DateTime.UtcNow.ToLocalTime().ToString("HH:mm:ss");
        //print(time);
    }
}
