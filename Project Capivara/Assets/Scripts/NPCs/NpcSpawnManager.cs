using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NpcSpawnManager : MonoBehaviour
{
    public GameObject npcPrefab;
    public NpcContainer[] npcsInScenes;
    public Transform[] npcSpawnPointsInScene;
    public Transform[] npcWaypointsInScene;

    [Header("Variables used During EXEC Time")]
    #region
    public List<Npc> npcsToMove;
    public List<Npc> npcsToSpawn;
    public List<GameObject> npcsToDelete;
    public List<NpcContainer> containersToDeleteFrom;
    private int c;
    private int actualSceneForNpc;
    private GameObject go;
    public List<GameObject> npcsInActualScene;
    public List<Npc> npcsNPCsInActualScene;
    #endregion

    public GameManager gameManager;
    
    void Start()
    {
        gameManager = GameManager.instance;
        gameManager.npcSpawnManager = this;
        SpawnStartNpcs();
    }

    public void SpawnStartNpcs()
    {
        npcsInActualScene.Clear();
        npcsNPCsInActualScene.Clear();
        foreach (NpcContainer npcC in npcsInScenes)
        {
            foreach (Npc npc in npcC.npcsFromScene)
            {
                actualSceneForNpc = -1;
                foreach (NpcTimeAndLocation npcTimeAndLocation in npc.npcTimeAndLocations)
                {
                    if (npcTimeAndLocation.hour <= gameManager.timeManager.GetHour())
                    {
                        if (npcTimeAndLocation.minute <= gameManager.timeManager.GetMinute())
                        {
                            actualSceneForNpc = npcTimeAndLocation.sceneToBeIn;
                        }
                    }
                }
                if (actualSceneForNpc.Equals(SceneManager.GetActiveScene().buildIndex) && !npcsNPCsInActualScene.Contains(npc))
                {
                    //Caso o Npc deva estar na cena atual no horário atual, Spawna o NPC
                    SpawnNamedNpc(npc,  npcSpawnPointsInScene[Random.Range(0, npcSpawnPointsInScene.Length -1)],
                        npcWaypointsInScene);
                    break;
                }
            }
        }
    }

    void SpawnNamedNpc(Npc npc, Transform spawnPoint, Transform[] waypoints)
    {
       go = Instantiate(npcPrefab, spawnPoint);
       go.transform.parent = null;
       go.GetComponent<NpcWalkController>().waypoints = waypoints; 
       go.GetComponent<NpcWalkController>().myPersonality = npc;
       go.GetComponentInChildren<SpriteRenderer>().sprite = npc.npcSprite;
       npcsInActualScene.Add(go);
       npcsNPCsInActualScene.Add(npc);
    }

    void Update()
    {
        VerifyNpcTime();
        VerifyNpcSpawn();
    }
    
    void VerifyNpcTime()
    {
        npcsToMove.Clear();
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
                            foreach (GameObject npcD in npcsInActualScene)
                            {
                                if (npcD.GetComponent<NpcWalkController>().myPersonality.dialogueContainer.Equals(npc.dialogueContainer))
                                {
                                    if (!npcsToDelete.Contains(npcD))
                                    {
                                        npcsToDelete.Add(npcD);
                                    }
                                }
                            }
                            if (!npcsToMove.Contains(npc))
                            {
                                npcsToMove.Add(npc);
                                if (!containersToDeleteFrom.Contains(npcC))
                                {
                                    containersToDeleteFrom.Add(npcC);
                                }
                                break;   
                            }
                        }
                    }       
                }
            }
        }
        if (npcsToDelete != null)
        {
            foreach (GameObject g in npcsToDelete)
            {
                npcsInActualScene.Remove(g);
                Destroy(g);
            }
            npcsToDelete.Clear();
        }
        if (npcsToMove != null)
        {
            foreach (Npc npcM in npcsToMove)
            {
                MoveNpcToAnotherScene(npcM);
                c++;
            }
        }
    }

    void VerifyNpcSpawn()
    {
        npcsToSpawn.Clear();
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
                            if (npcTimeAndLocation.sceneToBeIn.Equals(SceneManager.GetActiveScene().buildIndex))
                            {
                                if (!npcsToSpawn.Contains(npc))
                                {
                                    npcsToSpawn.Add(npc);
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }
        foreach (Npc npcTS in npcsToSpawn)
        {
            SpawnNamedNpc(npcTS, npcSpawnPointsInScene[Random.Range(0, npcSpawnPointsInScene.Length -1)],
                    npcWaypointsInScene);
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
        containersToDeleteFrom.Clear();
    }

    void RemoveNpcFromLastScene(Npc n, NpcContainer nC)
    {
        nC.npcsFromScene.Remove(n);
    }

}