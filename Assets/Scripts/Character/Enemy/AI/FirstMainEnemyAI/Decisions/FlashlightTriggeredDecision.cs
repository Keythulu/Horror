using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable AI/Decision/Flashlight Triggered")]
public class FlashlightTriggeredDecision : Decision {

    public override bool Decide(StateController controller)
    {
        bool enemyTriggered = CheckTimeInFlashlight(controller);
        return enemyTriggered;
    }

    private bool CheckTimeInFlashlight(StateController controller)
    {
        if (controller.currentTimeInFlashlight > controller.enemyData.timeInFlashLightToTrigger)
        {
            if (controller.flashlightHoldingPlayer)
            {
                controller.chaseTarget = controller.flashlightHoldingPlayer.transform;
                return true;
            }
        }
        return false;
    }
}
