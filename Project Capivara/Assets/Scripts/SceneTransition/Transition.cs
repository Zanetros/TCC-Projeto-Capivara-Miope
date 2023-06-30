using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TransitionType
{
    Warp,
    Scene
}
;
public class Transition : MonoBehaviour
{
    [SerializeField] private TransitionType transitionType;
    [SerializeField] private string sceneNameToTransition;
    [SerializeField] private Vector3 targetPosition;
    
    private Transform destination;

    public PlayerMovementTeste playerMovementTeste;
    
    void Start()
    {
        destination = transform.GetChild(1);
        playerMovementTeste = GetComponent<PlayerMovementTeste>();
    }

    void Update()
    {
        
    }

    public void InitiateTransition(Transform toTransition)
    {
        switch (transitionType)
        {
            case TransitionType.Warp:
                Cinemachine.CinemachineBrain currentCamera = Camera.main.GetComponent<CinemachineBrain>();
                currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(
                    toTransition,
                    destination.position - toTransition.position
                );
                toTransition.position = new Vector3(
                    destination.position.x,
                    destination.position.y,
                    toTransition.position.z);
                break;
            
            case TransitionType.Scene:
                GameSceneManager.instance.InitSwitchScene(sceneNameToTransition, targetPosition);
                break;
        }
        
    }
}
