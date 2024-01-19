using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ButtonTextEffect : MonoBehaviour
{
    private TextMeshProUGUI buttonText;
    private Color originalColor;
    public float darkeningMultiplier = 0.5f; // Adjust the multiplier for the desired darkness
    public float fadeDuration = 1.0f; // Adjust the duration for the fade back to original color

    void Awake()
    {
        buttonText = GetComponent<TextMeshProUGUI>();
        originalColor = buttonText.color;
    }

    public void OnButtonPressed()
    {
        // Change the text color to a darker shade when the button is pressed
        buttonText.color = originalColor * darkeningMultiplier;

        // Start the coroutine to fade back to the original color after some time
        StartCoroutine(FadeBackToOriginal());
    }

    private IEnumerator FadeBackToOriginal()
    {
        yield return new WaitForSeconds(fadeDuration);

        // Gradually fade back to the original color
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            buttonText.color = Color.Lerp(originalColor * darkeningMultiplier, originalColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        buttonText.color = originalColor; // Ensure the final color is set exactly
    }
}