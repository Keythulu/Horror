using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable AI/Decision/Resume Patrol")]
public class ResumePatrolDecision : Decision {

    private float timer;

    public override bool Decide(StateController controller)
    {
        return ResumePatrol(controller);
    }

    public bool ResumePatrol(StateController controller)
    {
        if (Vector3.Distance(controller.gameObject.transform.position, controller.investigateTarget.transform.position) > controller.enemyData.investigateDistance)
        {
            timer = Time.time;
        }
        if (Time.time - timer > controller.enemyData.investigateTimer)
        {
            controller.investigateTarget = null;
            return true;
        }
        else
        {
            return false;
        }
    }
}
