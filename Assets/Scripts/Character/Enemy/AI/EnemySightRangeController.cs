using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is attached to each enemy and creates a list of all player GameObjects within range of this enemy for the purposes of testing whether the AI can see
//(or detect) the player 
public class EnemySightRangeController : MonoBehaviour {

    public StateController controller;
    public SphereCollider sphereCollider;

    public List<GameObject> playersInRange;

    public void Start()
    {
        //Sets the sphere collider size to match the lookRange of this enemy
        sphereCollider.radius = controller.enemyData.lookRange;
    }
    
    //Add the detected player from the list of player objects in range of this enemy
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!playersInRange.Contains(other.gameObject))
                playersInRange.Add(other.gameObject);
        }
    }
    //Remove the detected player from the list of player objects in range of this enemy
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playersInRange.Contains(other.gameObject))
                playersInRange.Remove(other.gameObject);
        }
    }
}
