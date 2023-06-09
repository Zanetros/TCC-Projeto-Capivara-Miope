using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ResourceNode : ToolHit
{
    [SerializeField] GameObject pickUpDrop;
    [SerializeField] float spread = 0.7f;

    [SerializeField] Item item;
    [SerializeField] int itemCountInOneDrop;
    [SerializeField] int dropCount = 5;
    [SerializeField] ResourceNodeType nodeType;
    
    public override void Hit()
    {
        while (dropCount > 0)
        {
            dropCount -= 1;

            Vector3 position = transform.position;
            position.x += spread * UnityEngine.Random.value - spread / 2;
            position.y += spread * UnityEngine.Random.value - spread / 2;
            //GameObject go = Instantiate(pickUpDrop);

            DropedItemSpawner.instance.SpawnItem(position, item, itemCountInOneDrop);
        }
        
        Destroy(gameObject);
    }

    public override bool CanHit(List<ResourceNodeType> canBeHit)
    {
        return canBeHit.Contains(nodeType);
    }
}
