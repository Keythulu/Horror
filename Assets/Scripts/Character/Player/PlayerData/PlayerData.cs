using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Data/Player Data"))]
public class PlayerData : ScriptableObject {

    [System.Serializable]
    public class MovementProperties
    {
        [Range(0, 20)]
        public float slowWalkSpeed;
        [Range(0, 20)]
        public float walkSpeed;
        [Range(0, 20)]
        public float sprintSpeed;
        [Range(0, 100)]
        public float jumpForce;
    }

    [System.Serializable]
    public class StealthProperties
    {
        [Range(0, 100)]
        public float baseChanceForRevealed;
        [Range(0, -100)]
        public float crouchedChanceModifier;
    }

    [System.Serializable]
    public class InputProperties
    {
        [Range(0, 500)]
        public float mouseSensitivity;
    }

    public MovementProperties movementProperties;
    public StealthProperties stealthProperties;
    public InputProperties inputProperties;
    
    


}
