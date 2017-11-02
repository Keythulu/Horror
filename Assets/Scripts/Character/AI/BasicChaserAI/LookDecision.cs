using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable AI/Decision/LookDecision")]
public class LookDecision : Decision
{

    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }

    private bool Look(StateController controller)
    {
        RaycastHit hit;
        Debug.DrawRay(controller.aiHead.transform.position, controller.aiHead.transform.forward.normalized * controller.lookRange, Color.green);
        if ((Physics.SphereCast(controller.aiHead.transform.position, controller.gizmoSphereRadius, controller.aiHead.transform.forward, out hit, controller.lookRange))
            && hit.collider.CompareTag("Player"))
        {
            controller.chaseTarget = hit.transform;
            return true;
        }
        else
            return false;
    }
}
