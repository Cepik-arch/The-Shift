using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndInteraction : MonoBehaviour
{
    public float interactionRange = 2f; // Range for interaction with the object

    public GameObject player;
    private PlayerVariables playerVariables;

    public CallUI UI;

    public GameObject gameManager;
    private CursorManager cursorManager;

    void Awake()
    {
        playerVariables = FindObjectOfType<PlayerVariables>();

        cursorManager = gameManager.GetComponent<CursorManager>();

        //resetScript = voiceObject.GetComponent<ResetAllSubtitles>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && IsPlayerLookingAtObject())
        {
            playerVariables = FindObjectOfType<PlayerVariables>();

            UI.setUIstate(true);
            playerVariables.canMove(false);

            StartCoroutine(DisableAfterDelay(3.0f));

        }
    }

    private IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay

        cursorManager.SetCursorActive(true);
        SceneManager.LoadScene("Menu");

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
}
