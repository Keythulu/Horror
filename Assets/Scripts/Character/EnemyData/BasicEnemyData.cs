using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyData : ScriptableObject {

    public int healthValue = 50;
    public float timeInFlashLightToTrigger = 0.5f;
    public float speed;
    public float attackDamage;
}
