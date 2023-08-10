using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawnPointsSceneController : MonoBehaviour
{
    public int sceneBuildIndex = -1;
    public Transform[] spawnPointsInScene;
    public Transform[] waypointsInScene;

    void Start()
    {
        GameManager.instance.npcSpawnManager.npcSpawnPointsInScene = spawnPointsInScene;
        GameManager.instance.npcSpawnManager.npcWaypointsInScene = waypointsInScene;
    }
}