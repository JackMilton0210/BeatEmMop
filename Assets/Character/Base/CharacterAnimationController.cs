using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CharacterAnimationController : MonoBehaviour
{
    public enum AnimationState { Idle, Walk, Attack1, Attack2, Attack3, Mop, Hit, Falling, Prone, GetUp };
    public AnimationState animationState = AnimationState.Idle;

    CharacterController controller;
    Animator animator;

    //List of the animations in attatched to the character
    [SerializeField] Dictionary<AnimationState, string> anims = new Dictionary<AnimationState, string>();
    //Add an animation to the character
    protected void AddAnim(AnimationState arg_trigger_state, string arg_name) { anims.Add(arg_trigger_state, arg_name); }
    //Returns the animation with the attached trigger state
    string GetAnimFromState(AnimationState arg_state) { return anims[arg_state]; }
    //Play the Current states animation
    void PlayAnim(AnimationState arg_state) { animator.Play(GetAnimFromState(arg_state)); }


    //Returns true when the animation has finished
    public bool AnimFinished()
    { 
        float length = animator.GetCurrentAnimatorStateInfo(0).length;
        float current_time = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if(current_time >= length)
            return true;
        return false;
    }

    //Modify the playback speed of the animation
    void SetAnimSpeed(float arg_sample, float arg_frames, bool arg_timed, float arg_duration)
    {
        float _sample = arg_sample;
        float _frames = arg_frames;
		bool _timed = arg_timed; 
        float _duration = arg_duration;

        //Set Speed Based on Vars
        float _new_speed = _frames / _sample;
        //Apply Duration Limit for timed animations
        if (_timed) _new_speed /= _duration;

        //Apply Speed
        animator.speed = _new_speed;
    }
    public void SetSpeed(float arg_duration)
    {
        SetAnimSpeed(24, animator.GetCurrentAnimatorStateInfo(0).length, true, arg_duration);
    }

    //State this frame and last frame
    AnimationState StateLastFrame = AnimationState.Walk;
    AnimationState StateThisFrame = AnimationState.Idle;
    //Returns true if the state this frame is different to the state last frame
    bool NewState() { if (StateThisFrame == StateLastFrame) return false;  return true; }

    AnimationState ControllerToAnimationState(State controllerState)
    {
        switch (controllerState)
        {
            case State.Idle:return AnimationState.Idle;
            case State.Walk:return AnimationState.Walk;
            case State.Hit:return AnimationState.Hit;
            case State.Mop:return AnimationState.Mop; 
            case State.Knocked:
                {
                    return AnimationState.Falling;
                }
            case State.Attack:
                {
                    if (GetComponent<CharacterAttackHandler>().CurrentAttack == 0)
                        return AnimationState.Attack1;
					if (GetComponent<CharacterAttackHandler>().CurrentAttack == 1)
						return AnimationState.Attack2;
					if (GetComponent<CharacterAttackHandler>().CurrentAttack == 2)
						return AnimationState.Attack3;
                    return AnimationState.Idle;
				}
        }
        return AnimationState.Idle;
    }

    //Plays the appropriate animation
    protected void HandleAnimator()
    {
        StateThisFrame = ControllerToAnimationState(controller.CurrentState);
        if (NewState())
        {
            PlayAnim(StateThisFrame);
        }

        StateLastFrame = StateThisFrame;

    }
    public void SetupComponents()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //HandleAnimator();   
    }
}
