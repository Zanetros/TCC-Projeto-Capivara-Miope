using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.Rendering.Universal;
using System;

public class DayTimeController : MonoBehaviour
{
    [SerializeField] Color nightLightColor;
    const float secondsInDay = 86400f;
    const float phaseLenght = 600f;
    [SerializeField] Color dayLight = Color.white;

    [SerializeField] AnimationCurve nightTimeCurve;

    float time;
    [SerializeField] float timeScale = 60f;
    [SerializeField] float starAtTime = 26800f;

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Light2D globalLight;
    private int days;

    public List<TimeAgent> agents;

    private void Awake()
    {
        agents = new List<TimeAgent>();
    }

    public void Start()
    {
        time = starAtTime;
    }

    public void Subscribe(TimeAgent timeAgent)
    {

    }

    public void Unsubscribe(TimeAgent timeAgent)
    {
        agents.Remove(timeAgent);
    }

    float Hours
    {
        get { return time / 3600f; }
    }

    float Minutes
    {
        get { return time % 3600f / 60f; }
    }

    private void Update()
    {
        time += Time.deltaTime * timeScale;
        
        TimeValueCalculator();
        DayLight();
        TimeAgents();

        if (time > secondsInDay)
        {
            NextDay();
        }
    }

    int oldPhase = 0;
    private void TimeAgents()
    {
        int currentPhase = (int)(time / phaseLenght);
        
        if (oldPhase != currentPhase)
        {
            oldPhase = currentPhase;
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i].Invoke();
            }
        }       
    }

    private void DayLight()
    {
        float v = nightTimeCurve.Evaluate(Hours);
        Color c = Color.Lerp(dayLight, nightLightColor, v);
        globalLight.color = c;
    }

    private void TimeValueCalculator()
    {
        int hh = (int)Hours;
        int mm = (int)Minutes;
        text.text = Hours.ToString("00") + ":" + mm.ToString("00");
    }

    private void NextDay()
    {
        time = 0;
        days += 1;
    }
}
