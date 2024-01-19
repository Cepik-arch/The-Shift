using System.Collections;
using TMPro;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    public GameObject subtitlesUI;
    public TextMeshProUGUI subtitleText;
    public float displayDuration = 2.0f; // Time duration for each subtitle to display
    public AudioSource audioSource;
    public AudioClip[] audioClips; // Array of audio clips for subtitles
    public string[] subtitles; // Array of subtitles corresponding to the audio clips

    private bool isShowingSubtitles;
    private int currentSubtitleIndex;


    private void Start()
    {
        subtitleText.text = string.Empty; // Clear subtitle text at start
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isShowingSubtitles)
            {
                StartCoroutine(DisplaySubtitles());
            }
            else
            {
                // If subtitles are already showing, wait until they finish and then start new ones.
                StartCoroutine(WaitForSubtitlesToEndAndStartNew());
            }
        }
    }

    private IEnumerator WaitForSubtitlesToEndAndStartNew()
    {
        // Wait until subtitles finish
        while (isShowingSubtitles)
        {
            yield return null;
        }

        // Start new subtitles
        StartCoroutine(DisplaySubtitles());
    }

    public IEnumerator DisplaySubtitles()
    {
        subtitlesUI.SetActive(true);
        isShowingSubtitles = true;

        while (currentSubtitleIndex < subtitles.Length)
        {
            Debug.Log("Displaying subtitle: " + subtitles[currentSubtitleIndex]);
            subtitleText.text = subtitles[currentSubtitleIndex];

            if (audioSource != null && audioClips.Length > currentSubtitleIndex && audioClips[currentSubtitleIndex] != null)
            {
                audioSource.clip = audioClips[currentSubtitleIndex];
                audioSource.Play();
                Debug.Log("Playing audio: " + audioSource.clip.name);

                yield return new WaitForSeconds(audioSource.clip.length); // Wait for audio clip length
            }
            else
            {
                yield return new WaitForSeconds(displayDuration);
            }
            currentSubtitleIndex++;
        }
    
        subtitlesUI.SetActive(false);
        isShowingSubtitles = false;
        subtitleText.text = string.Empty; // Clear subtitle text after all subtitles are shown
    }

    public void ResetSubtitles()
    {
        // Stop any ongoing coroutine and reset variables
        StopAllCoroutines();
        isShowingSubtitles = false;
        currentSubtitleIndex = 0;
        subtitleText.text = string.Empty;
        subtitlesUI.SetActive(false);
    }
}
