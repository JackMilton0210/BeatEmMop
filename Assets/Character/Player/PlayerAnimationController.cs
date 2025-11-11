using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimationController : CharacterAnimationController
{
	

    // Start is called before the first frame update
    void Start()
    {
		AddAnim(AnimationState.Idle, "PlayerIdle");
        AddAnim(AnimationState.Walk, "PlayerWalk");
        AddAnim(AnimationState.Mop, "PlayerClean");
        AddAnim(AnimationState.Attack1, "PlayerAttack1");
        AddAnim(AnimationState.Attack2, "PlayerAttack2");
        AddAnim(AnimationState.Attack3, "PlayerAttack3");
        AddAnim(AnimationState.Falling, "PlayerFall");
	}

    // Update is called once per frame
    void Update()
    {
        HandleAnimator();
	}
}
