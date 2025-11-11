using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{

    [SerializeField] RuntimeAnimatorController PlayerAnimator;

    void AddComponents()
    {
		gameObject.AddComponent<PlayerController>();
		gameObject.AddComponent<PlayerInputHandler>();
		gameObject.AddComponent<PlayerCombat>();
		gameObject.AddComponent<PlayerAttackController>();
		//gameObject.AddComponent<PlayerAnimationController>();
		//gameObject.AddComponent<Animator>();
        gameObject.AddComponent<PlayerHealth>();
	}

    void SetupComponents()
    {
        GetComponent<PlayerInputHandler>().SetupComponents();
        GetComponent<PlayerCombat>().SetupComponents();
        GetComponent<PlayerAnimationController>().SetupComponents();
        GetComponent<PlayerAttackController>().SetupComponents();
        //Animator animator = GetComponent<Animator>();
        //animator.runtimeAnimatorController = PlayerAnimator;
        GetComponent<PlayerController>().MovementSpeed = new Vector2(5, 3);

        GetComponent<PlayerHealth>().SetupComponents();
        GetComponent<PlayerHealth>().Begin(100, 100);

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
