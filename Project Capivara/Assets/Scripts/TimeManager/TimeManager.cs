using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering;

public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;
    public static Action OnDayChanged;

    public static int Minute {get;private set; }
    public static int Hour {get;private set; }
    public static int Day { get; private set; }

    const float phaseLenght = 0.02f;

    [SerializeField] private float minuteToRealTime = 0.3f;
    private float timer;
    public Volume ppv;

    public List<TimeAgent> agents;

    private void Awake()
    {
        agents = new List<TimeAgent>();
    }

    public void Subscribe(TimeAgent timeAgent)
    {
        agents.Add(timeAgent);
    }

    public void Unsubscribe(TimeAgent timeAgent)
    {
        agents.Remove(timeAgent);
    }

    void Start()
    {
        Minute = 0;
        Hour = 10;
        Day = 1;
        timer = minuteToRealTime;
    }

    private void FixedUpdate()
    {
        CalculateTime();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        TimeManagement();

        TimeAgents();
    }

    public void TimeManagement()
    {
        if (Hour > 24)
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

    public void CalculateTime()
    {
        ControlPPV();
    }

    public void ControlPPV()
    {
        if (Hour >= 18 && Hour < 19)
        {
            ppv.weight = (float)Minute / 60;
        }

        if (Hour >= 6 && Hour < 7)
        {
            ppv.weight = 1 - (float)Minute / 60;
        }
    }

    int oldPhase = 0;
    private void TimeAgents()
    {
        int currentPhase = (int)(timer / phaseLenght);

        if (oldPhase != currentPhase)
        {
            oldPhase = currentPhase;
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i].Invoke();
            }
        }
    }
}
