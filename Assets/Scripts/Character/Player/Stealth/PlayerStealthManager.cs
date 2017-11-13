using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LightValueDetection))]
public class PlayerStealthManager : MonoBehaviour {

    [Header("Required Components: ")]
    public LightValueDetection lightValueDetection;

    [Header("Stealth Chance Values: ")]
    [Range(0, 100)]
    public float currentChanceToReveal;
    [Range(0, 100)]
    public float baseChanceToReveal;
    [Range(0, 100)]
    public float crouchRevealChanceReduction;

    private bool crouching;

    public void Update()
    {
        UpdateChanceToReveal();
    }
    private void UpdateChanceToReveal()
    {
        if (crouching)
            currentChanceToReveal = baseChanceToReveal + lightValueDetection.lightValue + currentChanceToReveal;
        else
            currentChanceToReveal = baseChanceToReveal + lightValueDetection.lightValue;
    }

}
