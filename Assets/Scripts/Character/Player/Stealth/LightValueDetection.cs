using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStealthManager))]
public class LightValueDetection : MonoBehaviour {
    //
    //This script handles my form of "light detection" with ranges and intensities of lights to output a lightValue that will serve as a percentage modifier
    //for enemies to spot you
    //NOTE: Currently supports only point lights, which are not handled additively
    //NOTE: Point lights require a collider and to be tagged as lights in order for this script to work appropriately
    //
    private Light[] sceneLights;

    //Used as a percentage modifier to determine whether the player will be revealed if in sight range of an enemy
    [Range(0, 100)]
    public float lightValue;
    public GameObject player;

    //Initializes an array of all Light objects in the scene for the purposes of "light detection"
    public void Start()
    {
        sceneLights = FindObjectsOfType<Light>();
    }
    public void Update()
    {
        CalculateLightValue();
    }

    private void CalculateLightValue()
    {
        //Bool that will reset the lightValue to 0 if no lights are in range
        bool inRangeOfLight = false;
        //Stores the current highestLightValue as the Lights are iterated through
        //NOTE: Additive lighting is not supported by this script. Percentage chances may be erratic when in range of overlapping point lights
        float highestLightValue = 0;
        //For loop to iterate through Lights array
        for (int i = 0; i < sceneLights.Length; i++)
        {
            //Ensures that the light we are dealing with is of type point.
            if (sceneLights[i].type == LightType.Point)
            {   
                float playerLightDistance = Vector3.Distance(player.transform.position, sceneLights[i].transform.position);
                //Determines if the player is within range of this light
                if (playerLightDistance < sceneLights[i].range)
                {
                    inRangeOfLight = true;
                    RaycastHit hit;
                    //Raycast towards the light
                    Physics.Raycast(player.transform.position, (sceneLights[i].transform.position - player.transform.position), out hit, playerLightDistance);
                    Debug.DrawRay(player.transform.position, (sceneLights[i].transform.position - player.transform.position) * playerLightDistance, Color.red);
                    if (hit.collider != null)
                    { 
                        //Check to see if hit object is a light. If so, then there isn't any object between the player and the light.
                        if (hit.collider.CompareTag("Light"))
                        {
                            float tempLightValue;
                            
                            if (playerLightDistance < (sceneLights[i].intensity / 2) + 3)
                            {
                                //This modifier serves to increase the percentage chance to reveal of lighting as the player gets closer to the light. This is to 
                                //address the way that lights behave when closer to the source
                                tempLightValue = (100 - ((playerLightDistance / (sceneLights[i].range + sceneLights[i].intensity * 10))) * 100.0f);
                            }
                            else
                                //Otherwise, the percentage chance to reveal is linearly distance based
                                tempLightValue = (100 - ((playerLightDistance / sceneLights[i].range) * 100.0f));
                            //Uses to see if this light value is the highest out of all the lights calculated so far
                            if (tempLightValue > highestLightValue)
                                highestLightValue = tempLightValue;
                        }
                    }
                }

            }
        }
        //If the player is in range of the light, then their lightValue or chance to reveal is set to the highest one calculated, otherwise it is 0.
        if (!inRangeOfLight)
            lightValue = 0;
        else
            lightValue = highestLightValue;
    }
}
