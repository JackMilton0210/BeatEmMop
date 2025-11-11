using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : CharacterHealth
{

    [SerializeField] float DisplayHealth;

	public void SetupComponents()
	{
		controller = GetComponent<PlayerController>();
	}


	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DisplayHealth = CurrentHealth();
        HandleHealth();
        HandleStance();
        UpdateHealthLastFrame();

	}
}
