using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{

    //GameObjects and Components
    protected CharacterController controller;
    [SerializeField] protected float HealthLastFrame;
    [SerializeField] protected bool HealthDroppedThisFrame = false;

    //Health Element
    struct HealthElement
    {
        public void Begin(float arg_maxHealth)
        {
            Max = 0;
            Current = 0;
            SetMax(arg_maxHealth);
            SetCurrentToMax();
        }

        float Max;
        float Current;
        public void SetCurrentToMax() { Current = Max; }
        public void SetMax(float arg_newMax) { Max = arg_newMax; }
        public void Increase(float arg_amount) { Current += arg_amount; }
        public void Decrease(float arg_amount) { Current -= arg_amount; }
        public void Set(float new_value) { Current = new_value; }
        public float Get() { return Current; }

        

        public bool CurrentAtZero()
        {
            if (Current <= 0)
                return true;
            return false;
        }

        public bool CurrentHealthAtMax()
        {
            if (Current >= Max)
                return true;
            return false;
        }
		//Return Current
		public float GetCurrent() { return Current; }
		//Returns Max
		public float GetMax() { return Max; }

    }



    /// <summary>
    /// Health
    /// </summary>

    //Health Vars
    public bool alive = true;
    [SerializeField] HealthElement health;
    
    //Update Health Vars
    protected void HandleHealth()
    {
        //If health drops below 0
        if (health.CurrentAtZero())
            alive = false;
        else
            alive = true;
    }

	//Applies Damage to the Health
	public void DamageHealth(float arg_healthDamage)
	{
		health.Decrease(arg_healthDamage);
	}
	//Returns Alive Atate of the Player
	public bool IsAlive()
	{
		return alive;
	}
	//Returns Current Health of the Player
	public float CurrentHealth()
	{
		return health.GetCurrent();
	}
    //Returns Current Health as a percentage of max
    public float CurrentHealthPercentage()
    {
        return health.GetCurrent() / health.GetMax();
    }

    public void IncreaseHealth(float arg_amount)
    {
        health.Increase(arg_amount);
    }
    public void IncreaseStance(float arg_amount)
    {
        stance.Increase(arg_amount);
    }

    public void SetStanceToZero()
    {
        stance.Set(0);
    }


	/// <summary>
	/// Stance
	/// </summary>
	/// 

	HealthElement stance;

    //Handle the Character Stance
    protected void HandleStance()
    {
        //If stance drops below 0
        if (stance.CurrentAtZero())
            BeginKnock();

    }

    //Start knock timer and knock down character
    void BeginKnock()
    {
        stance.SetCurrentToMax();
        //Move controller to KnockState
        controller.SetState(State.Knocked);
        
    }

    //Deal Damage to the stance Var
    public void DamageStance(float arg_stanceDamage)
    {
        stance.Decrease(arg_stanceDamage);
    }
    //Returns the current stance health of the character
    public float CurrentStance() { return stance.GetCurrent(); }
    //return the current stance health of the character as a percentage of the max
    public float CurrentStancePercentage() { return stance.GetCurrent() / stance.GetMax(); }


    // Update HealthLastFrame Variable
    protected void UpdateHealthLastFrame()
    {
        // Run Comparison to return true
        if(HealthLastFrame > health.Get())
        {
            HealthDroppedThisFrame = true;
        }
        else
        {
			HealthDroppedThisFrame = false;
		}
        HealthLastFrame = health.Get();
    }

    public bool DidHealthDropThisFrame() { return HealthDroppedThisFrame; }




    //Set starting Variables
    public void Begin(float arg_maxHealth, float arg_maxStance)
    {
        health.Begin(arg_maxHealth);
        stance.Begin(arg_maxStance);
        alive = true;
        HealthLastFrame = health.Get();
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
