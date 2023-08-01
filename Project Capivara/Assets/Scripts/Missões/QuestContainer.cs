using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Quest/Quest Container")]
public class QuestContainer : ScriptableObject
{
    public List<QuestStage> stages;
    public string questName;
    [TextArea] public string questDescription;
    //Colocar, posteriormente, uma variável para diferenciar uma quest da outra. A priori, é um id
    public int questId = 1;
    public int actualStage = 0;
    public bool compleated = false;
    public bool active = false;
    public CraftingRecipe[] rewardRecipes = null;
    public int coinReward = 0;
}
