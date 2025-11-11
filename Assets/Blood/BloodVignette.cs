using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BloodVignette : MonoBehaviour
{
    //Position Vars
    [SerializeField] GameObject cameraObject;
    [SerializeField] float depth = -5;

    //TransparencyVars
    [SerializeField] BloodPower power;
    [SerializeField] CollectedBlood collected_blood;
    [SerializeField] Vector3 BaseColour = new Vector3(1, 0, 0);
    [SerializeField] float default_alpha = 0;
    [SerializeField] float rage_alpha = 0.2f;
    Color currentColour;

    //Timers
    public bool effect_active = false;
    public float timer = 0.0f;
    [SerializeField] float settle_time = 1;
    [SerializeField] EasingFunctions.Ease easer;

    void MoveToCamera()
    {
        Vector3 cameraPos = cameraObject.transform.position;
        transform.position = new Vector3(cameraPos.x, cameraPos.y, depth);
    }

    void ApplyEffect()
    {
        //Begin Effect
        if(power.GetPowerActive() == true)
        {
            if(effect_active == false)
            {
                effect_active = true;

            }
        }

        //Increase Timer
        if(effect_active == true)
        {
            timer += Time.deltaTime;
        }

        //End Effect
        if(power.GetPowerActive() == false)
        {
            if(effect_active == true)
            {
                effect_active = false;
                timer = 0;
            }
        }

    }

    void AdjustTransparency()
    {

        //Set Default Colour
        currentColour.r = BaseColour.x;
        currentColour.g = BaseColour.y;
        currentColour.b = BaseColour.z;
        currentColour.a = default_alpha;

        //Apply Rage Alpha
        //Debug.Log(power.PowerActive);
        if (effect_active)
        {
            float blood_ratio = collected_blood.CurrentBlood / collected_blood.MaxBlood;
            //currentColour.a = rage_alpha * EasingFunctions.EaseOutElastic(0, 1, timer) * blood_ratio;
            currentColour.a = rage_alpha * EasingFunctions.ApplyEase(easer, 0, settle_time, timer) * blood_ratio;
		}

        //Set Sprite to this color
        GetComponent<SpriteRenderer>().color = currentColour;

        //AdjustRotation
        Vector3 newRot = transform.rotation.eulerAngles;
        newRot.z += 90;
        transform.rotation = Quaternion.Euler(newRot);

        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        MoveToCamera();

        ApplyEffect();
        AdjustTransparency();
    }
}
