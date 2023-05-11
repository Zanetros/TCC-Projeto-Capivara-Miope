using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterInteractController : MonoBehaviour
{
    private PlayerMovementTeste playerMovementController;
    private Rigidbody2D rgbd2d;
    [SerializeField] private float offsetDistance = 1F;
    [SerializeField] private float sizeOfInteractableArea = 1.2F;
    private Character character;
    [SerializeField] private HighLightController highLightController;
    private Vector2 position;
    
    void Awake()
    {
        playerMovementController = GetComponent<PlayerMovementTeste>();
        rgbd2d = GetComponent<Rigidbody2D>();
        character = GetComponent<Character>();
    }

    private void Update()
    {

        Check();

        if (Input.GetMouseButtonDown(1))
        {
            Interact();    
        }
    }
    
    private void Check()
    {
        position = rgbd2d.position + playerMovementController.lastMotionVector * offsetDistance;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);
        
        foreach (Collider2D c in colliders)
        {
            Interactable hit = c.GetComponent<Interactable>();
            if (hit != null)
            {
                highLightController.Highlight(hit.gameObject);
                return;
            }
        }
        highLightController.Hide();
    }

    public void Interact()
    {
        Debug.Log("interagido");
        Vector2 position = rgbd2d.position * offsetDistance;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        foreach (Collider2D c in colliders)
        {
            Interactable hit = c.GetComponent<Interactable>();
            if (hit != null)
            {
                hit.Interact(character);
                break;
            }
        }
    }
}