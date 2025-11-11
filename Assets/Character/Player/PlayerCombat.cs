using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : CharacterCombat
{

    public void SetupComponents()
    {
		//Get Components
		attackHandler = GetComponent<PlayerAttackController>();
		controller = GetComponent<PlayerController>();
		health = GetComponent<PlayerHealth>();

		//Blood System
		blood = Component.FindFirstObjectByType<BloodSystem>();

		//Only Damage Objects with the EnemyObject Tag
		DamageTag = "EnemyObject";

	}
    
    // Start is called before the first frame update
    void Start()
    {
		//Add Swings
		SwingList.Clear();
		SwingList.Add(new HitPositionSwing(new Vector2(-2, 0.5f), new Vector2(15, 2), 0.2f, 0.45f));
		SwingList.Add(new HitPositionSwing(new Vector2(-0.85f, 0.5f), new Vector2(12, 0), 0.1f, 0.3f));
		SwingList.Add(new HitPositionSwing(new Vector2(-8, 0.2f), new Vector2(23, 0), 0.5f, 0.85f));

	}

	// Update is called once per frame
	void Update()
    {
		HandleHitTimer();
        UpdateHitPosition();
		CollisionCheck();
    }
}
