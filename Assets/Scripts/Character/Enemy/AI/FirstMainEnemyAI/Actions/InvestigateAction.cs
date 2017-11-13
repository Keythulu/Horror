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
        controller.navMeshAgent.destination = controller.investigateTarget.position;
    }
}
