using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NpcSpawnManager : MonoBehaviour
{
    public NpcContainer[] npcsInScenes;
    public Transform[] npcSpawnPoints;
    public List<Npc> npcsToMove;
    public List<NpcContainer> containersToDeleteFrom;
    private int c;

    public GameManager gameManager;
    
    void Start()
    {
        gameManager = GameManager.instance;
        gameManager.npcSpawnManager = this;
        SpawnNpcs();
    }

    void SpawnNpcs()
    {
        
    }

    void Update()
    {
        VerifyNpcTime();
    }
    

    void VerifyNpcTime()
    {
        c = 0;
        foreach (NpcContainer npcC in npcsInScenes)
        {
            foreach (Npc npc in npcC.npcsFromScene)
            {
                foreach (NpcTimeAndLocation npcTimeAndLocation in npc.npcTimeAndLocations)
                {
                    if (gameManager.timeManager.GetHour().Equals(npcTimeAndLocation.hour))
                    {
                        if (gameManager.timeManager.GetMinute().Equals(npcTimeAndLocation.minute))
                        {
                            //Tirando o NPC da cena em que estava, caso precise estar em outra cena no horário atual
                            //npc.GameObject().SetActive(false);
                            if (!npcsToMove.Contains(npc))
                            {
                                npcsToMove.Add(npc);
                                containersToDeleteFrom.Add(npcC);
                                break;   
                            }
                        }
                    }       
                }
            }
        }
        if (npcsToMove != null)
        {
            foreach (Npc npcM in npcsToMove)
            {
                MoveNpcToAnotherScene(npcM);
                c++;
            }
            npcsToMove.Clear();
        }
    }

    void MoveNpcToAnotherScene(Npc npc)
    {
        //Encontrando a nova cena em que o NPC será alocado
        foreach (NpcContainer npcsN in npcsInScenes)
        {
            foreach (NpcTimeAndLocation npcTimeAndLocation in npc.npcTimeAndLocations)
            {
                if (gameManager.timeManager.GetHour().Equals(npcTimeAndLocation.hour) && (gameManager.timeManager.GetMinute().Equals(npcTimeAndLocation.minute) &&
                    npcsN.sceneBuildIndex.Equals(npcTimeAndLocation.sceneToBeIn)) && !npcsN.npcsFromScene.Contains(npc))
                {
                    npcsN.npcsFromScene.Add(npc);
                    print("Npc Movido para outra cena!");
                    RemoveNpcFromLastScene(npc, containersToDeleteFrom[c]);
                    return;
                }      
            }
        }
        
    }

    void RemoveNpcFromLastScene(Npc n, NpcContainer nC)
    {
        nC.npcsFromScene.Remove(n);
    }

    void SpawnNpc(Npc npc)
    {
        
    }

}
