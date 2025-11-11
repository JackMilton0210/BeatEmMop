using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{

    public float MaxBlood = 10;
    public float CurrentBlood;
    public float CurrentBloodPercentage;


    public void ReduceBlood(float lblood)
    {
        if(CurrentBlood - lblood >= 0)
				CurrentBlood -= lblood;

        

    }

    public bool AtMaxBlood()
    {
        if (CurrentBlood >= MaxBlood)
            return true;
        return false;
    }


    // Start is called before the first frame update
    void Start()
    {
        CurrentBlood = 0;    
    }

    // Update is called once per frame
    void Update()
    {
        CurrentBloodPercentage = (CurrentBlood / MaxBlood) * 100;
        Color newColour = GetComponent<SpriteRenderer>().color;
        newColour.a = CurrentBloodPercentage/100;
        GetComponent<SpriteRenderer>().color = newColour;
    }
}
