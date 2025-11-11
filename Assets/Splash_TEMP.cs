using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Splash_TEMP : MonoBehaviour
{

    [SerializeField] GameObject levelObject;
    [SerializeField] TextMeshProUGUI StartText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Make SplashScreen Invisible when ESC is pressed
        if(Input.GetKeyDown(KeyCode.Escape)) {
            //gameObject.SetActive(false);
            GetComponent<CanvasRenderer>().SetAlpha(0.0f);
            GetComponentInChildren<TextMeshProUGUI>().alpha = 0.0f;
            
        }

        //Make Splash Screen Visible when level finished
        if (levelObject.GetComponent<Level>().LevelDone())
        {
			GetComponent<CanvasRenderer>().SetAlpha(1.0f);
			GetComponentInChildren<TextMeshProUGUI>().alpha = 0.0f;
		}
    }
}
