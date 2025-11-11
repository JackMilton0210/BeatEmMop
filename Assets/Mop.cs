using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mop : MonoBehaviour
{
    //Blood Component
    Blood bloodComponent;
    void GetBloodComponent() {
        bloodComponent = GameObject.FindWithTag("BloodObject").GetComponent<Blood>();
        if (bloodComponent != null) Debug.Log("Error - Mop::GetBloodComponent() Failed");
    }
    SpriteSwapper spriteSwapper;
    void GetSpriteSwapperComponent()
    {
        spriteSwapper = GetComponent<SpriteSwapper>();
        if (spriteSwapper != null) Debug.Log("Error - Mop::GetSpriteSwapperComponent() Failed");
    }

    public bool Mopping = false;
    bool CleanFinished = true;
    
    [SerializeField] float BloodCleaned = 1;
	[SerializeField] float MaxMopTime = 0.2f;
    float CurrentMopTime;

	public void BeginMop()
    {
        if (!Mopping)
        {
            CleanFinished = false;
            Mopping = true;
            CurrentMopTime = MaxMopTime;
			GetComponent<SpriteRenderer>().sprite = spriteSwapper.Mopping;
		}
        
    }

    void UpdateMop()
    {

		
		if (Mopping)
        {
            CurrentMopTime -= Time.deltaTime;
		}

        if (!CleanFinished)
        {
            CleanFinished = true;
            bloodComponent.ReduceBlood(BloodCleaned);
        }

        if ((CurrentMopTime <= 0))
        {
			EndMop();
        }
    }

    void EndMop()
    {
        
        Mopping = false;
        CurrentMopTime = MaxMopTime;
        GetComponent<SpriteRenderer>().sprite = spriteSwapper.Idle;
    }

    // Start is called before the first frame update
    void Start()
    {
        GetBloodComponent();
        GetSpriteSwapperComponent();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMop();
    }
}
