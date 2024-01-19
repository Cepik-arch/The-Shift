using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerOpen : MonoBehaviour
{
    public GameObject door;
    public float delayTime = 0f;

    private DoorController doorController;

    private void Start()
    {
        doorController = door.GetComponent<DoorController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && doorController != null)
        {
            Invoke("OpenDoorWithDelay", delayTime);
        }
    }

    private void OpenDoorWithDelay()
    {
        if (!doorController.IsDoorOpen())
        {
            doorController.OpenDoor();
        }
    }
}
