using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Resume()
    {
        menuPanel.SetActive(false);
        Time.timeScale = 1;

    }

    public void Quit()
    {
        Application.Quit();
    }
}
