using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSystem : MonoBehaviour
{
    Collected_BloodResource Collected;
    Uncollected_BloodResource Uncollected;
    PowerUp power;

    //Returns True if Blood Can Be Transfered from Uncollected To Collected
    bool CanMop()
    {
        //Cannot Mop When Power Is Active
        if (power.IsActive())
            return false;

        //Cannot Mop When Collected Blood Is Full
        if (Collected.AtMax())
            return false;

        //Cannot Mop When Uncollected Blood is Empty
        if (Uncollected.AtMin())
            return false;
        
        //Mopping allowed
        return true;
    }
    //Mop up blood by Decreasing Uncollected blood and Increasing COllected Blood
    public void MopBlood(float arg_amount)
    {
        //Return If Cant mop
        if (!CanMop()) return;

        //Decrease Uncollected Blood
        Uncollected.Decrease(arg_amount);
        //Increase Collected Blood
        Collected.Increase(arg_amount);

    }

    //Returns True if Blood Can be added to the Uncollected Blood Pool
    bool CanAddBlood()
    {
        if (Uncollected.AtMax()) 
            return false;
        return true;
    }
    //Increase the amount of Uncollected Blood
    public void AddBlood(float arg_amount)
    {
        //Return If no blood can be added
        if (!CanAddBlood()) return;

        //Increase the Blood in the Uncollected Pool
        Uncollected.Increase(arg_amount);
    }

    //Returns Collected Blood as from 0..1 as a factor of its maximum value
    public float CollectedBloodPercentage()
    {
        return Collected.AsPercentage();
    }
    //Return Uncollected Blood as perccentage from 0...1
    public float UncollectedBloodPercentage()
    {
        return Uncollected.AsPercentage();
    }
    //Return true when Collected Blood is full
    public bool CollectedBloodFull()
    {
        if (Collected.AtMax())
            return true;
        return false;
        
    }

    //Add Components Used In Blood System
    void AddComponents()
    {
        Collected = gameObject.AddComponent<Collected_BloodResource>();
        Uncollected = gameObject.AddComponent<Uncollected_BloodResource>();
        power = gameObject.AddComponent<PowerUp>();
        power.blood = Collected;
        power.PlayerObject = Component.FindFirstObjectByType<PlayerComponent>().gameObject;

	}
    // Start is called before the first frame update
    void Start()
    {
        AddComponents();
        
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            power.ActivatePower();
        }

        if(Input.GetKeyDown("joystick 1 button 2"))
        {
            power.ActivatePower();
        }
    }
}
