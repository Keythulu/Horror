using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable AI/Decision/Continue Chase")]
public class ContinueChaseDecision : Decision {

    private float timer;

    public override bool Decide(StateController controller)
    {
        return ContinueChase(controller);
    }

    public bool ContinueChase(StateController controller)
    {
        RaycastHit hit;
        Debug.DrawRay(controller.gameObject.transform.position, (controller.chaseTarget.transform.position - controller.gameObject.transform.position) * controller.enemyData.lookRange, Color.red);
        if (Physics.Raycast(controller.gameObject.transform.position, (controller.chaseTarget.transform.position - controller.gameObject.transform.position).normalized, out hit, controller.enemyData.lookRange))
        {
            if (hit.collider.gameObject == controller.chaseTarget.gameObject)
            {
                timer = Time.time;
                return true;
            }
        }
        if (Time.time - timer > controller.enemyData.pursuitTimeout)
        {
            return false;
        }
        return true;
    }
}
