using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadEssentials : MonoBehaviour
{
    List<AsyncOperation> sceneToLoad = new List<AsyncOperation>();

    public void StartGame()
    {
        sceneToLoad.Add(SceneManager.LoadSceneAsync("CenaPrincipal"));
        sceneToLoad.Add(SceneManager.LoadSceneAsync("Essentials", LoadSceneMode.Additive));
    }   
}
