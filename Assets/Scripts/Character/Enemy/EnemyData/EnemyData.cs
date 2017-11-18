using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Enemy Data")]
public class EnemyData : ScriptableObject {

    public int healthValue;
    public float timeInFlashLightToTrigger;
    public float lookRange;
    public float sprintSpeed;
    public float attackDamage;

    public float investigateDistance;
    public float investigateTimer;

    public float pursuitTimeout;
}
