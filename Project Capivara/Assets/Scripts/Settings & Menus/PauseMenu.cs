using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject soundPanel;
    public GameManager gameManager;
    private float normalFTime;

    void Start()
    {
        normalFTime = Time.fixedDeltaTime;
    }
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuPanel.SetActive(true);
            Time.timeScale = 0;
            Time.fixedDeltaTime = 0F;
            gameManager.musicManager.Pause_UnpauseMusic(true);
            gameManager.playerMovement.enabled = false;
            gameManager.characterInteractController.enabled = false;
            gameManager.inventoryController.enabled = false;
        }
    }

    public void Resume()
    {
        menuPanel.SetActive(false);
        Time.timeScale = 1;
        Time.fixedDeltaTime = normalFTime;
        gameManager.musicManager.Pause_UnpauseMusic(false);
        gameManager.playerMovement.enabled = true;
        gameManager.characterInteractController.enabled = true;
        gameManager.inventoryController.enabled = true;
    }

    public void SoundMenu()
    {
        menuPanel.SetActive(false);
        soundPanel.SetActive(true);
    }

    public void SoundBackToMenu()
    {
        menuPanel.SetActive(true);
        soundPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
