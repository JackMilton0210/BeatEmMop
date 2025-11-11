using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodMop : MonoBehaviour
{

    FloorBlood m_FloorBlood;
    CollectedBlood m_CollectedBlood;
    [SerializeField] GameObject FloorBloodObject;
    [SerializeField] GameObject CollectedBloodObject;

    bool MoppingActive = false;
    bool BloodCleaned = false;

    [SerializeField] float BloodCleanedOnMop = 1;
    [SerializeField] float MaxMopTime = 1;
    [SerializeField] float BloodCleanTime;
    float CurrentMopTime;

    void BeginMop()
    {
        if (!MoppingActive)
        {
            CurrentMopTime = MaxMopTime;
            MoppingActive = true;
            BloodCleaned = false;
            GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteSwapper>().Mopping;
        }
    }
    void UpdateMop()
    {

        //Debug.Log("Update Mop Pressed");

        //Decrease Mop Time
        if (MoppingActive)
        {
            CurrentMopTime -= Time.deltaTime;
        }

        //Apply Blood Clean
        if (!BloodCleaned)
        {
            //Debug.Log("PerformMop");
            BloodCleaned = true;
            m_CollectedBlood.PerformClean(BloodCleanedOnMop);
        }

        //End Mop
        if(CurrentMopTime <= 0)
        {
            EndMop();
        }

    }
    void EndMop()
    {
        MoppingActive = false;
		GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteSwapper>().Idle;
	}

    // Start is called before the first frame update
    void Start()
    {
        m_FloorBlood = FloorBloodObject.GetComponent<FloorBlood>();
        m_CollectedBlood = CollectedBloodObject.GetComponent<CollectedBlood>();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.M))
		{
            //Debug.Log("M Key Pressed");
			BeginMop();
		}
		UpdateMop();

        
    }
}
