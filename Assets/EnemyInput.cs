using UnityEngine;

public enum EnemyBehaviourType { Stationary, MoveToPlayer}

public class EnemyInput : MonoBehaviour
{

    //[SerializeField] EnemyBehaviourType currentBehaivour = EnemyBehaviourType.Stationary;
    [SerializeField] public EnemyBehaviourType currentBehaivour = EnemyBehaviourType.MoveToPlayer;
    [SerializeField] Vector2 Min_MoveToThreshold;
    [SerializeField] float moveToPlayerRange;
    [SerializeField] float punchRange;
	Transform playerTransform;


	void FindPlayer()
    {
        playerTransform = GameObject.FindGameObjectWithTag("PlayerObject").transform;
	}

    void Stationary_Behaviour()
    {
		//Debug.Log("EnemyInput::StationaryBehaviour");
		return;
    }
    void MoveToPlayer_Behaviour()
    {

        //Get player and enemy positions
        GameObject playerObject = GameObject.FindGameObjectWithTag("PlayerObject");
        Transform playerTransform = playerObject.transform;

        Vector3 playerPosition = playerTransform.position;
        Vector3 enemyPosition = transform.position;


        //If horizontal distance to the player is larger than threshold than enemy will move to player
        float diff_x = enemyPosition.x - playerPosition.x;
        float diff_y = enemyPosition.y - playerPosition.y;


        //Move towards the player - Horizontal
		MovementController controller = GetComponent<MovementController>(); 
        if ((diff_x * diff_x > Min_MoveToThreshold.x * Min_MoveToThreshold.x))
        {
            if (diff_x>=0) controller.moving_left = true;
            else controller.moving_right = true;
        }
        else { controller.moving_left = false; controller.moving_right = false; }

		//Move towards the player - Vertical
		if ( (diff_y * diff_y > Min_MoveToThreshold.y * Min_MoveToThreshold.y))
        {
            if(diff_y >= 0) controller.moving_down = true;
            else controller.moving_up = true;
            
        }
        else { controller.moving_up = false; controller.moving_down = false; }


    }

    

    // Start is called before the first frame update
    void Start()
    {
        Min_MoveToThreshold.x = 1.5f; Min_MoveToThreshold.y = 0.5f;
        FindPlayer();

    }

	private void UpdateMovement()
    {
        //Old default enemy behaviour
        if (false)
        {
            //Determine Behaviour type
            float x_diff = playerTransform.position.x - transform.position.x;
            if (x_diff * x_diff < moveToPlayerRange * moveToPlayerRange)
            {
                currentBehaivour = EnemyBehaviourType.MoveToPlayer;
            }
        }

		//Update position
		switch (currentBehaivour)
		{
			case EnemyBehaviourType.Stationary:
				{
					Stationary_Behaviour();
					break;
				}
			case EnemyBehaviourType.MoveToPlayer:
				{
					MoveToPlayer_Behaviour();
					break;
				}
		}
	}

    void UpdateCombat()
    {
		//If the hitpoint in attack is colliding with the player
		float distToPlayer = playerTransform.position.x - transform.position.x;
		if (distToPlayer * distToPlayer < punchRange)
		{
			GetComponent<Attack>().Hit();
		}
	}

	// Update is called once per frame
	void Update()
    {

        //Find Player Object if it is lost
        if (playerTransform == null)
            FindPlayer();

        UpdateMovement();
        UpdateCombat();

	}
}
