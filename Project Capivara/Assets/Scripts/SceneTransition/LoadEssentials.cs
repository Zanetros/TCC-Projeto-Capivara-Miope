using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadEssentials : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Cidade", LoadSceneMode.Single);
        SceneManager.LoadScene("Essentials", LoadSceneMode.Additive);
    }   
}
