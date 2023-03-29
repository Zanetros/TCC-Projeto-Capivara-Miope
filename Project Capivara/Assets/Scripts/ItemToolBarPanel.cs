using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToolBarPanel : ItemPanel
{
    [SerializeField] ToolBarController toolBarController;

    public void Start()
    {
        Initialize();
        toolBarController.onChange += Highlight;
        Highlight(0);
    }

    public override void OnClick(int id)
    {
        toolBarController.Set(id);
        Highlight(id);
    }

    int currentSelectedTool;

    public void Highlight(int id)
    {
        buttons[currentSelectedTool].Highlight(false);
        currentSelectedTool = id;
        buttons[currentSelectedTool].Highlight(true);      
    }
}
