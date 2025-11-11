using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

public class MovementController : MonoBehaviour
{
	//Private Vars
	[HideInInspector] public bool moving_left;
	[HideInInspector] public bool moving_right;
	[HideInInspector] public bool moving_up;
	[HideInInspector] public bool moving_down;


    [HideInInspector] public bool Moving;


	//Changeable
	public float VerticalSpeed = 2;
	public float HorizontalSpeed = 3;

	[HideInInspector] public float SpeedModifier = 1;

	//Audio Guy Was Here
	[Header("Wwise Events")]
	//public AK.Wwise.Event myFootstep;

	private bool footstepIsPlaying = false;
	private float lastFootstepTime = 0f;

	float HorizontalMovement(float x, bool left, bool right)
	{

		float new_x = 0;


		if (right)
		{
			new_x += HorizontalSpeed * Time.deltaTime;

		}
		if (left)
		{
			new_x -= HorizontalSpeed * Time.deltaTime;

		}

		new_x *= SpeedModifier;

		return new_x + x;
	}
  
    float VerticalMovement(float y, bool up, bool down)
    {
        float new_y = 0;
        if (up)
        {
            new_y += VerticalSpeed * Time.deltaTime;
        }
        else if (down)
        {
            new_y -= VerticalSpeed * Time.deltaTime;
        }
        new_y *= SpeedModifier;
        return new_y + y;
    }
    void HandleMovement()
    {
        //Get Moving Bool
        if ((!moving_down && !moving_up) && (!moving_left) && (!moving_right))
            Moving = false;
        else
            Moving = true;


        Vector3 pos = transform.position;

		pos.x = HorizontalMovement(pos.x, moving_left, moving_right);
		pos.y = VerticalMovement(pos.y, moving_up, moving_down);
		transform.position = pos;


        //Play Audio
        if ((moving_down) || (moving_up) || (moving_right) || (moving_left))
        {
            if (!footstepIsPlaying)
            {
                //myFootstep.Post(gameObject);
                lastFootstepTime = Time.time;
                footstepIsPlaying = true;
            }
            else
            {
                if (VerticalSpeed > 1 || HorizontalSpeed > 1)
                {
                    if (Time.time - lastFootstepTime > 90 * Time.deltaTime)
                    {
                        footstepIsPlaying = false;
                    }
                }
            }
        }

	}

	// Start is called before the first frame update
	void Awake()
	{
		lastFootstepTime = Time.time;
	}

	private void Update()
	{
		HandleMovement();

	}
}