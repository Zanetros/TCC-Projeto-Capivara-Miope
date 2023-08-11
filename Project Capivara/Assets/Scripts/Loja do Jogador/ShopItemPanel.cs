using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemPanel : MonoBehaviour
{
    public ItemContainer inventory;
    [SerializeField] private List<PlayerShopButton> buttons;

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

    public void SetIndex()
    {
        for (int i = 0; i < inventory.slots.Count && i < buttons.Count; i++)
        {
            buttons[i].SetIndex(i);
        }
    }

    private void LateUpdate()
    {
        if (inventory.isDirty)
        {
            Show();
            inventory.isDirty = false;
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
                buttons[i].Set(inventory.slots[i], i);
            }
        }
    }

    public virtual void OnClick(int id)
    {

    }
}
