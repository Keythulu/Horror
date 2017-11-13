using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//This script loops through each light in the scene, checking whether it is a point light or not. If it is, then it checks to see if it has a sphere collider is attached.
//If it does not, a warning is thrown.
//This is for the way light stealth is handled in this game, as light value detection is based on distance from a point light, as opposed to any texture light detection
[ExecuteInEditMode]
public class PointLightManager : MonoBehaviour {

    private void Update()
    {
        Light[] sceneLights;
        sceneLights = FindObjectsOfType<Light>();
        foreach (Light light in sceneLights)
        {
            if (light.type == LightType.Point)
            {
                if (!light.GetComponent<SphereCollider>())
                {
                    Debug.LogWarning("Point lights in the scene do not have sphere colliders attached. This will result in buggy behaviour if the light is intended to be " +
                        "used with the stealth system.");
                }
            }
        }
    }
}
