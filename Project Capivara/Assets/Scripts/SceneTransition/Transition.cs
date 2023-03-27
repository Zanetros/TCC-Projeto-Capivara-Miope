using System.Collections;
using System.Collections.Generic;
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
    
    private Transform destination;
    
    // Start is called before the first frame update
    void Start()
    {
        destination = transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitiateTransition(Transform toTransition)
    {
        switch (transitionType)
        {
            case TransitionType.Warp:
                toTransition.position = new Vector3(
                    destination.position.x,
                    destination.position.y,
                    toTransition.position.z);
                break;
            
            case TransitionType.Scene:
                print("FOI?");
                SceneManager.LoadScene(sceneNameToTransition);
                break;
        }
        
    }
}
