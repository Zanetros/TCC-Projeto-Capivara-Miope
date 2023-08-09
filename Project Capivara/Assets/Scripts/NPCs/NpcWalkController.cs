using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcWalkController : MonoBehaviour
{
    [Header("Base Stats")]
    #region
    public float waitTime;
    private float actualWaitTime;
    public bool onSwitch;
    public enum npcStates
    {
        walking,
        waiting,
        swithing
    }
    public npcStates myState;
    #endregion
    
    public Transform[] waypoints;
    public float distanceToStartWait;
    private int waypointCount = 0;
    private NavMeshAgent agent;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = false;
        onSwitch = false;
        myState = npcStates.waiting;
        actualWaitTime = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        switch (myState)
        {
            case npcStates.waiting:
                Waiting();
                break;
            case npcStates.walking:
                Walking();
                break;
            case npcStates.swithing:
                SwithingScene();
                break;
        }
    }

    void Walking()
    {
        if (VerifyTime())
        {
            return;
        }
        if (Vector2.Distance(gameObject.transform.position, waypoints[waypointCount].position) <= distanceToStartWait)
        {
            agent.isStopped = false;
            actualWaitTime = waitTime;
            myState = npcStates.waiting;
        }
    }

    private void GetNewWaypoint()
    {
        if (waypointCount + 1 <= waypoints.Length-1)
        {
            waypointCount++;
        }
        else
        {
            waypointCount = 0;
        }
        agent.SetDestination(waypoints[waypointCount].position);
    }
    
    void Waiting()
    {
        if (VerifyTime())
        {
            return;
        }
        if (actualWaitTime > 0)
        {
            actualWaitTime -= Time.deltaTime;
            if (actualWaitTime <= 0)
            {
                myState = npcStates.walking;
                agent.isStopped = false;
                GetNewWaypoint();
            }
        }
    }

    void SwithingScene()
    {
        
    }

    bool VerifyTime()
    {
        /*agent.isStopped = false;
        onSwitch = true;
        myState = npcStates.swithing;*/
        return false;
    }
}
