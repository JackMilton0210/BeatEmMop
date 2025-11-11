using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI    ;

public class UI_Prompt : MonoBehaviour
{
    
    [SerializeField] bool Visible = true;
	[SerializeField] bool FadingIn = false;
	[SerializeField] float FadeInSpeed = 10;
	[SerializeField] float FadeOutSpeed = 2;
    [SerializeField] float GreyValue = 0.5f;

    [SerializeField] Sprite ColourSprite;
    [SerializeField] Sprite GreySprite;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void HandlePrompt()
    {
        Visible = Component.FindFirstObjectByType<BloodSystem>().CollectedBloodFull();
    }

    void FadeIn()
    {
        //Get Vars
        float _speed = FadeInSpeed;
        Image image = GetComponent<Image>();
        Color _color = image.color;

        //Modfy Colour
        _color.a += _speed * Time.deltaTime;
        image.color = _color;

        //Toggle Fading In/Out
        if (_color.a >= 1)
            FadingIn = false;

    }
    void FadeOut()
    {
        //Get Vars
        float _speed = FadeOutSpeed;
		Image image = GetComponent<Image>();
		Color _color = image.color;

        //Modify Colour
		_color.a -= _speed * Time.deltaTime;
		image.color = _color;

        //Toggle Fading In/Out
        if (_color.a <= 0)
            FadingIn = true;
	}

    //Slowly fade in and out
    void HandleFade()
    {
        if (FadingIn)
        {
            FadeIn();
        }
        else
        {
            FadeOut();
        }
    }

    //
    void GreyAlpha()
    {
		//Get Vars
		
		Image image = GetComponent<Image>();
		Color _color = image.color;

		//Modify Colour
		_color.a = 0.5f;

        //Apply Colour
		image.color = _color;


	}

	//Handle Visibility
	void HandleGrey()
    {
        //Get SpriteRenderer
        Image image = GetComponent<Image>();


        if (Visible)
        {
            image.sprite = ColourSprite;
            HandleFade();
        }
        else
        {
            image.sprite = GreySprite;
            GreyAlpha();
        }

        

    }



    // Update is called once per frame
    void Update()
    {
        //Get Visible
        HandlePrompt();

        //Turn opacity to 0 if not visible
        HandleGrey();
        
    }
}
