using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    //AI State data 
    public State currentState;
    public GameObject aiEyes;
    public State remainState;
    private bool aiActive = true;

    //Enemy data
    public EnemyData enemyData;
    public EnemySightRangeController sightRangeController;

    //Debug sphere for seeing the enemys current state based on color 
    public float gizmoSphereRadius;

    //Flashlight data for this enemy
    public float currentTimeInFlashlight;
    public GameObject flashlightHoldingPlayer;

    //Navigation data for this enemy
    public Transform[] debugWaypointList;
    public NavMeshAgent navMeshAgent;

    //Helper variables to store navigational targetting data
    [HideInInspector] public int nextWaypoint = 0;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public Transform investigateTarget;

    public void Update()
    {
        if (!aiActive)
            return;
        currentState.UpdateState(this);
        StartCoroutine(CheckTimeInFlashlight());
    }

    //Draws a sphere around this enemy for the purposes of seeing it's current AI State
    private void OnDrawGizmos()
    {
        if (currentState != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(aiEyes.transform.position, gizmoSphereRadius);
        }
    }

    //Helper method to trasition to the next state
    public void TransitionToState(State nextState)
    {
        //RemainState is the state that will allow the ai to go from a decision back to their starting state
        if (nextState != remainState)
        {
            currentState = nextState;
        }
    }

    //Coroutine for determining the amount of time this enemy has been in the view of a flashlight for the purposes of an AI Decision
    public IEnumerator CheckTimeInFlashlight()
    {
        float tempTimer = 0; ;
        while (true)
        {
            if (currentTimeInFlashlight != 0)
            {
                if (tempTimer != currentTimeInFlashlight)
                {
                    tempTimer = currentTimeInFlashlight;
                }
                else
                {
                    flashlightHoldingPlayer = null;
                    currentTimeInFlashlight = 0;
                }               
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
