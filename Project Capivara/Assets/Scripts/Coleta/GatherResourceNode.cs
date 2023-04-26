using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceNodeType
{
    Undefined,
    Tree,
    Crop
}

[CreateAssetMenu(menuName = "Data/Tool action/Gather Resource Node")]
public class GatherResourceNode : ToolActions
{
    [SerializeField] float sizeofInteractableArea = 1f;
    [SerializeField] List<ResourceNodeType> canHitNodesOfType;  

    public override bool OnAply(Vector2 worldPoint)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPoint, sizeofInteractableArea);

        foreach (Collider2D c in colliders)
        {
            ToolHit hit = c.GetComponent<ToolHit>();
            if (hit != null)
            {
                if (hit.CanHit(canHitNodesOfType) == true)
                {
                    hit.Hit();
                    return true;
                }              
            }
        }
        return false;
    }
}
