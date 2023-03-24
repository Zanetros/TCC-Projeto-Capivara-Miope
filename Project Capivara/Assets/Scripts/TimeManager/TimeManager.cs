using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;
    public static Action OnDayChanged;

    public static int Minute {get;private set; }
    public static int Hour {get;private set; }
    public static int Day { get; private set; }

    [SerializeField] private float minuteToRealTime = 0.3f;
    private float timer;
    
    void Start()
    {
        Minute = 0;
        Hour = 6;
        Day = 1;
        timer = minuteToRealTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        
        if(Hour > 24)
        {
            Minute = 0;
            Hour = 0;
            Day++;
            OnDayChanged?.Invoke();
        }

        else if (timer <= 0)
        {
            Minute++;
            OnMinuteChanged?.Invoke();
            if (Minute >= 60)
            {
                Hour++;
                Minute = 0;
                OnHourChanged?.Invoke();
            }

            timer = minuteToRealTime;
        }
    }
}
