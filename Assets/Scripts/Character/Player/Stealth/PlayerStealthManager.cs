using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerActor))]
[RequireComponent(typeof(LightValueDetection))]
[RequireComponent(typeof(FirstPersonController))]
public class PlayerStealthManager : MonoBehaviour {

    [HideInInspector] public float currentChanceToReveal;

    PlayerActor playerActor;
    LightValueDetection lightValueDetection;
    FirstPersonController fpController;

    public void Awake()
    {
        playerActor = GetComponent<PlayerActor>();
        if (playerActor == null)
            throw new UnassignedReferenceException();
        lightValueDetection = GetComponent<LightValueDetection>();
        if (lightValueDetection == null)
            throw new UnassignedReferenceException();
        fpController = GetComponent<FirstPersonController>();
        if (fpController == null)
            throw new UnassignedReferenceException();
    }

    public void Update()
    {
        UpdateChanceToReveal();
    }

    private void UpdateChanceToReveal()
    {
        if (fpController.crouching)
            currentChanceToReveal = playerActor.playerData.stealthProperties.baseChanceForRevealed + 
                lightValueDetection.lightValue + 
                playerActor.playerData.stealthProperties.crouchedChanceModifier;
        else
            currentChanceToReveal = playerActor.playerData.stealthProperties.baseChanceForRevealed + 
                lightValueDetection.lightValue;
    }

}
