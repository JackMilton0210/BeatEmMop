using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackController : CharacterAttackHandler
{


    // Start is called before the first frame update
    void Start()
    {
		NumOfAttacks = 3;
        
        AttackDuration.Clear();
		AttackDuration.Add(0.75f);
        AttackDuration.Add(0.5f);
        AttackDuration.Add(1.05f);
        
        AttackQueueTime.Clear();
        AttackQueueTime.Add(0.5f);
        AttackQueueTime.Add(0.5f);
        AttackQueueTime.Add(0.3f);

        HealthDamage.Add(30);
        HealthDamage.Add(30);
        HealthDamage.Add(40);

        StanceDamage.Add(30);
        StanceDamage.Add(30);
        StanceDamage.Add(40);

    }

	public void SetupComponents()
	{
		combat = GetComponent<PlayerCombat>();
	}

	// Update is called once per frame
	void Update()
    {
        AttackUpdate();
	}
}
