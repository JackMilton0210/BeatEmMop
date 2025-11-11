using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
	public Collected_BloodResource blood;
    public GameObject PlayerObject;

	float BloodDrainSpeed = 2;      //Amount of blood that drains during the power per second
    bool Active;                    //Current Active State of the Power

    public bool IsActive()
    {
        return Active;
    }


    public void ActivatePower()
    {
        if (blood.AtMax())
        {
            Active = true;
            PlayerObject.GetComponent<PlayerAttackController>().HealthDamageMultiplier = 2;
            PlayerObject.GetComponent<PlayerAttackController>().StanceDamageMultiplier = 2;

        }
    }
    public void UpdatePower()
    {
        if (Active)
        {            
            //Decrease Blood
            blood.Decrease(BloodDrainSpeed * Time.deltaTime);

            //If Blood Reaches Min then end Power
            if (blood.AtMin())
            {
                DeactivatePower();
            }
        }
    }
    public void DeactivatePower()
    {
        Active = false;
		PlayerObject.GetComponent<PlayerAttackController>().HealthDamageMultiplier = 1;
		PlayerObject.GetComponent<PlayerAttackController>().StanceDamageMultiplier = 1;
	}


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePower();
    }
}
