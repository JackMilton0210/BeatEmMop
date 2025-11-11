using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StanceHealth : MonoBehaviour
{

    bool knocked;
    [SerializeField] float maxKnockTime = 1;
    float currentKnockTime;
    [SerializeField] float maxStanceHealth = 5;
    float currentStanceHealth;

    //Components
    MovementController movement;
    SpriteSwapper swapper;

    //Used to damage the stance
    public void ApplyStanceDamage(float stance_dmg)
    {
        if(!knocked)
            currentStanceHealth -= stance_dmg;
    }

    
    void KnockDown()
    {
        knocked = true;
        currentKnockTime = maxKnockTime;
        currentStanceHealth = maxStanceHealth;
        movement.SpeedModifier = 0;


        //Swap to Knocked sprite
        {
			GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteSwapper>().Knocked;
			
        }
    }
    void GetUp()
    {
        knocked = false;
        movement.SpeedModifier = 1;

        //Swap to Idle
        {
			GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteSwapper>().Idle;
			
		}

    }

    // Start is called before the first frame update
    void Start()
    {
        //AssignComponents
        movement = GetComponent<MovementController>();
        if (!movement)
            Debug.Log("Error - StanceHealth::Start: No MovementController Component");

        currentStanceHealth = maxStanceHealth;
    }

    // Update is called once per frame
    void Update()
    {

        //Check If Knocked
        if(currentStanceHealth <= 0)
        {
            KnockDown();
        }

        //While Knocked
        if (knocked)
        {
            currentKnockTime -= Time.deltaTime;
            if (currentKnockTime <= 0)
                GetUp();
        }


        //Temp StanceDamage Testing
        if (Input.GetKeyDown(KeyCode.G))
        {
            ApplyStanceDamage(1);
        }

    }
}
