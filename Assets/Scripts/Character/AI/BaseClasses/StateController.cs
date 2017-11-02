using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour {

    public State currentState;
    public GameObject aiHead;
    public State remainState;

    public float gizmoSphereRadius;
    public float lookRange;
    private bool aiActive = true;

    public Transform[] debugWaypointList;
    public NavMeshAgent navMeshAgent;

    [HideInInspector] public int nextWaypoint = 0;
    [HideInInspector] public Transform chaseTarget;

    public void Update()
    {
        if (!aiActive)
            return;
        currentState.UpdateState(this);
    }

    private void OnDrawGizmos()
    {
        if (currentState != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(aiHead.transform.position, gizmoSphereRadius);
        }
    }

    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            currentState = nextState;
        }
    }
}
