using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanel : MonoBehaviour
{
    public ItemContainer inventory;
    public List<InventoryButton> buttons;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        SetIndex();
        Show();
    }

    public void OnEnable()
    {
        Show();
    }

    private void SetIndex()
    {
        for (int i = 0; i < inventory.slots.Count && i < buttons.Count; i++)
        {
            buttons[i].SetIndex(i);
        }
    }

    public virtual void Show()
    {
        for (int i = 0; i < inventory.slots.Count && i < buttons.Count; i++)
        {
            if (inventory.slots[i].item == null)
            {
                buttons[i].Clean();
            }

            else
            {
                buttons[i].Set(inventory.slots[i]);
            }
        }
    }

    public virtual void OnClick(int id)
    {

    }
}
