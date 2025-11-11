using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPower : MonoBehaviour
{

    //BucketBlood
    CollectedBlood collectedBlood;

    bool PowerActive;
    
    public bool GetPowerActive() { return PowerActive; }
    
    [SerializeField] float DecreaseTime = 3;

    //PowerActive is broken?? use this for now
    public bool vignette = false;

	void BeginPower()
    {
        if (collectedBlood.CanUsePower())
        {
            PowerActive = true;
            vignette = true;

            //Begin Effects

        }
    }
    void UpdatePower()
    {
        if (PowerActive)
        {
            float decreaseRate = (DecreaseTime/1) * Time.deltaTime;
            if (collectedBlood.CurrentBlood - decreaseRate >= 0)
            {
                collectedBlood.DecreaseBlood(decreaseRate);
                PowerActive = true;
                //Debug.Log("PowerActive - Decreasing");
            }
            else
            {
                EndPower();
                //Debug.Log("PowerActive - End");

            }
        }
    }

    void EndPower()
    {
        PowerActive = false;
        vignette = false;
		collectedBlood.CurrentBlood = 0;

		//End Effects
	}


    // Start is called before the first frame update
    void Start()
    {
        collectedBlood = GetComponent<CollectedBlood>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePower();

        if (Input.GetKeyDown(KeyCode.N))
        {
            BeginPower();
        }

        //Debug.Log(PowerActive);
        //Debug.Log(vignette);

    }
}
