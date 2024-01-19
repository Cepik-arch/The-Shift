using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using StarterAssets;

public class ChairInteraction : MonoBehaviour
{
    public float interactionRange = 1f; // Range for interaction with the object

    public GameObject player;
    private PlayerVariables playerVariables;

    public float waitTime = 1f;

    public CallUI UI;

    public GameObject subtitle;
    private SubtitleManager subtitleManager;

    void Awake()
    {
        playerVariables = FindObjectOfType<PlayerVariables>();
        subtitleManager = subtitle.GetComponent<SubtitleManager>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && IsPlayerLookingAtObject() && playerVariables.getStateWork() != true)
        {
            playerVariables = FindObjectOfType<PlayerVariables>();

            UI.setUIstate(true);
            playerVariables.canMove(false);
            playerVariables.setStateSleep(false);
            playerVariables.setStateWork(true);


            StartCoroutine(DisableAfterDelay(waitTime));

        }
    }

    private IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay

        if (subtitleManager != null)
        {
            subtitleManager.StartCoroutine(subtitleManager.DisplaySubtitles());
        }

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

    //public void ResetAllSubtitles()
    //{
    //    if (voiceObject != null)
    //    {

    //        if (resetScript != null)
    //        {
    //            resetScript.ResetAllSubtitleManagers();
    //        }
    //    }
    //}

}
