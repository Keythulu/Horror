using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FirstPersonController))]
[RequireComponent(typeof(PlayerStealthManager))]
[RequireComponent(typeof(LightValueDetection))]
public class PlayerActor : MonoBehaviour {

    public PlayerData playerData;
}
