using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeCuttable : ToolHit
{
    public override void Hit()
    {
        Destroy(gameObject);
    }
}
