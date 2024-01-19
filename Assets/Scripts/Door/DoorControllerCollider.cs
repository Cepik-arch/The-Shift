using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControllerCollider : MonoBehaviour
{
    public GameObject door; // Reference to the door GameObject
    public float delayTime = 1.5f; // Delay time before opening/closing the door
    private bool isOpen = false;


    public GameObject enterColliderObject; // Collider for player entering
    public GameObject exitColliderObject; // Collider for player exiting

    private Collider enterCollider;
    private Collider exitCollider;

    public Animator doorAnimator;

    private void Start()
    {
        if (enterColliderObject != null)
            enterCollider = enterColliderObject.GetComponent<Collider>();

        if (exitColliderObject != null)
            exitCollider = exitColliderObject.GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpen && other.gameObject == enterColliderObject)
        {
            isOpen = true;
            Invoke("OpenDoor", delayTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isOpen && other.gameObject == exitColliderObject)
        {
            isOpen = false;
            Invoke("CloseDoor", delayTime);
        }
    }

    private void OpenDoor()
    {
        if (doorAnimator != null && isOpen == false)
        {
            doorAnimator.SetTrigger("Open");
            Debug.LogError("Opening");
            isOpen = true;
        }
        else
        {
            Debug.LogError("Animator component not found!");
        }
    }

    private void CloseDoor()
    {
        if (doorAnimator != null && isOpen == true)
        {
            doorAnimator.SetTrigger("Close");
            Debug.LogError("Closing");
            isOpen = false;
        }
        else
        {
            Debug.LogError("Animator component not found!");
        }
    }
}
