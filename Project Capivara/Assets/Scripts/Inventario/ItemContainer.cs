using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class ItemSlot
{
    public Item item;
    public int count;

    public void Copy(ItemSlot slot)
    {
        item = slot.item;
        count = slot.count;
    }   

    public void Clear()
    {
        item = null;
        count = 0;
    }

    public void Set(Item item, int count)
    {
        this.item = item;
        this.count = count;
    }
}


[CreateAssetMenu(menuName = "Data/Item Container")]
public class ItemContainer : ScriptableObject
{
    public List<ItemSlot> slots;

    //adiciona itens no inventario
    public void Add(Item item , int count = 1)
    {
        //adiciona itens stackaveis no invetario 
        if (item.stackable == true)
        {
            ItemSlot itemSlot = slots.Find(x => x.item == item);
            if (itemSlot != null)
            {
                itemSlot.count += count;
            }
            //se nao tiver o item no inventario ainda, adiciona ele
            else
            {
                itemSlot = slots.Find(x => x.item == null);
                if (itemSlot != null)
                {
                    itemSlot.item = item;
                    itemSlot.count = count;
                }
            }
        }
        else
        {
            //adicionar item nao stackaveis pro container
            ItemSlot itemSlot = slots.Find(x => x.item = null);
            if (itemSlot == null)
            {
                itemSlot.item = item;
                itemSlot.count = count;
            }
        }
    }

    public void Remove(Item itemRemove, int count = 1)
    {
        if (itemRemove.stackable)
        {
            ItemSlot itemSlot = slots.Find(x => x.item == itemRemove);
            if (itemSlot == null) { return; }
            
            itemSlot.count -= count;
            if (itemSlot.count <= 0)
            {
                itemSlot.Clear();
            }
        }

        else
        {
            while (count > 0)
            {
                count -= 1;

                ItemSlot itemSlot = slots.Find(x => x.item == itemRemove);
                if (itemSlot == null) { break; }
                itemSlot.Clear();
            }
        }
    }
}
