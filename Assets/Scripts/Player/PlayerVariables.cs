using UnityEngine;
using StarterAssets;

public class PlayerVariables : MonoBehaviour
{
    public static PlayerVariables instance;
    public GameObject player;
    public int playerID;
    public int keyID;

    public bool stateSleep;
    public bool stateWork;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void canMove(bool canMove)
    {
        if (canMove)
        {
            EnablePlayerController();

        }
        else
        {
            DisablePlayerController();
        }
    }

    private void DisablePlayerController()
    {
        FirstPersonController playerController = player.GetComponent<FirstPersonController>();

        if (playerController != null)
        {
            playerController.enabled = false;
        }
        else
        {
            Debug.LogError("FirstPersonController script not found on the player GameObject!");
        }
    }

    private void EnablePlayerController()
    {
        FirstPersonController playerController = player.GetComponent<FirstPersonController>();

        if (playerController != null)
        {
            playerController.enabled = true;
        }
        else
        {
            Debug.LogError("FirstPersonController script not found on the player GameObject!");
        }
    }

    public void setStateSleep(bool canSleep)
    {
        stateSleep = canSleep;
    }

    public void setStateWork(bool canWork)
    {
        stateWork = canWork;
    }

    public bool getStateSleep()
    {
        return stateSleep;
    }

    public bool getStateWork()
    {
        return stateWork;
    }


    public void setID(int id)
    {
        playerID = id;
    }

    public int getID()
    {
        return playerID;
    }

    public void setKeyID(int id)
    {
        keyID = id;
    }

    public int getKeyID()
    {
        return keyID;
    }
}
