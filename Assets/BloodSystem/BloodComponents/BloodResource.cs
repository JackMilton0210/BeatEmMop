using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodResource : MonoBehaviour
{

    [SerializeField] protected float Current;          //Current Amount of blood
	[SerializeField] protected float Max;              //Maximum amount of blood
	[SerializeField] protected float Min;              //Minimum amount of blood



    //Ensure Current remains within Min and Max
    protected void Limit()
    {
        if (Current > Max) Current = Max;
        if (Current < Min) Current = Min;
    }
    //Increase Current by arg amount
    public void Increase(float arg_amount)
    {
        Current += arg_amount;
        Limit();
    }
    //Decrease Current bu arg_amount
    public void Decrease(float arg_amount)
    {
        Current -= arg_amount;
        Limit();
    }


    //Returns true if Current is at Max
    public bool AtMax()
    {
        if (Current >= Max) 
            return true;
        return false;
    }
    //Returns true if Current is at Min
    public bool AtMin()
    {
        if (Current <= Min)
            return true;
        return false;
    }
    //Returns Blood As a Percentage of Max
    public float AsPercentage()
    {
        return Current / Max;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
