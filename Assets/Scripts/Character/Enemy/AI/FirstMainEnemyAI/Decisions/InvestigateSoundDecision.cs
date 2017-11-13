using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable AI/Decision/Investigate Sound")]
public class InvestigateSoundDecision : Decision {

    public override bool Decide(StateController controller)
    {
        return CheckSoundTarget(controller);
    }

    private bool CheckSoundTarget(StateController controller)
    {
        //Checks to see if the enemy has a target for investigation. This transform serves as a boolean which tells the enemy whether they can investigate.
        if (controller.investigateTarget)
        {
            return true;
        }
        else
            return false;
    }
}
