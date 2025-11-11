using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;

public class EnemyComponent : MonoBehaviour
{
	[SerializeField] RuntimeAnimatorController EnemyAnimator;

	void AddComponents()
	{
		gameObject.AddComponent<EnemyController>();
		gameObject.AddComponent<EnemyInputHandler>();
		gameObject.AddComponent<EnemyCombat>();
		gameObject.AddComponent<EnemyAttackHandler>();
		gameObject.AddComponent<EnemyAnimationController>();
		gameObject.AddComponent<Animator>();
		gameObject.AddComponent<EnemyHealth>();
		gameObject.AddComponent<CircleCollider2D>();
	}

	public void SetupComponents()
	{
		GetComponent<EnemyInputHandler>().SetupComponents();
		GetComponent<EnemyCombat>().SetupComponents();
		GetComponent<EnemyAttackHandler>().SetupComponents();
		GetComponent<EnemyAnimationController>().SetupComponents();

		GetComponent<EnemyHealth>().SetupComponents();
		GetComponent<EnemyHealth>().Begin(100, 100);

		GetComponent<CircleCollider2D>().radius = 0.1f;
		GetComponent<Animator>().runtimeAnimatorController = Component.FindFirstObjectByType<EnemyHandler>().GetAnimatorController();

	}

	// Start is called before the first frame update
	void Start()
	{
		AddComponents();
		SetupComponents();

	}

	// Update is called once per frame
	void Update()
	{

	}
}
