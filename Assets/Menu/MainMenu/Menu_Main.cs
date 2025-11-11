using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_Main : MonoBehaviour
{

    //[SerializeField]public Button StartButton;

    [SerializeField] string MainLevel = "ControllerOverhaulTest";
    bool ButtonPressedToStartLevel = false;
    bool ButtonPressedToQuitOut = false;

    [SerializeField] bool ControlsVisible = false;
    [SerializeField] GameObject ControlsOverlayImage; 

    // Start is called before the first frame update
    void Start()
    {
		//Button btn = GetComponent<Button>();
		//btn.onClick.AddListener(StartButtonPressed);
       
	}

    //Start Button Function
    public void StartLevel()
	{
		Component.FindFirstObjectByType<UI_Fader_MainMenu>().SetFadeIn();
		ButtonPressedToStartLevel = true;
        
	}

    //Controls Buttons Function
    public void ControlsOverlay()
    {
        ControlsVisible = !ControlsVisible;
    }

    //Quit Button Function
    public void QuitOut()
    {
        Component.FindFirstObjectByType<UI_Fader_MainMenu>().SetFadeIn();
        ButtonPressedToQuitOut = true;
    }


    void HandleControlsOverlay()
    {
        if (ControlsVisible)
        {
            ControlsOverlayImage.SetActive(true);
        }
        else
        {
            ControlsOverlayImage.SetActive(false);
        }

        if (ControlsVisible)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ControlsVisible = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleControlsOverlay();

        if (ButtonPressedToStartLevel)
            if (Component.FindFirstObjectByType<UI_Fader_MainMenu>().IsVisible())
            {
                ButtonPressedToStartLevel = false;
                SceneManager.LoadSceneAsync("ControllerOverhaulTest");
            }

        if (ButtonPressedToQuitOut)
            if (Component.FindFirstObjectByType<UI_Fader_MainMenu>().IsVisible())
            {
                ButtonPressedToQuitOut = false;
                #if UNITY_STANDALONE
				Application.Quit();
                #endif
                #if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
                #endif
			}
	}
}
