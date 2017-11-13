using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable AI/Actions/Patrol")]
public class PatrolAction : Action
{
    public override void Act(StateController controller)
    {
        Patrol(controller);
    }

    private void Patrol(StateController controller)
    {
        Debug.Log("Patrolling");
        controller.navMeshAgent.destination = controller.debugWaypointList[controller.nextWaypoint].position;
        controller.navMeshAgent.isStopped = false;

        if ((controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance) && (!controller.navMeshAgent.pathPending))
        {
            controller.nextWaypoint = (controller.nextWaypoint + 1) % controller.debugWaypointList.Length;
        }
    }
}
