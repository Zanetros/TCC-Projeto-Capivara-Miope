using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToolBarPanel : ItemPanel
{
    [SerializeField] ToolBarController toolBarController;

    public void Start()
    {
        Initialize();
        print(toolBarController.onChange);
        toolBarController.onChange += Highlight;
        print(toolBarController.onChange);
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
