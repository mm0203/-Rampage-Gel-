using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishAnimSkullDragon : StateMachineBehaviour
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.SetBool("Finish", false);
    }
}
