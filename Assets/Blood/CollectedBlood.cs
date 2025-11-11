using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectedBlood : MonoBehaviour
{

	[SerializeField] GameObject FloorBloodObject;
    FloorBlood floorBlood;

	[SerializeField] public float MaxBlood = 10;
    [SerializeField] float BloodToUsePower = 10;
    public float CurrentBlood = 0;

    [SerializeField] float BloodPickedOnMop = 1;

    public void IncreaseBlood(float lblood)
    {
        if (CurrentBlood + lblood <= MaxBlood)
            CurrentBlood += lblood;
    }

    public void DecreaseBlood(float lblood)
    {
        if (CurrentBlood - lblood >= 0)
            CurrentBlood -= lblood;
    }

    bool AtMaxBlood()
    {
        if(CurrentBlood >=MaxBlood)
            return true;
        return false;
    }

    public bool CanUsePower()
    {
        if (CurrentBlood >= BloodToUsePower)
            return true;
        return false;
    }

    public void PerformClean(float lBlood)
    {
        if (floorBlood.CurrentBlood >= 1)
        {
            if (GetComponent<BloodPower>().GetPowerActive() == false)
            {
                IncreaseBlood(lBlood);
                floorBlood.DecreaseBlood(lBlood);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        floorBlood = FloorBloodObject.GetComponent<FloorBlood>();
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.B))
        {
            PerformClean(BloodPickedOnMop);
        }


    }
}
