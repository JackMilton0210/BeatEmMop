using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Win : MonoBehaviour
{

    bool keyPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		//On Key Press go to main menu
		if (Input.anyKeyDown)
		{
            keyPressed = true;
            Component.FindFirstObjectByType<UI_Fader_WinScreen>().SetFadeIn();
            
		}

        //Transition to Main Menu
        if(keyPressed)
		    if (Component.FindFirstObjectByType<UI_Fader_WinScreen>().IsVisible())
    			SceneManager.LoadSceneAsync("Credits");
	}
}
