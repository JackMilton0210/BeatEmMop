using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBlood : MonoBehaviour
{
    [SerializeField] float MaxBlood = 10;
    public float CurrentBlood = 0;
    [SerializeField] float BloodDroppedOnHit = 1;
    
    //Rendering
    SpriteRenderer this_sprite;
    

    public void IncreaseBlood(float lblood)
    {
        if (CurrentBlood + lblood <= MaxBlood)
            CurrentBlood += lblood;
    }

    public void DecreaseBlood(float lblood)
    {
        if(CurrentBlood - lblood >= 0)
        {
            CurrentBlood -= lblood;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this_sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.V))
        {
            IncreaseBlood(BloodDroppedOnHit);
        }


        //Update Appearance
        Color newColor = this_sprite.color;
        newColor.a = CurrentBlood / MaxBlood;
        this_sprite.color = newColor;


	}
}
