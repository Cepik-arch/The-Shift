using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CursorManager : MonoBehaviour
{
    public static CursorManager instance;

    private bool cursorActive = false;

    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private bool isChangingState = false;

    public void SetCursorActive(bool active)
    {
        if (!isChangingState)
        {
            isChangingState = true;
            StartCoroutine(UpdateCursorWithDelay(active));
        }
    }

    private IEnumerator UpdateCursorWithDelay(bool active)
    {
        yield return new WaitForSeconds(.1f); // Adjust the delay time as needed

        cursorActive = active;
        UpdateCursorState();
        isChangingState = false;
    }

    private void UpdateCursorState()
    {
        if (cursorActive)
        {
            Debug.Log("Cursor unlock");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Debug.Log("Cursor lock");
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

}
