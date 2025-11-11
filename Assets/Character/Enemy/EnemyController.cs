using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		HandleDirection();
		HandleMovement();
		HandleAttack();
        //HandleMop();
        HandleKnock();
        HandleHit();

        if (IsKnocked())
        {
            BlockAttack = true;
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CharacterHealth>().DamageHealth(1000);
            GetComponent<CharacterHealth>().DamageStance(1000);

            Destroy(GetComponent<CircleCollider2D>());
            Destroy(GetComponent<BoxCollider2D>());
        }
	}
}
