using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepAnimSkullDragon : StateMachineBehaviour
{
    public float ftime = 3.0f;

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if(animatorStateInfo.normalizedTime > ftime)
        {
            animator.SetBool("Finish", true);
        }
    }
}
