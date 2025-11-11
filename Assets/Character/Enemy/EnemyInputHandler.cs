using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyInputHandler : CharacterInput
{
    //Behaviour Types
	public enum EnemyBehaviour { None, FacePlayer, ApproachPlayer, HalfApproachPlayer }
	[SerializeField] private EnemyBehaviour CurrentEnemyBehaviour;
	public void SetCurrentBehaviour(EnemyBehaviour arg_behaviour) { CurrentEnemyBehaviour = arg_behaviour; }

	//Player Object
	GameObject PlayerObject;

    //Movement Variables
    float AttackRange = 2.5f;
    float ApproachDistance = 2;
    float HalfApproachDistance = 6;

    //Attack CoolDown
    [SerializeField] float AttackCooldownTimer = 0;
    [SerializeField] float AttackCooldownDuration = 0.9f;
    [SerializeField] bool AttackCooldown = false;


    public void SetupComponents()
    {
		//Find Player Object
		//PlayerObject = GameObject.FindGameObjectWithTag("Player");
        //Component.FindFirstObjectByType<PlayerComponent>().gameObject;
        PlayerObject = Component.FindFirstObjectByType<PlayerComponent>().gameObject;

		//Find Components
		attackHandler = GetComponent<EnemyAttackHandler>();
		controller = GetComponent<EnemyController>();
	}

    // Start is called before the first frame update
    void Start()
    {
        
	}



    //Face the Player Object
    void FacePlayer(Vector3 arg_playerPosition, Vector3 arg_enemyPosition)
    {
        //Vars
        Vector3 _enemy_pos = arg_enemyPosition;
        Vector3 _player_pos = arg_playerPosition;

        //Calculated Vars
        float _distance = _player_pos.x - _enemy_pos.x;

        //Face Left
        if (_distance < 0) controller.facing = Direction.Left;
        //Face Right
        else controller.facing = Direction.Right;


	}
	void ApproachHorizontal(Vector3 _player, Vector3 _enemy, float _approach)
	{
		//Get Distance of player with and without direction
		float _distance = _player.x - _enemy.x;
		float _mag_distance = Mathf.Sqrt(Mathf.Pow(_distance, 2));

		//If player is within approach range - break
		if (_mag_distance < _approach)
		{
			IN.movement_left = false;
			IN.movement_right = false;
			return;
		}

		//If player is to the right of enemy - move right
		if (_distance < 0)
		{
			IN.movement_left = true;
			IN.movement_right = false;
		}
		//If player is to the left of enemy - move left
		else
		{
			IN.movement_left = false;
			IN.movement_right = true;
		}
	}

	void ApproachVertical(Vector3 _player, Vector3 _enemy, float _approach)
	{
		float _distance = _player.y - _enemy.y;
		float _mag_distance = Mathf.Sqrt(Mathf.Pow(_distance, 2));

		if (_mag_distance < _approach)
		{
			IN.movement_up = false;
			IN.movement_down = false;
			return;
		}
		else if (_distance < 0)
		{
			IN.movement_up = false;
			IN.movement_down = true;
		}
		else
		{
			IN.movement_up = true;
			IN.movement_down = false;
		}
	}

	//Behaviour Patterns
	void ApproachPlayer(Vector3 arg_playerPosition, Vector3 arg_enemyPosition, float arg_approachDistance)
	{

		//Get Vars
		Vector3 _enemy_pos = arg_enemyPosition;         //Get enemy Position
		Vector3 _player_pos = arg_playerPosition;       //Get Player Position
		float _approach_distance = arg_approachDistance;


		ApproachHorizontal(_player_pos, _enemy_pos, _approach_distance);
		ApproachVertical(_player_pos, _enemy_pos, _approach_distance/5);

	}
	//Determine Wether or not the enemy should be punching the player
	void AttackWhenCloseToPlayer(Vector3 arg_playerPosition, Vector3 arg_enemyPosition, float arg_attackRange)
    {

		AttackCooldownTimer += Time.deltaTime;
		if (AttackCooldownTimer >= AttackCooldownDuration)
		{
			AttackCooldown = false;
		}

		if (!AttackCooldown)
        {
            //Vars
            Vector3 _player_position = arg_playerPosition;
            Vector3 _enemy_position = arg_enemyPosition;
            float _attack_range = arg_attackRange;

            //Calculate Vars
            float _distance = _player_position.x - _enemy_position.x;
            float _mag_distance = Mathf.Sqrt(Mathf.Pow(_distance, 2));

            //Attack if the player is close enough
            IN.attack = (_mag_distance < _attack_range);

            //Set Attack Cooldown
            AttackCooldown = true;
            AttackCooldownTimer = 0;
        }
        else
        {
            IN.attack = false;
        }
    }




	//Choose the enemies input based on CurrentBehaviour
	void DetermineInputFromCurrentBehaviour()
    {
        //Vars
		Vector3 _player_pos = PlayerObject.transform.position;
		Vector3 _this_enemy_pos = transform.position;



		////////Movement
		{       

            switch (CurrentEnemyBehaviour)
            {
                case EnemyBehaviour.None:
                    {

                        break;
                    }

                case EnemyBehaviour.FacePlayer:
                    {
                        FacePlayer(_player_pos, _this_enemy_pos);
                        break;
                    }

                case EnemyBehaviour.ApproachPlayer:
                    {
                        FacePlayer(_player_pos, _this_enemy_pos);
                        ApproachPlayer(_player_pos, _this_enemy_pos, ApproachDistance);
                        break;
                    }

                case EnemyBehaviour.HalfApproachPlayer:
                    {
                        FacePlayer(_player_pos, _this_enemy_pos);
                        ApproachPlayer(_player_pos, _this_enemy_pos, HalfApproachDistance);

                        break;
                    }



            }
        }


        ///////Combat
        {

            //If the player is within punch range...swing
            AttackWhenCloseToPlayer(_player_pos, _this_enemy_pos, AttackRange);

        }


    }

    //Determine Enemy Behaivour
    void EnemyInput()
    {
        DetermineInputFromCurrentBehaviour();
    }


    // Update is called once per frame
    void Update()
    {
        EnemyInput();
        HandleInput();
        HandleState();

    }
}
