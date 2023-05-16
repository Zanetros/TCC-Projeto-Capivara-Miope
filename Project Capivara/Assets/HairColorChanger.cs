using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HairColorChanger : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Slider haircolor1;
    public Slider haircolor2;
    public Slider haircolor3;

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        spriteRenderer.material.color = new Color(haircolor1.value, haircolor2.value, haircolor3.value);
        
    }
}
