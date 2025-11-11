using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AWW_MopSwing : AudioWwise
{
	//AttackComponent
	[SerializeField] PlayerAttackController attackComponent;

	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		//StartCondition = Component.FindFirstObjectByType<PlayerAttackController>().InAttack;
		PlayerAttackController atk = Component.FindFirstObjectByType<PlayerAttackController>();
		if (atk.InAttack)
		{
			if (atk.AttackTimer < 0.1f)
				StartCondition = true;
			else
				StartCondition = false;
		}
		else
		{
			StartCondition = false;
		}
		AudioUpdate();

	}
}
