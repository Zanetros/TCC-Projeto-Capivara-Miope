using UnityEngine;
using UnityEngine.Rendering;
using System;
using System.Collections.Generic;

public class DayNightCycle : MonoBehaviour
{
    public Volume ppv;

 
    public void Start()
    {
        ppv = GetComponent<Volume>();              
    }

    public void FixedUpdate()
    {
        CalculateTime();
    }

    public void CalculateTime()
    {
        ControlPPV();
    }

    public void ControlPPV()
    {
        if (TimeManager.Hour >= 18 && TimeManager.Hour < 19)
        {
            ppv.weight = (float)TimeManager.Minute / 60;
        }

        if (TimeManager.Hour >= 6 && TimeManager.Hour < 7) 
        {
            ppv.weight = 1 - (float)TimeManager.Minute / 60;             
        }
    }
}
