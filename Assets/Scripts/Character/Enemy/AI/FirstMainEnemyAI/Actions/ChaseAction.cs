using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable AI/Actions/Chase")]
public class ChaseAction : Action {

    public override void Act(StateController controller)
    {
        Debug.Log("Chasing");
        Chase(controller);
    }

    private void Chase(StateController controller)
    {
        controller.navMeshAgent.speed = controller.currentState.aiSpeed;
        controller.navMeshAgent.destination = controller.chaseTarget.transform.position;
    }
}
