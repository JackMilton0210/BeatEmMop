using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;

public class EnvironmentThrowable : MonoBehaviour
{

    bool Finished = false;
    bool IsFinished() { return Finished; }
    
    int CollidedWith = 0;
	[SerializeField] int NumberOfCollisions = 2;

    //CollisionTimer
    [SerializeField] bool Collided = false;
    [SerializeField] float CollisionTimer = 0;
    float CollisionDuration = 0.1f;
    public bool EnemyHitByThrowable() { return Collided; }

    //MovingTimer
    float MovingTimer = 0;
    float MovingDuration = 1;

    public enum ThrowableState { Idle, Activated, Moving, Collided, Offscreen }
    public ThrowableState CurrentState;
    [SerializeField] ThrowableState DisplayCurrentState;

    public bool IsOffscreen() { if (CurrentState == ThrowableState.Offscreen) return true; return false; }

    string DamageTag = "EnemyObject";
    string ActivateTag = "PlayerObject";

    float ThrowableMoveDirection = 1;
    Vector2 ThrowableMoveSpeed = new Vector2(20, 1);
    float ThrowableGravity = 5;
    float ThrowableSpinSpeed = 100;

    float ThrowableHealthDamage = 40;
    float ThrowableStanceDamage = 100;


    public bool IsMoving() { if (CurrentState == ThrowableState.Moving) return true; return false; }


    /// <summary>
    /// Base Functions
    /// </summary>  
    void HandleMovement()
    {
		//Get Current Transform
		Vector3 _current_pos = transform.position;
		Vector3 _current_rot = transform.rotation.eulerAngles;
		//Apply Speed
		_current_pos.x += ThrowableMoveSpeed.x * ThrowableMoveDirection* Time.deltaTime;
		_current_pos.y += ThrowableMoveSpeed.y * Time.deltaTime;
		_current_rot.z += ThrowableSpinSpeed * -ThrowableMoveDirection * Time.deltaTime;
        //Apply Gravity
        ThrowableMoveSpeed.y -= ThrowableGravity * Time.deltaTime;
		//Apply New Transform
		transform.position = _current_pos;
		transform.rotation = Quaternion.Euler(_current_rot);

        MovingTimer += Time.deltaTime;
        if(MovingTimer >= MovingDuration)
        {
            MovingTimer = 0;
            CurrentState = ThrowableState.Offscreen;
        }
	}
    void HandleCollision()
    {
        //If reached max number of collisions
        if(CollidedWith >= NumberOfCollisions)
        {
            CurrentState = ThrowableState.Collided;
        }

        //HandleCollisionTimer
        if (Collided == true)
        {
            CollisionTimer += Time.deltaTime;
            if(CollisionTimer >= CollisionDuration)
            {
                CollisionTimer = 0;
                Collided = false;
            }
        }

    }
	void Hit(GameObject collision_object)
    {
		Collided = true;
		CollisionTimer = 0;
        CollidedWith++;
		collision_object.GetComponent<EnemyHealth>().DamageHealth(ThrowableHealthDamage);
        collision_object.GetComponent<EnemyHealth>().DamageStance(ThrowableStanceDamage);
    }

	private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Throwable Collision Enter");

		
		if (CurrentState == ThrowableState.Moving)
        {
            if(collision.gameObject.tag == DamageTag)
            {
                Hit(collision.gameObject);
			}
        }
    }

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (CurrentState == ThrowableState.Idle)
		{
			if (collision.gameObject.tag == ActivateTag)
			{
				if (collision.gameObject.GetComponent<PlayerController>().GetCurrentState() == State.Attack)
				{
					CurrentState = ThrowableState.Moving;
					ThrowableMoveDirection = 1;
					if (collision.gameObject.GetComponent<PlayerController>().facing == Direction.Left)
						ThrowableMoveDirection = -1;
				}
			}
		}
	}


	// Start is called before the first frame update
	void Start()
    {
        
    }

    /// <summary>
    /// State Functions
    /// </summary>
    void ThrowableIdle() { }
    void ThrowableActivated() {
        CurrentState = ThrowableState.Moving;
    }
    void ThrowableMoving()
    {
        HandleMovement();
        HandleCollision();

    }
    void ThrowableCollided() {
        Finished = true;
    }
    void ThrowableOffscreen() {
        Finished = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (CurrentState)
        {
            case ThrowableState.Idle: { ThrowableIdle();  break; };
            case ThrowableState.Activated: { ThrowableActivated();  break; };
            case ThrowableState.Moving: { ThrowableMoving();  break; }
            case ThrowableState.Collided: { ThrowableCollided();  break; };
            case ThrowableState.Offscreen: { ThrowableOffscreen();  break; }
        }


        DisplayCurrentState = CurrentState;

    }
}
