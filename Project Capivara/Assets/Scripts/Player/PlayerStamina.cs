using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    public Image staminaBar;
    public float stamina = 100f;

    public void Update()
    {
        if (stamina <= 0)
        {
            GameManager.instance.playerMovement.enabled = false;
        }

        else
        {
            GameManager.instance.playerMovement.enabled = true;
        }
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeStamina(20);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            GiveStamina(5);
        }
    }

    public void TakeStamina(float takeStamina)
    {
        stamina -= takeStamina;
        staminaBar.fillAmount = stamina / 100f;
    }

    public void GiveStamina(float giveStamina)
    {
        stamina += giveStamina;
        stamina = Mathf.Clamp(stamina, 0, 100);
        staminaBar.fillAmount = stamina / 100f;
    }
}
