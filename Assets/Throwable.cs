using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{

    //Objects and Components
    GameObject PlayerObject;
    MovementController movement;
    BoxCollider2D collider;

    //Vars
    [SerializeField] TagWrapper DamageTag;
    [SerializeField] float Damage = 3;
    [SerializeField] float StanceDamage = 10;

    [HideInInspector] public bool InputKeyPressed = false;
    [SerializeField] float InteractionRange = 2;

	bool thrown = false;
    [SerializeField] float Initial_HorizontalSpeed;
    [SerializeField] float Initial_VerticalSpeed;
    [SerializeField] float FallSpeed = 1;
    [SerializeField] float RotationSpeed = 1;
    float current_rotation = 0;
    
    //List of colliders that have already been hit
    List<BoxCollider2D> alreadyHit = new List<BoxCollider2D>();

    //Throw
    void ActivateThrowable()
    {
        thrown = true;
        movement.moving_right = true;
        movement.HorizontalSpeed = Initial_HorizontalSpeed;
        movement.moving_down = true;
        movement.VerticalSpeed = Initial_VerticalSpeed;
    }
    //In Motion
    void ThrowUpdate()
    {
        if (thrown)
        {
			movement.VerticalSpeed += FallSpeed * Time.deltaTime;

			current_rotation -= RotationSpeed * Time.deltaTime;
            Vector3 rotCopy = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
            rotCopy.z = current_rotation;
            transform.rotation = Quaternion.Euler(rotCopy);


            if (true)
            {
                //CollisionCheck
                BoxCollider2D[] colliders = FindObjectsByType<BoxCollider2D>(FindObjectsSortMode.None);

                for (int i = 0; i < colliders.Length; i++)
                {

                    //Translate the TagWrapper to String Tag
                    string damageTagString = "";
                    {
                        if (DamageTag == TagWrapper.Player)
                            damageTagString = "PlayerObject";
                        else if (DamageTag == TagWrapper.Enemy)
                            damageTagString = "EnemyObject";
                    }


                    //If the object is a player object apply damage
                    //if (colliders[i].gameObject.tag == damageTagString)
                    if (colliders[i].gameObject.tag == damageTagString)
                    {

                        if (colliders[i].IsTouching(collider))
                        {
                            //If this collider has not already been hit
                            bool canHit = true;
                            for(int j = 0; j < alreadyHit.Count; j++)
                            {
                                if (colliders[i] == alreadyHit[j])
                                {
                                    canHit = false;
                                    break;
                                }
                            }

                            if (canHit)
                            {
                                colliders[i].gameObject.GetComponent<Health>().ApplyDamage(Damage);
                                colliders[i].gameObject.GetComponent<StanceHealth>().ApplyStanceDamage(StanceDamage);
                                alreadyHit.Add(colliders[i]);
                            }

                        }
                        else
                        {
                            
						}
                    }
                }
            }

		}
    }

    bool PlayerInRange()
    {
        float d = InteractionRange;

        Vector2 throwerPos = new Vector2(PlayerObject.transform.position.x, PlayerObject.transform.position.y);
        Vector2 thisPos = new Vector2(transform.position.x, transform.position.y);


        //Return false if the player is out of range
        if ((throwerPos.x - thisPos.x) * (throwerPos.x - thisPos.x) > d * d)
            return false;
        if ((throwerPos.y - thisPos.y) * (throwerPos.y - thisPos.y) > d * d)
            return false;

        return true;
	}



    // Start is called before the first frame update
    void Start()
    {


        //Assign PlayerObject
        PlayerObject = GameObject.FindWithTag("PlayerObject");
        if (!PlayerObject)
            Debug.Log("Error - Throwble::Start: No PlayerObject found");

		//AssignComponents
		movement = GetComponent<MovementController>();
		if (!movement)
			Debug.Log("Error - Throwable::Start: No MovementController Component");

        collider = GetComponent<BoxCollider2D>();
        if (!collider)
            Debug.Log("Error - Throwable::Start: No BoxCollider2D Component");
	}

    // Update is called once per frame
    void Update()
    {
        ThrowUpdate();




        //Test Throwable
        if (InputKeyPressed)
        {

            if(PlayerInRange())
                    ActivateThrowable();
        }
    }
}
