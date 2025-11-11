using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    MovementController movement;
    CombatController combat;

    void AssignComponents()
    {
		movement = GetComponent<MovementController>();
		if (!movement)
			Debug.Log("Error - PlayerInput::Start: No MovementController Component");

		combat = GetComponent<CombatController>();
		if (!combat)
			Debug.Log("Error - PlyaerInput::Start: No CombatController Component");
	}


    // Start is called before the first frame update
    void Start()
    {
        AssignComponents();
    }

    // Update is called once per frame
    void Update()
    {

        //Movment Inputs
        if (Input.GetKey(KeyCode.A)) movement.moving_left = true;
        else movement.moving_left = false;

        if (Input.GetKey(KeyCode.D)) movement.moving_right = true;
        else movement.moving_right = false;

        if (Input.GetKey(KeyCode.W)) movement.moving_up = true;
        else movement.moving_up = false;

        if(Input.GetKey(KeyCode.S)) movement.moving_down = true;
        else movement.moving_down = false;

        //Combat Inputs
        if (Input.GetKeyDown(KeyCode.Space)) combat.attack1 = true;
        combat.throw_button = Input.GetKey(KeyCode.Space);
        if(Input.GetKeyDown(KeyCode.R)) combat.mop = true;
    }
}
