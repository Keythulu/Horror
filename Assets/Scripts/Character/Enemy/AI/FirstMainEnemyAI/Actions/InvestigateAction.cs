using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable AI/Actions/Investigate")]
public class InvestigateAction : Action {

    public override void Act(StateController controller)
    {
        Debug.Log("investigating");
        Investigate(controller);
    }

    private void Investigate(StateController controller)
    {
        controller.navMeshAgent.speed = controller.currentState.aiSpeed;
        controller.navMeshAgent.destination = controller.investigateTarget.position;
        if (Vector3.Distance(controller.navMeshAgent.destination, controller.transform.position) < controller.navMeshAgent.stoppingDistance)
        {
            controller.navMeshAgent.isStopped = true;
            Vector3.RotateTowards(controller.transform.position, (controller.navMeshAgent.destination - controller.transform.position).normalized * 100, 0.2f * Time.deltaTime, 0);
        }
        else
        {
            controller.navMeshAgent.isStopped = false;
        }
        
    }
}
