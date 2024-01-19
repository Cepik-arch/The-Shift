using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerClose : MonoBehaviour
{
    public GameObject door;
    public float delayTime = .1f;

    private DoorController doorController;

    private void Start()
    {
        doorController = door.GetComponent<DoorController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && doorController != null)
        {
            Invoke("CloseDoorWithDelay", delayTime);
        }
    }

    private void CloseDoorWithDelay()
    {
        if (doorController.IsDoorOpen())
        {
            doorController.CloseDoor();
        }
    }
}
