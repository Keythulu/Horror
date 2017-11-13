using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable AI/Decision/Player Spotted")]
public class PlayerSpottedDecision : Decision {

    private float timer;
    public override bool Decide(StateController controller)
    {
        return PlayerSpotted(controller);
    }
    private void OnEnable()
    {
        timer = Time.time;
    }
    private bool PlayerSpotted(StateController controller)
    {
        if (Time.time - timer > 1)
        {
            if (controller.sightRangeController.playersInRange.Count > 0)
            {
                foreach (GameObject player in controller.sightRangeController.playersInRange)
                {
                    if (Vector3.Angle(controller.aiEyes.transform.forward, player.transform.position) < 90)
                    {
                        float aiRevealAttempt = Random.Range(0, 99) + (Random.Range(0, 99) / 100);
                        //Debug.Log("AI Reveal: " + aiRevealAttempt);
                        //Debug.Log("Player chance to reveal: " + player.GetComponent<PlayerStealthManager>().currentChanceToReveal);
                        Debug.Log("Player Spotted: " + (aiRevealAttempt < player.GetComponent<PlayerStealthManager>().currentChanceToReveal));

                        if (aiRevealAttempt < player.GetComponent<PlayerStealthManager>().currentChanceToReveal)
                        {
                            Debug.Log("return true");
                            controller.chaseTarget = player.transform;
                            timer = Time.time;
                            return true;
                        }
                    }
                }
            }
            timer = Time.time;
        }
        return false;
    }
}
