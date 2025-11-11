using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterCombat : MonoBehaviour
{

	public struct HitPositionSwing
	{
		public HitPositionSwing(Vector2 arg_StartPos, Vector2 arg_Speed, float arg_Delay, float arg_End)
		{
			StartPos = arg_StartPos;
			Speed = arg_Speed;
			Delay = arg_Delay;
			End = arg_End;
		}

		public Vector2 StartPos;
		public Vector2 Speed;
		public float Delay;
		public float End;
	}

	protected BloodSystem blood;

	protected CharacterController controller;
	[SerializeField] protected CharacterAttackHandler attackHandler;
	protected CharacterHealth health;

	//Hitposition Data
	protected Vector2 HitPosition;
	//[SerializeField] protected Vector2[] SwingStartPos = new Vector2[3];
	//[SerializeField] protected Vector2[] SwingSpeed = new Vector2[3];
	//[SerializeField] protected float[] SwingDelay = new float[3];
	//[SerializeField] protected float[] SwingEnd = new float[3];


	[SerializeField] public List<HitPositionSwing> SwingList = new List<HitPositionSwing>();
	public string DamageTag = "None";


	float HealthGainedOnMop = 5;

	//Getters
	public Vector2 GetHitPosition() { return HitPosition; }

	//Get Position of the HitPoint Based on Attack Data
	protected void UpdateHitPosition()
	{
		if (attackHandler.InAttack)
		{
			float flip = 1;
			if (controller.facing == Direction.Left)
				flip = -1;

			//Get Player Position
			HitPosition = transform.position;


			HitPosition.x += SwingList[attackHandler.CurrentAttack].StartPos.x * flip;
			HitPosition.y += SwingList[attackHandler.CurrentAttack].StartPos.y;

			//Add Swing Speed
			if (attackHandler.AttackTimer > (SwingList[attackHandler.CurrentAttack].Delay))
			{
				if (attackHandler.AttackTimer < SwingList[attackHandler.CurrentAttack].End)
				{
					HitPosition.x += attackHandler.AttackTimer * (1 / attackHandler.GetAttackDuration(attackHandler.CurrentAttack)) * SwingList[attackHandler.CurrentAttack].Speed.x * flip;
					HitPosition.y += attackHandler.AttackTimer * (1 / attackHandler.GetAttackDuration(attackHandler.CurrentAttack)) * SwingList[attackHandler.CurrentAttack].Speed.y;
				}
			}
		}

	}


	/// <summary>
	/// Collision Data and Functions
	/// </summary>
	/// <returns></returns>
	 

	//Objects that have already been hit this swing
	List<GameObject> AlreadyHit = new List<GameObject>();
	bool HitCollision = false;
	float HitCollisionTimer = 0;
	float HitCollisionDuration = 0.1f;
	public bool GetHitCollision() { return HitCollision; }


	public void ClearAlreadyHitList() { AlreadyHit.Clear(); }
	

	//Returns a list of all the gameobjects in the scene, currently colliding with the HitPosition
	List<GameObject> CurrentlyCollidingWith()
	{
		GetComponent<CircleCollider2D>().enabled = GetComponent<CharacterAttackHandler>().InAttack;

		//Return List
		List<GameObject> collisions = new List<GameObject>();

		//All Colliders in the scene
		Collider2D[] colliders = FindObjectsByType<Collider2D>(FindObjectsSortMode.None);

		for (int i = 0; i < colliders.Length; i++)
		{
			int withTag = 0;
			if (colliders[i].tag == DamageTag)
			{
				withTag++;


				if (colliders[i].IsTouching(GetComponent<CircleCollider2D>()))
				{
					collisions.Add(colliders[i].gameObject);
				}

			}
		}

		return collisions;

	}


	//Check For collision
	protected bool CollisionCheck()
	{
		List<GameObject> collisions = CurrentlyCollidingWith();
		for (int i = 0; i < collisions.Count; i++)
		{
			//If the object appears on the List of Objects already hit then ignore the collision
			bool object_already_hit = false;
			for (int j = 0; j < AlreadyHit.Count; j++)
			{
				if (collisions[i] == AlreadyHit[j])
				{

					object_already_hit = true;
				}
			}
			//If Object Has not already been hit then run hit func
			if (object_already_hit == false)
			{
				
				Hit(collisions[i]);
				bool enemy_alive = collisions[i].gameObject.GetComponent<CharacterHealth>().CurrentHealth() <= 0;
				//Debug.Log(enemy_alive);
				Vector3 collision_pos = transform.position;
				collision_pos.x += GetComponent<CircleCollider2D>().offset.x;
				collision_pos.y += GetComponent<CircleCollider2D>().offset.y;
				Component.FindFirstObjectByType<PunchEffectsManager>().SpawnEffect(collision_pos, GetComponent<CharacterController>().facing, enemy_alive);
				Debug.Log("Every Collision");
				return true;
			}
		}
		return false;

		/*
		string already_hit_size_message = "Already Hit Size " + AlreadyHit.Count;
		Debug.Log(already_hit_size_message);
		*/
	
	}

	float GetHealthDamage() { return attackHandler.GetHealthDamage(attackHandler.CurrentAttack); }
	float GetStanceDamage() { return attackHandler.GetStanceDamage(attackHandler.CurrentAttack); }

	void IncreaseBlood()
	{
		blood.AddBlood(1);
	}

	void Hit(GameObject arg_object)
	{
		
		AlreadyHit.Add(arg_object);
		HitCollision = true;
		HitCollisionTimer= 0;
		
		arg_object.GetComponent<CharacterHealth>().DamageHealth(GetHealthDamage());
		arg_object.GetComponent<CharacterHealth>().DamageStance(GetStanceDamage());
		
		//If the Hit Object is an enemy then move to hit animation
		EnemyComponent isEnemy = arg_object.GetComponent<EnemyComponent>();
		if(isEnemy != null)
		{
			arg_object.GetComponent<CharacterController>().SetState(State.Hit);
		}

		
		
		//If the Blood Power Is Active then Add Health
		if (Component.FindFirstObjectByType<PowerUp>().IsActive())
		{
			GetComponent<CharacterHealth>().IncreaseHealth(HealthGainedOnMop);
			GetComponent<CharacterHealth>().IncreaseStance(HealthGainedOnMop);
		}

		//Increase the amount of blood on the floor
		IncreaseBlood();

	}

	public void HandleHitTimer()
	{
		HitCollisionTimer += Time.deltaTime;
		if(HitCollisionTimer > HitCollisionDuration)
		{
			HitCollision = false;
		}
		//Debug.Log(HitCollision.ToString());
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
	
