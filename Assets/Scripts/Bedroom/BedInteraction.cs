using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedInteraction : MonoBehaviour
{
    public GameObject doorOpen;
    public GameObject doorClose;

    public float interactionRange = 1f; // Range for interaction with the object
    public bool changePlayerId;
    public Transform teleportPosition;

    public int roomID;

    public GameObject player;
    private PlayerVariables playerVariables;

    public GameObject voiceObject;
    private ResetAllSubtitles resetScript;

    public CallUI UI;

    void Awake()
    {
        playerVariables = FindObjectOfType<PlayerVariables>();

        resetScript = voiceObject.GetComponent<ResetAllSubtitles>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && IsPlayerLookingAtObject() && playerVariables.getStateSleep() != true)
        {
            if (changePlayerId)
            {
                playerVariables = FindObjectOfType<PlayerVariables>();

                if (playerVariables.getKeyID() != roomID)
                {
                    UI.setUIstate(true);
                    playerVariables.canMove(false);
                    playerVariables.setStateSleep(true);

                    DoorController doorController = doorOpen.GetComponent<DoorController>();

                    if (doorController != null)
                    {
                        doorController.OpenDoor();
                    }
                    else
                    {
                        Debug.LogError("DoorController component not found on the assigned doorOpen GameObject!");
                    }

                    doorController = doorClose.GetComponent<DoorController>();
                    if (doorController != null)
                    {
                        doorController.CloseDoor();
                    }
                    else
                    {
                        Debug.LogError("DoorController component not found on the assigned doorClose GameObject!");
                    }


                    changeID();
                    TeleportPlayer();
                    ResetAllSubtitles();

                    StartCoroutine(DisableAfterDelay(2.0f));

                }

            }


        }
    }

    void TeleportPlayer()
    {
        if (teleportPosition != null)
        {

            if (player != null)
            {
                
                Debug.Log("Teleporting..");
                player.transform.position = teleportPosition.transform.position;
            }
            else
            {
                Debug.LogError("Player object not found with the 'Player' tag!");
            }
        }
        else
        {
            Debug.LogError("Teleport destination not set!");
        }
    }


    private IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay

        playerVariables.canMove(true);
    }

    bool IsPlayerLookingAtObject()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));

        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            if (hit.collider.gameObject == gameObject) // Check if the object hit is this GameObject
            {
                return true;
            }
        }
        return false;
    }

    void changeID()
    {
        int playerID = playerVariables.getKeyID();
        int keyID = playerVariables.getID();

        if (playerVariables != null)
        {
            playerVariables.setID(playerID);
            playerVariables.setKeyID(keyID);
        }
        else
        {
            Debug.LogError("PlayerVariables script not found!");
        }
    }

    public void ResetAllSubtitles()
    {
        if (voiceObject != null)
        {

            if (resetScript != null)
            {
                resetScript.ResetAllSubtitleManagers();
            }
        }
    }

}
