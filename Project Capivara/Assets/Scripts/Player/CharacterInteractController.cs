using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterInteractController : MonoBehaviour
{
    private PlayerMovement playerMovementController;
    private Rigidbody2D rgbd2d;
    [SerializeField] private float offsetDistance = 1F;
    [SerializeField] private float sizeOfInteractableArea = 1.2F;
    private Character character;
    
    void Awake()
    {
        playerMovementController = GetComponent<PlayerMovement>();
        rgbd2d = GetComponent<Rigidbody2D>();
        character = GetComponent<Character>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Interact();    
        }
    }

    public void Interact()
    {
        Vector2 position = rgbd2d.position + playerMovementController.lastMotionVector * offsetDistance;

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
