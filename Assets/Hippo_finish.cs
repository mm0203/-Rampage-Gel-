using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hippo_finish : StateMachineBehaviour
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.SetBool("Finish", false);
    }
}
