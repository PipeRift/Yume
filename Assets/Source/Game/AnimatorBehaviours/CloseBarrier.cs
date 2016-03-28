using UnityEngine;
using System.Collections;

public class CloseBarrier : StateMachineBehaviour {
	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        ERockBarrier rockBarrier = animator.GetComponent<ERockBarrier>();
        if(rockBarrier)
            rockBarrier.Reset();
	}
}
