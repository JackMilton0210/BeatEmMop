using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] bool testingLogs = false;

    //Main health var
    [HideInInspector] public bool alive;
    //AudioWwise SFX logic
    [HideInInspector] public bool playerHit = false;
    public float maxHealth = 100;
    public float currentHealth;

    public void ApplyDamage(float dmg) {
        if(testingLogs)
            Debug.Log("Damage");
        playerHit = true;
        currentHealth -= dmg;
        playerHit = false;
    }
    void ResetHealth() {
        currentHealth = maxHealth;
        alive = true;
    }


    public void SetMaxHealth(float newHealth)
    {
        maxHealth = newHealth;
        ResetHealth();
    }
    // Start is called before the first frame update
    void Start()
    {
        //maxHealth = 100;
        ResetHealth();
    }

    // Update is called once per frame
    void Update()
    {

        //Set Entity to dead if health drops below 0
        if(currentHealth <= 0)
        {
            alive = false;
        }

        //Debug.Log(currentHealth);
    }
}
