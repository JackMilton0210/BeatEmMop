using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Overlay : MonoBehaviour
{
    [SerializeField] Sprite NormalImage;
    [SerializeField] Sprite RageImage;

    [SerializeField] bool BlinkingEffect = true;
    bool RageActive = false;

    //Normal Colour
    Vector4 Colour_Normal = new Vector4(1, 1, 1, 1);
    Vector4 Colour_Rage = new Vector4(1, 1, 1, 1);

    //Flash Timer
    float FadeOutDuration = 0.6f;
    float FadeOutTimer = 0;
    


    void GetRageBool()
    {
        RageActive = Component.FindFirstObjectByType<PowerUp>().IsActive();
    }
    void SetOverlay()
    {
        if (RageActive)
        {
            GetComponent<Image>().sprite = RageImage;
        }
        else
        {
            GetComponent<Image>().sprite = NormalImage;
        }
    }
    void HandleFlashing()
    {
        if (BlinkingEffect)
        {
            if (RageActive)
            {
                //Handle Fade Timer
                FadeOutTimer += Time.deltaTime;
                if (FadeOutTimer >= FadeOutDuration)
                {
                    FadeOutTimer = 0;
                }

                //Set Colour of sprite based on timer
                float _current_transparency = GetComponent<Image>().color.a;
                Color newColour = Colour_Rage;
                newColour.a = 1 - EasingFunctions.EaseInOutSine(0, FadeOutDuration, FadeOutTimer);

                GetComponent<Image>().color = newColour;

                return;


            }
		}
	
        GetComponent<Image>().color = Colour_Normal;
		
	}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetRageBool();
        SetOverlay();
        HandleFlashing();
    }
}
