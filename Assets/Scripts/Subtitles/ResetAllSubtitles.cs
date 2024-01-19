using System.Collections.Generic;
using UnityEngine;

public class ResetAllSubtitles : MonoBehaviour
{
    public GameObject parentObject; // Assign the parent object that holds all the SubtitleManagers

    public void ResetAllSubtitleManagers()
    {
        Debug.Log("Resetting all subtitles...");
        if (parentObject != null)
        {
            ResetSubtitlesRecursively(parentObject.transform);
        }
    }

    private void ResetSubtitlesRecursively(Transform parent)
    {
        SubtitleManager subtitleManager = parent.GetComponent<SubtitleManager>();

        if (subtitleManager != null)
        {
            subtitleManager.ResetSubtitles(); // Reset subtitles if found
        }

        foreach (Transform child in parent)
        {
            ResetSubtitlesRecursively(child); // Recursively check children
        }
    }
}
