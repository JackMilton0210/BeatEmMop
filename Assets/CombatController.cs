using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    Throwable[] throwables;

	Attack attackScript1;
    Mop mopScript;

    [HideInInspector] public bool attack1;
    [HideInInspector] public bool throw_button;
    [HideInInspector] public bool mop;

	void GetThrowables()
	{
		//All Throwables in the scene
		throwables = FindObjectsByType<Throwable>(FindObjectsSortMode.None);

	}

    //Get Components
    void GetComponents()
    {
		attackScript1 = GetComponent<Attack>();
		if (!attackScript1)
			Debug.Log("Error - CombatController::Start: No Attack Component");

        mopScript = GetComponent<Mop>();
        if (!mopScript)
            Debug.Log("Error - CombatController::GetComponents: No Mop Component");


	}

	// Start is called before the first frame update
	void Start()
    {
        GetComponents();
        GetThrowables();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (attack1)
        {
            attackScript1.Hit();
            attack1 = false;
        }

        if (mop)
        {
            mopScript.BeginMop();
            mop = false;
        }

        //Update each throwables InputKey
        for(int i = 0; i < throwables.Length; i++)
        {
            throwables[i].InputKeyPressed = throw_button;
        }


    }
}
