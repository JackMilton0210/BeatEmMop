using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Fader : MonoBehaviour
{

    [SerializeField] protected bool TriggerFadeOut = false;
    [SerializeField] protected bool TriggerFadeIn = false;

    //State of the fader
    [SerializeField] float CurrentVisibility = 1;
    float FadeInSpeed = 1;
    float FadeOutSpeed = 1;

    enum FadeState { Invisible, FadingIn, Visible, FadingOut };
    FadeState CurrentFadeState = FadeState.Invisible;

    //Set State
    public void SetFadeIn() { CurrentFadeState = FadeState.FadingIn; }
    public void SetFadeOut() { CurrentFadeState = FadeState.FadingOut; }

    //State Checkers
    public bool IsInvisible() { if (CurrentFadeState == FadeState.Invisible) return true; return false; }
    public bool IsVisible() { if (CurrentFadeState == FadeState.Visible) return true; return false; }
    public bool IsFadingIn() { if (CurrentFadeState == FadeState.FadingIn) return true; return false; }
    public bool IsFadingOut() { if (CurrentFadeState == FadeState.FadingOut) return true; return false; }
    public bool IsFading() { if (CurrentFadeState == FadeState.FadingOut || CurrentFadeState == FadeState.FadingIn) return true; return false; }




    void FadeIn()
    {
        
		//Hold time scale
		float oldTimeScale = Time.timeScale;
		Time.timeScale = 1;
		CurrentVisibility += FadeInSpeed * Time.deltaTime;
		Time.timeScale = oldTimeScale;

		if (CurrentVisibility < 1)
            return;
        CurrentFadeState = FadeState.Visible;
        CurrentVisibility = 1;
    }
    void FadeOut()
    {

        //Hold time scale
        float oldTimeScale = Time.timeScale;
        Time.timeScale = 1;
        CurrentVisibility -= FadeOutSpeed * Time.deltaTime;
        Time.timeScale = oldTimeScale;

        if (CurrentVisibility > 0)
            return;
        CurrentFadeState = FadeState.Invisible;
        CurrentVisibility = 0;

    }
    void Fade()
    {
        if (CurrentFadeState == FadeState.FadingIn) FadeIn();
        if (CurrentFadeState == FadeState.FadingOut) FadeOut();
    }

    void ApplyFade()
    {
        Color new_color = GetComponent<Image>().color;
        new_color.a = EasingFunctions.EaseInOutQuad(0,1,CurrentVisibility);
        GetComponent<Image>().color = new_color;
    }

    void HandleTriggers()
    {
        if (TriggerFadeIn)
        {
            SetFadeIn();
            TriggerFadeIn = false;
        }
        if (TriggerFadeOut)
        {
            SetFadeOut();
            TriggerFadeOut = false;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Fade();
        ApplyFade();
        HandleTriggers();
    }
}
