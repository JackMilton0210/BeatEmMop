using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    public struct CharacterInputStruct
    {
		public bool movement_up;
		public bool movement_down;
		public bool movement_left;
		public bool movement_right;
		public bool attack;
		public bool mop;
	}



	//Components
	protected CharacterController controller;
	protected CharacterAttackHandler attackHandler;

	//User / AI Input
	public CharacterInputStruct IN;
	protected virtual void GetInput() { }


	protected void HandleInput()
	{
		//Up
		if (IN.movement_up)
		{
			controller.CurrentVertical = VerticalMove.Up;
		}
		//Down
		if (IN.movement_down)
		{
			controller.CurrentVertical = VerticalMove.Down;
		}
		if (!IN.movement_up && !IN.movement_down)
		{
			controller.CurrentVertical = VerticalMove.None;
		}

		//Left
		if (IN.movement_left)
		{
			controller.CurrentHorizontal = HorizontalMove.Left;
			controller.facing = Direction.Left;
		}
		//Right
		if (IN.movement_right)
		{
			controller.CurrentHorizontal = HorizontalMove.Right;
			controller.facing = Direction.Right;
		}
		if (!IN.movement_left && !IN.movement_right)
		{
			controller.CurrentHorizontal = HorizontalMove.None;
		}

		//Attack Input
		if (IN.attack)
		{
			attackHandler.AttackKeyPressed();
		}
	}
	protected void HandleState()
	{
		//Only Move to walking state if current state is idle
		if (IN.movement_up || IN.movement_down || IN.movement_left || IN.movement_right)
		{
			controller.SetState(State.Walk);
		}
		else
		{
			if (controller.CurrentState == State.Walk)
				controller.SetState(State.Idle);
		}

		//Can move to attack state from any position
		if (IN.attack)
		{
			controller.SetState(State.Attack);
		}

		//Can Mop from any state
		if (IN.mop)
		{
			controller.SetState(State.Mop);
		}
	}


	// Start is called before the first frame update
	void Start()
    {
		attackHandler = GetComponent<CharacterAttackHandler>();
    }

    // Update is called once per frame
    void Update()
    {
		
	}
}
