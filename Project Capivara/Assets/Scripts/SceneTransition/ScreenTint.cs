using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTint : MonoBehaviour
{
    [SerializeField] Color unTintedColor;
    [SerializeField] private Color tintedColor;
    private float f;
    public float speed = 0.5F;

    private Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Tint()
    {
        StopAllCoroutines();
        f = 0F;
        StartCoroutine(TintScreen());
    }

    public void UnTint()
    {
        StopAllCoroutines();
        f = 0F;
        StartCoroutine(UnTintScreen());
    }
    
    private IEnumerator TintScreen()
    {
        while (f < 1F)
        {
            f += Time.deltaTime * speed;
            f = Mathf.Clamp(f, 0, 1F);
            Color c = image.color;
            c = Color.Lerp(unTintedColor, tintedColor, f);
            image.color = c;
            
            yield return new WaitForEndOfFrame();
        }
    }
    
    private IEnumerator UnTintScreen()
    {
        while (f < 1F)
        {
            f += Time.deltaTime * speed;
            f = Mathf.Clamp(f, 0, 1F);
            Color c = image.color;
            c = Color.Lerp(tintedColor, unTintedColor, f);
            image.color = c;
            
            yield return new WaitForEndOfFrame();
        }
    }
    
}
