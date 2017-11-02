using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//THIS CLASS USES A CUSTOM INSPECTOR SCRIPT
[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour {

    //Simple flicker effect achieved with interpolating between a randomized range of a point light
    //between two values
    [Header("Required Flicker Components")]
    public Light flickerLight;

    [Header("Light Flicker Settings")]
    [Range(0, 10)]
    public float rangeMin;
    [Range(0, 10)]
    public float rangeMax;
    [Range(0, 1)]
    public float lightFlickerLerpSpeed;

    //This setting determines how many randomized flickers will pass before it flickers back to its base setting. This prevents the light from looking too different
    //from it's base settings. This gives it a look stability that the randomized settings may not give.
    public int lightFlickersBeforeReset;

    //Optional: Can interpolate between colors for added dynamics
    //NOTE: The first element in the array is the lights base color setting
    [Header("Color Settings")]
    public bool colorInterpolation;
    public Color[] colors;
    //
    //

    private float baseRange;
    private bool doneFlicker;
    private int flickerTicker;

    public void Start()
    {
        baseRange = flickerLight.range;
        if (colorInterpolation)
            colors[0] = flickerLight.color;
        doneFlicker = true;
    }
    public void Update()
    {
        if (doneFlicker)
            StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        int randomColour;
        float randomRange;
        doneFlicker = false;

        if (flickerTicker >= lightFlickersBeforeReset)
        {
            randomRange = baseRange;
            randomColour = 0;
            flickerTicker = 0;
        }
        else
        {
            randomRange = Random.Range(rangeMin, rangeMax);
            //This line does not check for colorInterpolation boolean due to throwing compiler errors
            randomColour = (int)Random.Range(0, colors.Length - 1);
        }
        while (flickerLight.range != randomRange)
        {
            flickerLight.range = Mathf.Lerp(flickerLight.range, randomRange, lightFlickerLerpSpeed);
            if (Mathf.Abs(flickerLight.range - randomRange) <= 0.1)
                flickerLight.range = randomRange;
            //Optional color interpolation
            if (colorInterpolation)
            {
                flickerLight.color = Color.Lerp(flickerLight.color, colors[randomColour], lightFlickerLerpSpeed);
                if ((Mathf.Abs(flickerLight.color.r - colors[randomColour].r) <= 0.1) &&
                        (Mathf.Abs(flickerLight.color.g - colors[randomColour].g) <= 0.1) &&
                        (Mathf.Abs(flickerLight.color.b - colors[randomColour].b) <= 0.1))
                {
                    flickerLight.color = colors[randomColour];
                }
            }
            //
            
            yield return new WaitForEndOfFrame();
        }
        flickerTicker++;
        doneFlicker = true;
        yield return null;
    }
}
