using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable AI/Decision/Active State")]
public class ActiveStateDecision : Decision {

    public override bool Decide(StateController controller)
    {
        bool chaseTargetActive = controller.chaseTarget.gameObject.activeSelf;
        return chaseTargetActive;
    }
}
