using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using StarterAssets;

public class StorageKeyPadController : MonoBehaviour
{
    public GameObject gameManager;
    private CursorManager cursorManager;

    public TMP_Text displayText;
    public GameObject playerCamera;
    public GameObject correctUI;
    public GameObject invalidUI;

    public GameObject door;

    private bool keypadActive;
    private bool originalCursorState;
    public int maxDigits = 4; 
    public string password; 

    public float interactionRange = 1f;
    public GameObject player;

    private PlayerVariables playerVariables;

    public GameObject subtitleDoor;
    public GameObject subtitleWrong;

    private SubtitleManager subtitleDoorManager;
    private SubtitleManager subtitleWrongManager;

    void Awake()
    {
        playerVariables = FindObjectOfType<PlayerVariables>();

        subtitleDoorManager = subtitleDoor.GetComponent<SubtitleManager>();
        subtitleWrongManager = subtitleWrong.GetComponent<SubtitleManager>();

        cursorManager = gameManager.GetComponent<CursorManager>();

        originalCursorState = Cursor.visible; // Store the original cursor visibility state
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor initially
        keypadActive = false;
        HideUI(); // Hide UI elements initially
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerLookingAtObject())
        {
            keypadActive = !keypadActive;
            ToggleKeypad(keypadActive);

        }


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

    public void OnNumberButtonClick(int number)
    {
        if (displayText.text.Length < maxDigits)
        {
            displayText.text += number.ToString();
        }
    }

    public void OnDeleteButtonClick()
    {
        if (displayText.text.Length > 0)
        {
            displayText.text = displayText.text.Substring(0, displayText.text.Length - 1);
        }
    }

    public void OnEnterButtonClick()
    {

        if (displayText.text.Length == maxDigits)
        {
            if (displayText.text == password )
            {
                Debug.Log("Password correct! Door opened.");

                // Access the DoorController script from the assigned door GameObject
                if (door != null)
                {
                    DoorController doorController = door.GetComponent<DoorController>();
                    if (doorController != null)
                    {
                        doorController.OpenDoor(); // Call the OpenDoor function from DoorController
                    }
                    else
                    {
                        Debug.LogError("DoorController component not found on the assigned door GameObject!");
                    }
                }
                else
                {
                    Debug.LogError("Door GameObject is not assigned!");
                }

                if (subtitleDoorManager != null)
                {
                    subtitleDoorManager.StartCoroutine(subtitleDoorManager.DisplaySubtitles());
                }
                ShowCorrectUI();
            }
            else
            {
                if (subtitleWrongManager != null)
                {
                    subtitleWrongManager.StartCoroutine(subtitleWrongManager.DisplaySubtitles());
                }
                ShowInvalidUI();
            }

            displayText.text = "";
            keypadActive = !keypadActive;
            ToggleKeypad(keypadActive);
        }
    }

    void ToggleKeypad(bool state)
    {
        Transform keypad = transform.Find("Keypad");

        if (keypad != null)
        {
            keypad.gameObject.SetActive(state);
        }

        if (state)
        {
            ToggleCursor(true);
            playerVariables.canMove(false);
        }
        else
        {
            ToggleCursor(false);
            playerVariables.canMove(true);
        }
    }

    public void ToggleCursor(bool active)
    {
        cursorManager.SetCursorActive(active);
    }

    void HideUI()
    {
        correctUI.SetActive(false);
        invalidUI.SetActive(false);
    }

    void ShowCorrectUI()
    {
        HideUI();
        correctUI.SetActive(true);
    }

    void ShowInvalidUI()
    {
        HideUI();
        invalidUI.SetActive(true);
        StartCoroutine(HideAfterDelay(invalidUI));
    }

    IEnumerator HideAfterDelay(GameObject uiObject)
    {
        yield return new WaitForSeconds(3f); // Change the duration as needed
        uiObject.SetActive(false);
    }

    void TogglePlayerCamera(bool state)
    {
        Cinemachine.CinemachineBrain cinemachineBrain = playerCamera.GetComponent<Cinemachine.CinemachineBrain>();
        if (cinemachineBrain != null)
        {
            cinemachineBrain.enabled = state;
        }
    }

}
