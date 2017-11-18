using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimController : MonoBehaviour {

    public Animator anim;
    public StateController stateController;

    private void FixedUpdate()
    {
        if (stateController.currentState.aiSpeed != 0)
            anim.SetFloat("Speed", (stateController.navMeshAgent.velocity.magnitude / stateController.enemyData.sprintSpeed));
        else
            anim.SetFloat("Speed", 0);
    }
}
