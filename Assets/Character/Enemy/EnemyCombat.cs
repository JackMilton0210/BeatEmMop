using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : CharacterCombat
{
    public void SetupComponents()
    {
        controller = GetComponent<EnemyController>();
		attackHandler = GetComponent<EnemyAttackHandler>();
        health = GetComponent<EnemyHealth>();

		SwingList.Clear();
		SwingList.Add(new HitPositionSwing(new Vector2(-2, 0.5f), new Vector2(15, 2), 0.2f, 0.45f));
		SwingList.Add(new HitPositionSwing(new Vector2(-0.85f, 0.5f), new Vector2(12, 0), 0.1f, 0.3f));

        DamageTag = "PlayerObject";
	}


    // Start is called before the first frame update
    void Start()
    {

        
	}

    // Update is called once per frame
    void Update()
    {
        HandleHitTimer();
        UpdateHitPosition();
		if (CollisionCheck())
			Debug.Log("Enemy Hit Something");
	}
}
