using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackHandler : MonoBehaviour
{
	public struct AttackDamage
	{
		public void Create(float arg_healthDamage, float arg_stanceDamage)
		{
			health_damage = arg_healthDamage;
			stance_damage = arg_stanceDamage;
		}
		float health_damage;
		float stance_damage;
	}

	public float HealthDamageMultiplier = 1;
	public float StanceDamageMultiplier = 1;

	//Objects and Components
	public CharacterCombat combat;
   
	//State Handling
	public bool InAttack = false;
	public float AttackTimer = 0;
	//Attack Data
	public int CurrentAttack = 0;
	protected int NumOfAttacks = 1;
	[SerializeField] protected List<float> AttackDuration = new List<float>();

	//Attack Queue
	protected float QueueAttackTimer = 0;
	[SerializeField] protected List<float> AttackQueueTime = new List<float>();

	//Damage List
	protected List<float> HealthDamage = new List<float> ();
	public float GetHealthDamage(int index) { return HealthDamage[index] * HealthDamageMultiplier; }
	protected List<float> StanceDamage = new List<float>();
	public float GetStanceDamage(int index) { return StanceDamage[index] * StanceDamageMultiplier; }

	//Getters
	public int GetCurrentAttack() { return CurrentAttack; }
	public float GetAttackDuration(int index) { return AttackDuration[index]; }


	void BeginAttack()
	{
		InAttack = true;
		AttackTimer = 0;
		combat.ClearAlreadyHitList();
	}
	void EndAttack()
	{
		InAttack = false;

		//Queue Next Attack
		if (QueueAttackTimer < AttackQueueTime[CurrentAttack])
		{
			CurrentAttack++;
			if (CurrentAttack > NumOfAttacks - 1)
				CurrentAttack = 0;
			BeginAttack();
		}
		else
		{
			CurrentAttack = 0;
		}

	}
	protected void AttackUpdate()
	{
		if (InAttack)
		{
			AttackTimer += Time.deltaTime;
			if (AttackTimer >= AttackDuration[CurrentAttack])
			{
				EndAttack();
			}
		}
		QueueAttackTimer += Time.deltaTime;

	}


	//Trigger On Key Press / AI Input recieved
	public void AttackKeyPressed()
	{
		if (!InAttack)
		{
			BeginAttack();
		}
		else
		{
			QueueAttackTimer = 0;
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
