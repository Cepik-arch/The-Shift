using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlickeringLight : MonoBehaviour
{
    public float minIntensity = 0.5f;
    public float maxIntensity = 2.0f;
    public float flickerSpeed = 1.0f;
    public float pulsationSpeed = 2.0f;

    private Light flickeringLight;
    private float originalIntensity;
    private float targetIntensity;

    void Start()
    {
        flickeringLight = GetComponent<Light>();

        if (flickeringLight != null)
        {
            originalIntensity = flickeringLight.intensity;
            CalculateTargetIntensity();
            InvokeRepeating("Flicker", 0.0f, flickerSpeed);
            InvokeRepeating("Pulsate", 0.0f, pulsationSpeed);
        }
        else
        {
            Debug.LogError("ScaryFlickeringLight script attached to an object without a Light component.");
        }
    }

    void CalculateTargetIntensity()
    {
        targetIntensity = Random.Range(minIntensity, maxIntensity);
    }

    void Flicker()
    {
        float smoothStep = Mathf.SmoothStep(0, 1, Mathf.PingPong(Time.time * flickerSpeed, 1));
        flickeringLight.intensity = Mathf.Lerp(originalIntensity, targetIntensity, smoothStep);

        // Recalculate target intensity for the next flicker
        if (Mathf.Approximately(smoothStep, 0.0f) || Mathf.Approximately(smoothStep, 1.0f))
        {
            CalculateTargetIntensity();
        }
    }

    void Pulsate()
    {
        float pulsation = Mathf.PingPong(Time.time * pulsationSpeed, 1);
        flickeringLight.color = new Color(1, 1 - pulsation, 1 - pulsation);
    }
}