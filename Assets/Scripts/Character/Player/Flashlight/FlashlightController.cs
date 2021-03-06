﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class FlashlightController : MonoBehaviour {

    [HideInInspector] public Light flashLight;
    Camera firstPersonCamera;
    //The linear interpolation speed for all values
    [Header("General Flashlight Settings: ")]
    [Range(0, 1)]
    public float lightLerpSpeed;
    [Space()]
    
    [Header("Focussed Flashlight Settings: ")]
    [Range(0, 20)]
    public float focussedRange;
    [Range(0, 180)]
    public float focussedSpotAngle;
    [Range(0, 10)]
    public float focussedIntensity;
    [Range(1, 100)]
    public int focussedCameraFOV;
    [Space()]

    [Header("Unfocussed Flashlight Settings: ")]
    [Range(0, 20)]
    public float unfocussedRange;
    [Range(0, 180)]
    public float unfocussedSpotAngle;
    [Range(0, 10)]
    public float unfocussedIntensity;
    [Range(1, 100)]
    public int unfocussedCameraFOV;
    [Space()]

    bool focussing = false;
    [HideInInspector] public bool focussed = false;

    public void Awake()
    {
        flashLight = GetComponent<Light>();
        if (flashLight == null)
            throw new UnassignedReferenceException();
        firstPersonCamera = GetComponentInParent<Camera>();
        if (firstPersonCamera == null)
            throw new UnassignedReferenceException();
    }

    public void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            focussed = false;
            focussing = false;
            //Use co-routines for smoother handling of linear interpolation and interrupts
            StartCoroutine(UnfocusFL());

        }
        else
        {
            if (Input.GetMouseButton(1))
            {
                focussed = true;
                focussing = true;
                //Use co-routines for smoother handling of linear interpolation and interrupts
                StartCoroutine(FocusFL());
            }
            else
            {
                focussing = false;
            }
        }
    }

    //Loops through each frame interpolating towards the desired focussed flashlight state, halts after the
    //frame that focussed is set to false or it reaches the desired values
    IEnumerator FocusFL ()
    {
        while (((flashLight.range != focussedRange) || (flashLight.spotAngle != focussedSpotAngle) || (flashLight.intensity != focussedIntensity)) && (focussing))
        {
            flashLight.range = Mathf.Lerp(flashLight.range, focussedRange, lightLerpSpeed);
            if (Mathf.Abs(flashLight.range - focussedRange) < 0.1f)
                flashLight.range = focussedRange;
            flashLight.spotAngle = Mathf.Lerp(flashLight.spotAngle, focussedSpotAngle, lightLerpSpeed);
            if (Mathf.Abs(flashLight.spotAngle - focussedSpotAngle) < 0.1f)
                flashLight.spotAngle = focussedSpotAngle;
            flashLight.intensity = Mathf.Lerp(flashLight.intensity, focussedIntensity, lightLerpSpeed);
            if (Mathf.Abs(flashLight.intensity - focussedIntensity) < 0.1f)
                flashLight.intensity = focussedIntensity;
            firstPersonCamera.fieldOfView = Mathf.Lerp(firstPersonCamera.fieldOfView, focussedCameraFOV, lightLerpSpeed);
            if (Mathf.Abs(firstPersonCamera.fieldOfView - focussedCameraFOV) < 0.1f)
                firstPersonCamera.fieldOfView = focussedCameraFOV;
            if ((flashLight.range == focussedRange) || (flashLight.spotAngle == focussedSpotAngle) || (flashLight.intensity == focussedIntensity))
                focussing = false;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    //Loops through each frame interpolating towards the desired unfocussed flashlight state, halts after the
    //frame that focussed is set to true or it reaches the desired values
    IEnumerator UnfocusFL ()
    {
        while (((flashLight.range != unfocussedRange) || (flashLight.spotAngle != unfocussedSpotAngle) || (flashLight.intensity != unfocussedIntensity)) && (!focussing))
        {
            flashLight.range = Mathf.Lerp(flashLight.range, unfocussedRange, lightLerpSpeed);
            if (Mathf.Abs(flashLight.range - unfocussedRange) < 0.1f)
            {
                flashLight.range = unfocussedRange;
            }
            flashLight.spotAngle = Mathf.Lerp(flashLight.spotAngle, unfocussedSpotAngle, lightLerpSpeed);
            if (Mathf.Abs(flashLight.spotAngle - unfocussedSpotAngle) < 0.1f)
            {
                flashLight.spotAngle = unfocussedSpotAngle;
            }
            flashLight.intensity = Mathf.Lerp(flashLight.intensity, unfocussedIntensity, lightLerpSpeed);
            if (Mathf.Abs(flashLight.intensity - unfocussedIntensity) < 0.1f)
            {
                flashLight.intensity = unfocussedIntensity;
            }
            firstPersonCamera.fieldOfView = Mathf.Lerp(firstPersonCamera.fieldOfView, unfocussedCameraFOV, lightLerpSpeed);
            if (Mathf.Abs(firstPersonCamera.fieldOfView - unfocussedCameraFOV) < 0.1f)
                firstPersonCamera.fieldOfView = unfocussedCameraFOV;
            if ((flashLight.range == unfocussedRange) || (flashLight.spotAngle == unfocussedSpotAngle) || (flashLight.intensity == unfocussedIntensity))
                focussing = true;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
