using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Profiling;
//using UnityEditor.SearchService;
using UnityEngine;

public enum TagWrapper{ Player, Enemy, Blood };


public class Attack : MonoBehaviour
{

    [SerializeField] float Damage = 10;
    [SerializeField] float StanceDamage = 10;

    [SerializeField] public Vector2 HitPosition;
    [HideInInspector] public Vector2 CurrentHitPosition;
    [SerializeField] protected TagWrapper DamageTag;

    [SerializeField] float MaxAttackTimer = 0.2f;
    float CurrentAttackTimer;
    [HideInInspector] public bool inAttack = false;
    [HideInInspector] public bool enemyHit = false;
    [HideInInspector] public bool queueAttack = false;

    //Swing Position Movement
    float dist = 0;
    [SerializeField] Vector2 SwingStartPos = Vector2.one;
    [SerializeField] float swing_speed = 1;
    [SerializeField] float swing_delay = 0;
    float swing_time = 0;
    public float swingTime = 0;


	//Tests for colliders at the hit position and if its a player, apply Damage to the health component
	public void Hit()
    {

       
        //Perform Attack
        if (!inAttack)
        {

            inAttack = true;
			GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteSwapper>().Punch;


			//Get all boxcollider2d
			BoxCollider2D[] colliders = FindObjectsByType<BoxCollider2D>(FindObjectsSortMode.None);
            for (int i = 0; i < colliders.Length; i++)
            {

                //Translate the TagWrapper to String
                string damageTagString = "";
                {
                    switch (DamageTag)
                    {
                        case TagWrapper.Player: 
                            damageTagString = "PlayerObject";
                            break;

                        case TagWrapper.Enemy:
                            damageTagString = "EnemyObject";
                            break;

                        case TagWrapper.Blood:
                            damageTagString = "BloodObject";
                            break;
                    }

                }


                //If the object is a player object apply damage
                if (colliders[i].gameObject.tag == damageTagString)
                {
    
                    if (colliders[i].OverlapPoint(CurrentHitPosition))
                    {
                        //Attack Collision
                        Debug.Log("Hit");
                        enemyHit = true;

                        //Apply Damage
                        colliders[i].gameObject.GetComponent<Health>().ApplyDamage(Damage);
                        colliders[i].gameObject.GetComponent<StanceHealth>().ApplyStanceDamage(StanceDamage);

                        //Drop Blood
                        //FindFirstObjectByType<Blood>().ReduceBlood(-1);
                        FindFirstObjectByType<FloorBlood>().IncreaseBlood(1);

                    }
                    else
                    {
                        //Debug.Log("Miss");
                        
                    }
                }
            }
            
            

        }
        //Add Attack to a buffer
        else
        {
			//Debug.Log("Attack::Hit-FALSE------------------");

		}

	}

    
    void UpdateCurrentHitPosition()
    {
		//Update Current Hit position
		//Get flip status of the object
		float flipDir = 1;
        if (GetComponent<SpriteFlipper>() != null)
        {
            if (GetComponent<SpriteFlipper>().flip)
                flipDir = -1;
        }
        else
        {
            Debug.Log("Error - Attack::UpdateCurrentHitPosition: No SpriteFlipper Component Found");
        }

        if (false)
        {
            CurrentHitPosition.x = (flipDir * HitPosition.x) + transform.position.x;
            CurrentHitPosition.y = HitPosition.y + transform.position.y;
        }
        else
        {
            if (inAttack)
            {

                //Get Attack Timer
                swingTime = MaxAttackTimer - CurrentAttackTimer;

                if (swingTime > swing_delay)
                {
                    //Distance of the Swing
                    dist += Time.deltaTime * swing_speed;

                    //Directional Distance of the swing
                    dist *= flipDir;

                    //Get Player Pos
                    Vector2 PlayerPos = transform.position;

                    //Set swing to the player pos
                    Vector2 SwingPos = PlayerPos;

                    //Add the starting swing pos
                    SwingPos += SwingStartPos;

                    //Add distance to swingpos 
                    SwingPos.x += (dist * flipDir);

                    //Update HitPos
                    CurrentHitPosition = SwingPos;


                }
            }
            //While Not In Attack Reset the Hit Position
            else
            {
                //CurrentHitPosition = SwingStartPos;
            }

        }        

	}

    void UpdateAttack()
    {
		//During Attack Timer
		if (inAttack)
		{

			CurrentAttackTimer -= Time.deltaTime;

			if (CurrentAttackTimer <= 0)
			{
				inAttack = false;
                enemyHit = false;
                GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteSwapper>().Idle;
				CurrentAttackTimer = MaxAttackTimer;
                dist = 0;
			}
		}
	}

	// Start is called before the first frame update
	void Start()
    {
		CurrentAttackTimer = MaxAttackTimer;
	}

    // Update is called once per frame
    void Update()
    {
        UpdateCurrentHitPosition();
        UpdateAttack();

    }
}
