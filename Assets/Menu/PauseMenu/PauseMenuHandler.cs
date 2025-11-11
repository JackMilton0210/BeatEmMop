using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuHandler : MonoBehaviour
{

    bool ButtonPressedToQuitOut = false;

    //Paused Bool
    [SerializeField] bool Paused = false;

    [SerializeField] Image ControlsOverlayImage;

    //Bool Functions
    void SetPausedBool(bool arg) { Paused = arg; }
    void SetPause() { Paused = true; }
    void SetUnpause() { Paused = false; }
    void TogglePaused() { Paused = !Paused; }

    [SerializeField] bool ControlsVisible = false;
    void ToggleControls() { ControlsVisible = !ControlsVisible; }

    //Pause Menu Functions
    void GetKeyboardInput()
    {
        //if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetButtonDown("joystick 1 button 6")))
        if(Input.GetKeyDown(KeyCode.Escape))
        {
			if (ControlsVisible)
			{
				ToggleControls();
			}
			else
			{
				TogglePaused();
			}
		}
        
    }
    void GetGamepadInput()
    {
		if (Input.GetKeyDown("joystick 1 button 9"))
		{
			if (ControlsVisible)
			{
				ToggleControls();
			}
			else
			{
				TogglePaused();
			}
		}
	}
    void HandleVisibilty(bool arg_paused)
    {        
        GetComponent<Canvas>().enabled = arg_paused;   
    }
    void HandleTimeStep()
    {
        if (Paused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    void HandleControls()
    {
        ControlsOverlayImage.enabled = ControlsVisible;
    }



    public void ResumeGame()
    {
        if(Paused)
        SetUnpause();
    }
    public void RestartGame()
    {
        if(Paused)
        SceneManager.LoadScene("ControllerOverhaulTest");
    }
    public void ControlsScreen()
    {
        if(Paused)
        ToggleControls();
    }
    public void QuitOut()
    {
        if (Paused) {
            ButtonPressedToQuitOut = true;
            Component.FindFirstObjectByType<UI_Fader>().SetFadeIn();
        }
    }

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetKeyboardInput();
        GetGamepadInput();
        HandleVisibilty(Paused);
        HandleTimeStep();
        HandleControls();

        if (ButtonPressedToQuitOut)
            if (Component.FindFirstObjectByType<UI_Fader>().IsVisible())
            {
                ButtonPressedToQuitOut = false;
#if UNITY_STANDALONE
                Application.Quit();
#endif

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
            else
                Time.timeScale = 1;
	}
}
