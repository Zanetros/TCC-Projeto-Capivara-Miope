using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolActions : ScriptableObject
{      
    public virtual bool OnAply(Vector2 worldPoint)
    {
        Debug.LogWarning("is no working");
        return true;
    }
}
