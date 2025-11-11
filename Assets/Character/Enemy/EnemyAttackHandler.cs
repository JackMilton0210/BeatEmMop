using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHandler : CharacterAttackHandler
{
    // Start is called before the first frame update
    void Start()
    {
        NumOfAttacks = 2;

		AttackDuration.Clear();
		AttackDuration.Add(0.9f);
		AttackDuration.Add(0.5f);

		AttackQueueTime.Clear();
		AttackQueueTime.Add(0.3f);
        AttackQueueTime.Add(0.2f);

		HealthDamage.Add(10);
		HealthDamage.Add(10);

		StanceDamage.Add(10);
		StanceDamage.Add(10);
	}

	public void SetupComponents()
	{
		combat = GetComponent<EnemyCombat>();
	}

	// Update is called once per frame
	void Update()
    {
        AttackUpdate();
    }
}
