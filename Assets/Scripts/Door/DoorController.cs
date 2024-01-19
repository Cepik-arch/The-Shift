using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator;
    public bool openOnAwake;

    private bool open =false;

    void Awake()
    {
        if (openOnAwake)
        {
            OpenDoor();
            open = true;

        }
        else
        {
            open = false;
        }
    }


    public void OpenDoor()
    {
        if (doorAnimator != null && open == false)
        {
            doorAnimator.SetTrigger("Open");
            open = true;
        }
        else
        {
            Debug.LogError("Animator component not found!");
        }
    }

    public void CloseDoor()
    {
        if (doorAnimator != null && open == true)
        {
            doorAnimator.SetTrigger("Close");
            open = false;
        }
        else
        {
            Debug.LogError("Animator component not found!");
        }
    }

    public bool IsDoorOpen()
    {
        return open;
    }
}
