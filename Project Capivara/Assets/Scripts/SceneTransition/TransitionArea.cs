using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TransitionArea : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            print("!@!@");
            transform.parent.GetComponent<Transition>().InitiateTransition(collision.transform);
        }
    }
}
