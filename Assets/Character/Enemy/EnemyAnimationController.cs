using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : CharacterAnimationController
{
    // Start is called before the first frame update
    void Start()
    {
        AddAnim(AnimationState.Idle, "EnemyIdle");
        AddAnim(AnimationState.Walk, "EnemyWalk");
        AddAnim(AnimationState.Attack1, "EnemyAttack1");
        //AddAnim(AnimationState.Attack2, "EnemyAttack2_FP");
        //AddAnim(AnimationState.Attack3, "EnemyAttack3_FP");
        AddAnim(AnimationState.Hit, "EnemyHit");
        AddAnim(AnimationState.Falling, "EnemyFall");
    }

    // Update is called once per frame
    void Update()
    {
        HandleAnimator();
    }
}
