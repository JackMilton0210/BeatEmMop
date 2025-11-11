using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : CharacterHealth
{

    [SerializeField] float HealthDisplay;

    public void SetupComponents()
    {
        controller = GetComponent<EnemyController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HealthDisplay = CurrentHealth();

        HandleHealth();
        HandleStance();

    }
}
