using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallUI : MonoBehaviour
{
    public GameObject workingUI; // Reference to the UI GameObject
    public float displayDuration = 6f; // Duration to display the UI in seconds
    public float interactionRange = 1f; // Range for interaction with the object

    private bool isUIActive = false;
    private float displayTimer = 0f;

    public bool showUI;

    void Update()
    {
        if (isUIActive)
        {
            displayTimer += Time.deltaTime;
            if (displayTimer >= displayDuration)
            {
                HideUI();
            }
        }

        if (showUI && !isUIActive) 
        {
            // Check if the player is looking at the object within interaction range
            ShowUI();
        }

    }

    public void ShowUI()
    {
        workingUI.SetActive(true);
        isUIActive = true;
        displayTimer = 0f;
    }

    public void HideUI()
    {
        workingUI.SetActive(false);
        isUIActive = false;
        showUI = false;
    }

    public void setUIstate(bool state)
    {
        showUI = state;
    }

    public bool getUIstate()
    {
        return showUI;
    }

}
