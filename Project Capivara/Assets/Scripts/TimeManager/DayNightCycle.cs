using UnityEngine;
using UnityEngine.Rendering;
using System;
using System.Collections.Generic;

public class DayNightCycle : MonoBehaviour
{
    public static Action OnHourChanged;

    public Volume ppv;

    public bool areLightsOn;
    public GameObject[] lights;
 
    public void Start()
    {
        ppv = GetComponent<Volume>();
        areLightsOn = false;
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].SetActive(false);
        }
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

        if (areLightsOn == false)
        {
            if (TimeManager.Hour >= 19 && TimeManager.Hour < 20) 
            {
                for (int i = 0; i < lights.Length; i++)
                {
                    lights[i].SetActive(true); 
                }
                areLightsOn = true;
            }
        }

        if (TimeManager.Hour >= 6 && TimeManager.Hour < 7) 
        {
            ppv.weight = 1 - (float)TimeManager.Minute / 60; 
            if (areLightsOn == true) 
            {
                if (TimeManager.Hour >= 6 && TimeManager.Hour < 7) 
                {
                    for (int i = 0; i < lights.Length; i++)
                    {
                        lights[i].SetActive(false);
                    }
                    areLightsOn = false;
                }
            }
        }
    }
}
