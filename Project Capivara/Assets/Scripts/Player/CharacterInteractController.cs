using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractController : MonoBehaviour
{
    private CharacterController character;
    private Rigidbody2D rgbd2d;
    [SerializeField] private float offSetDistance = 1F;
    [SerializeField] private float sizeOfInteractableArea = 1.2F;

    void Awake()
    {
        character = GetComponent<CharacterController>();
        rgbd2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Interact();    
        }
    }

    private void Interact()
    {
        //Vector2 position = rgbd2d.position + offSetDistance;
    }
}
