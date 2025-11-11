using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerInputHandler : CharacterInput
{

    public enum InputMode { Any, MouseKeyboard, GamePad };
    [SerializeField] public InputMode inputMode = InputMode.Any;

    private void GetInput_Both()
    {
		IN.movement_up = Input.GetKey(KeyCode.W) || Input.GetAxis("Vertical") > 0;
		IN.movement_down = Input.GetKey(KeyCode.S) || Input.GetAxis("Vertical") < 0;
		IN.movement_left = Input.GetKey(KeyCode.A) || Input.GetAxis("Horizontal") < 0;
		IN.movement_right = Input.GetKey(KeyCode.D) || Input.GetAxis("Horizontal") > 0;

		IN.attack = Input.GetKey(KeyCode.Space) || Input.GetKey("joystick 1 button 0");
		IN.mop = Input.GetKey(KeyCode.E) || Input.GetKey("joystick 1 button 3");
	}
    private void GetInput_MouseKeyboard()
    {
		IN.movement_up = Input.GetKey(KeyCode.W);
		IN.movement_down = Input.GetKey(KeyCode.S);
		IN.movement_left = Input.GetKey(KeyCode.A);
		IN.movement_right = Input.GetKey(KeyCode.D);

		IN.attack = Input.GetKey(KeyCode.Space);
		IN.mop = Input.GetKey(KeyCode.E);
	}
    private void GetInput_Gamepad()
    {
		IN.movement_up = Input.GetAxis("Vertical") > 0;
		IN.movement_down = Input.GetAxis("Vertical") < 0;
		IN.movement_left = Input.GetAxis("Horizontal") < 0;
		IN.movement_right = Input.GetAxis("Horizontal") > 0;

		IN.attack = Input.GetKey("joystick 1 button 0");
		IN.mop = Input.GetKey("joystick 1 button 3");
	}
    protected override void GetInput()
    {
        if (inputMode == InputMode.Any)             GetInput_Both();
        if (inputMode == InputMode.MouseKeyboard)   GetInput_MouseKeyboard();
        if (inputMode == InputMode.GamePad)         GetInput_Gamepad();
    }

	public void SetupComponents()
	{
		attackHandler = GetComponent<PlayerAttackController>();
		controller = GetComponent<PlayerController>();
	}

    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
		GetInput();
		HandleInput();
		HandleState();
	}
}
