using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pluggable AI/State")]
public class State : ScriptableObject {

    public Action[] actions;
    public Transition[] transitions;

    public float aiSpeed; // Max speed of AI in this state for use with animation controller

    public Color sceneGizmoColor = Color.gray;

    public void UpdateState(StateController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    private void DoActions(StateController controller)
    {
        for(int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }

    private void CheckTransitions(StateController controller)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool decisionSuceeded = transitions[i].decision.Decide(controller);
            if (decisionSuceeded)
            {
                controller.TransitionToState(transitions[i].trueState);
            }
            else
            {
                controller.TransitionToState(transitions[i].falseState);
            }
        }
    }
}
