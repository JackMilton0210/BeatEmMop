using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : CharacterController
{

	// Start is called before the first frame update
	void Start()
    {
        
    }

	void HandlePlayerStance() {
		if (!GetComponent<PlayerHealth>().IsAlive()) {
			GetComponent<PlayerHealth>().SetStanceToZero();
		}
	}

    // Update is called once per frame
    void Update()
    {
		HandleDirection();
		HandleMovement();
		HandleAttack();
		HandleMop();
		HandleKnock();
		HandleHit();

		HandlePlayerStance();
	}
}
