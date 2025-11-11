using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public enum State { Idle, Walk, Attack, Mop, Knocked, Hit }
public enum VerticalMove { None, Up, Down };
public enum HorizontalMove { None, Left, Right };
public enum Direction { Left, Right };
public enum KnockState { Falling, Lying, Standing }

public class CharacterController : MonoBehaviour
{
	//Current State
	public State CurrentState = State.Idle;
	public Direction facing;
	public KnockState CurrentKnockState = KnockState.Falling;
	float FallingTimer = 0;
	float FallingDuration = 0.5f;
	float LyingTimer = 0;
	float LyingDuration = 0.8f;
	float StandingTimer = 0;
	float StandingDuration = 0.5f;
	float FallingXSpeed_Start = 3;
	float FallingXSpeed_End = 10;

	//Movement
	public HorizontalMove CurrentHorizontal;
	public VerticalMove CurrentVertical;
	[SerializeField] public Vector2 MovementSpeed = Vector2.one;

	//Timers
	//[InspectorLabel("Timers")]
	[SerializeField] public float MopDuration = 1.5f;
	protected float MopTimer = 0;

	//Hit Timer
	float HitTimer = 0;
	float HitDuration = 0.3f;

	//Getters
	public State GetCurrentState() { return CurrentState; }

	//Boundary Feelers
	Vector2 NorthFeeler = new Vector2(0, 0.2f);
	Vector2 SouthFeeler = new Vector2(0, -1);
	Vector2 WestFeeler = new Vector2(-0.2f, 0);
	Vector2 EastFeeler = new Vector2(0.2f, 0);

	//Block the character from making attacks
	protected bool BlockAttack = false;


	//Particle Spawner
	int BloodToSpawn = 3;
	int BloodSpawned = 0;
	float BloodSpawnTimer = 0;
	float BloodSpawnDuration = 0.2f;

	bool FeelerCheck(Vector2 _point, List<BoxCollider2D> _bounds)
	{
		for (int i = 0; i < _bounds.Count; i++) {
			if (_bounds[i].OverlapPoint(_point))
				return true;
		}
		return false;
	}


	struct BoundaryStatus
	{
		bool north;
		bool south;
		bool east;
		bool west;
		bool any;

		public void AllFalse() { north = false;south = false; east = false; west = false; any = false; }
		public void SetNorth(bool value) { north = value; }
		public void SetSouth(bool value) { south = value; }
		public void SetEast(bool value) { east = value; }
		public void SetWest(bool value) { west = value; }
		public bool GetNorth() { return north; }
		public bool GetSouth() { return south; }
		public bool GetEast() {  return east; }
		public bool GetWest() { return west; }

		public void UpdateAny()
		{
			any = false;
			if(north) { any = true; return; }
			if(south) { any = true; return; }
			if(east) { any = true; return; }
			if(west) { any = true; return; }
		}
	}
	BoundaryStatus CurrentBounds;

	void BoundaryCheck()
	{
		//Set all to false
		CurrentBounds.AllFalse();

		//Get all the LevelBounds
		List<BoxCollider2D> _bounds = Component.FindFirstObjectByType<LevelBoundManager>().GetBounds();
		//Character Position
		Vector2 _char_pos = new Vector2(transform.position.x, transform.position.y);

		//If Boundary to the north then bound north is true
		CurrentBounds.SetNorth(FeelerCheck(_char_pos + NorthFeeler, _bounds));

		//If boundary to the south then bound south is true
		CurrentBounds.SetSouth(FeelerCheck(_char_pos + SouthFeeler, _bounds));

		//If boundary to the east then bound east is true
		CurrentBounds.SetEast(FeelerCheck(_char_pos + EastFeeler, _bounds));

		//If Boundary to the west then bound west is true
		CurrentBounds.SetWest(FeelerCheck(_char_pos + WestFeeler, _bounds));


		//________TESTING
		if (Component.FindFirstObjectByType<DevOptions>().GetArrowBlockers())
		{
			if (Input.GetKey(KeyCode.UpArrow)) CurrentBounds.SetNorth(true);
			if(Input.GetKey(KeyCode.DownArrow)) CurrentBounds.SetSouth(true);
			if(Input.GetKey(KeyCode.LeftArrow))	CurrentBounds.SetWest(true);
			if(Input.GetKey(KeyCode.RightArrow)) CurrentBounds.SetEast(true);
		}

		//Update Any Bounds
		CurrentBounds.UpdateAny();
	}

	void BoundaryBlock()
	{
		if(CurrentState == State.Walk)
		{
			HorizontalMove _horz = CurrentHorizontal;
			VerticalMove _vert = CurrentVertical;
			BoundaryStatus _bounds = CurrentBounds;

			//Handle Vertical
			if(_vert == VerticalMove.Up)
				if (_bounds.GetNorth())
					_vert = VerticalMove.None;
			if (_vert == VerticalMove.Down)
				if (_bounds.GetSouth())
					_vert = VerticalMove.None;

			//Handle horizontal
			if (_horz == HorizontalMove.Left)
				if (_bounds.GetWest())
					_horz = HorizontalMove.None;
			if (_horz == HorizontalMove.Right)
				if (_bounds.GetEast())
					_horz = HorizontalMove.None;

			CurrentHorizontal = _horz;
			CurrentVertical = _vert;

		}
	}


	//Return True If Walking
	public bool IsWalking()
	{
		if (CurrentState == State.Walk)
			return true;
		return false;
	}

	//returns true is the controller is mopping
	public bool IsMopping()
	{
		if (CurrentState == State.Mop)
			return true;
		return false;
	}


	//Returns True if the Desired State can be swapped to from the current state
	public bool CanSwap(State swap_state)
	{
		switch (swap_state)
		{
			case State.Idle:
				{
					return true;
				}

			case State.Walk:
				{
					if (CurrentState == State.Idle) return true;
					return false;
				}

			case State.Attack:
				{
					if (BlockAttack) return false;
					if (CurrentState == State.Idle) return true;
					if (CurrentState == State.Walk) return true;
					return false;
				}

			case State.Mop:
				{
					if (CurrentState == State.Idle) return true;
					if (CurrentState == State.Walk) return true;
					return false;
				}
			case State.Knocked:
				{
					return true;
				}
			case State.Hit:
				{
					return true;
				}
		}

		return false;
	}
	//Swaps to the desired state
	public void SetState(State swap_state)
	{
		if (CanSwap(swap_state))
		{
			CurrentState = swap_state;
		}
	}
	//List of States that are allowed to swap character direction
	private bool CanSwapDirection(State arg_state)
	{
		switch (arg_state)
		{
			case State.Idle: return true;
			case State.Walk: return true;
		}

		return false;
	}

	//Flip Direction of the sprite
	protected void HandleDirection()
	{
		if (CanSwapDirection(CurrentState))
		{
			Vector3 _current_scale = transform.localScale;
			float _mag = Mathf.Sqrt(Mathf.Pow(transform.localScale.x,2));
			float _dir = 1;
			if (facing == Direction.Left)
			{
				_dir = -1;
			}
			_current_scale.x = _mag * _dir;
			transform.localScale = _current_scale;

		}
	}

	protected void HandleBounds()
	{

	}

	//Handle Character Movement
	protected void HandleMovement()
	{
		if (CurrentState == State.Walk)
		{
			BoundaryCheck();
			BoundaryBlock();


			//Create Movement Vector
			Vector3 movement_vector = Vector3.zero;

			//Apply Vertical Movement to Movement Vector
			if (CurrentVertical != VerticalMove.None)
			{
				if (CurrentVertical == VerticalMove.Up)
				{
					movement_vector.y += MovementSpeed.y * Time.deltaTime;
				}
				if (CurrentVertical == VerticalMove.Down)
				{
					movement_vector.y -= MovementSpeed.y * Time.deltaTime;
				}
			}

			//Apply Horizontal Movement to Movement Vector
			if (CurrentHorizontal != HorizontalMove.None)
			{
				if (CurrentHorizontal == HorizontalMove.Left)
				{
					movement_vector.x -= MovementSpeed.x * Time.deltaTime;
				}
				else if (CurrentHorizontal == HorizontalMove.Right)
				{
					movement_vector.x += MovementSpeed.x * Time.deltaTime;
				}
			}

			//Apply Movement Vector to the current Position
			transform.position = transform.position + movement_vector;
		}
	}
	//Handle Chacter Attack
	protected void HandleAttack()
	{
		if (CurrentState == State.Attack)
		{
			if (GetComponent<CharacterAttackHandler>().InAttack == false)
			{
				CurrentState = State.Idle;
			}

			//If this is a enemy character
			EnemyComponent _enemy= GetComponent<EnemyComponent>();
			if(_enemy)
			if (GetComponent<CharacterAnimationController>().AnimFinished())
			{
				CurrentState = State.Idle;
			}
		}
	}
	void SpawnBloodParticles()
	{

		SFX_BloodParticleManager _particle_spawner = Component.FindFirstObjectByType<SFX_BloodParticleManager>();

		float _spawn_range = 0;
		float _target_range = 0;
		float _move_variance = 0;

		Vector3 _rand = new Vector3(Random.Range(-_spawn_range, _spawn_range), Random.Range(-_spawn_range, _spawn_range), 0);
		Vector3 _target_rand = new Vector3(Random.Range(0, _target_range), 0, 0);
		float _move_speed = Random.Range(-_move_variance, _move_variance);

		SFX_BloodParticle _particle = _particle_spawner.Spawn(transform.position + _rand).GetComponent<SFX_BloodParticle>();
		_particle.TargetOffset = _target_rand;
		_particle.MoveSpeed += _move_speed;

	}

	//Handle Mopping
	protected void HandleMop()
	{
		if (CurrentState == State.Mop)
		{
			if (Component.FindFirstObjectByType<BloodSystem>().UncollectedBloodPercentage() != 0)
			{
				if (BloodSpawned < BloodToSpawn)
				{
					BloodSpawnTimer += Time.deltaTime;
					if (BloodSpawnTimer >= BloodSpawnDuration)
					{
						SpawnBloodParticles();
						BloodSpawned++;
						BloodSpawnTimer = 0;

					}

				}
			}

			MopTimer += Time.deltaTime;
			if (MopTimer >= MopDuration)
			{
				//Transfer Blood Resources
				Component.FindFirstObjectByType<BloodSystem>().MopBlood(4);

				//Reset Mop Timer
				MopTimer = 0;

				//Move To Idle State
				CurrentState = State.Idle;

				//Reset Blood Spawner
				BloodSpawned = 0;

				
			}
		}
	}

	bool isPlayer()
	{
		if (gameObject.tag == "PlayerObject")
			return true;
		return false;
	}

	void KnockStateFalling()
	{
		FallingTimer += Time.deltaTime;
		if (FallingTimer >= FallingDuration)
		{
			FallingTimer = 0;
			CurrentKnockState = KnockState.Lying;
		}

		//Move Character backwards along their X
		{
			
			float d = -1;
			if (facing == Direction.Right)
				d = 1;
			if(isPlayer())
				d *= -1;
			
			float n = FallingTimer / FallingDuration;
			
			float FallSpeed = d * EasingFunctions.ApplyEase(EasingFunctions.Ease.Linear, FallingXSpeed_Start, FallingXSpeed_End, n);
			Vector3 new_pos = transform.position + new Vector3(FallSpeed * Time.deltaTime, 0, 0);
			transform.position = new_pos;
		}
			
	}
	void KnockStateLying()
	{
		LyingTimer += Time.deltaTime;
		if(LyingTimer >= LyingDuration)
		{
			LyingTimer = 0;
			CurrentKnockState = KnockState.Standing;
		}
	}
	void KnockStateStanding()
	{
		StandingTimer += Time.deltaTime;
		if(StandingTimer >= StandingDuration)
		{
			StandingTimer = 0;
			CurrentKnockState = KnockState.Falling;
			CurrentState = State.Idle;
		}
	}
	public bool IsKnocked()
	{
		if (CurrentState == State.Knocked)
			return true;
		return false;
	}
	public bool IsKnockStateStandingUp()
	{
		if (CurrentState == State.Knocked)
			if (CurrentKnockState == KnockState.Standing)
				return true;
		return false;
	}

	//Handle Knock
	protected void HandleKnock()
	{
		if(CurrentState == State.Knocked) { 

			switch (CurrentKnockState)
			{
				case KnockState.Falling:
					{
						KnockStateFalling();
						break;
					}
				case KnockState.Lying:
					{
						KnockStateLying();
						break;
					}
				case KnockState.Standing:
					{
						KnockStateStanding();
						break;
					}
			}


		}
	}

	protected void HandleHit()
	{
		if(CurrentState == State.Hit)
		{
			HitTimer += Time.deltaTime;
			if(HitTimer >= HitDuration)
			{
				HitTimer = 0;
				SetState(State.Idle);
			}
		}
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
