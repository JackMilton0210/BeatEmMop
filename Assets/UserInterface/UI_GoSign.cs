using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GoSign : MonoBehaviour
{


    [SerializeField] bool SignVisible = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void GetVisibility()
    {
        SignVisible = !Component.FindFirstObjectByType<LevelCamera>().IsCameraLocked();
    }

    void HandleVisibility()
    {
		Image image = GetComponent<Image>();
		Color _color = image.color;

		if (SignVisible)
		{
            _color.a = 1;
		}
		else
		{
            _color.a = 0;
		}
		//Apply Colour
		image.color = _color;


		

    }


    // Update is called once per frame
    void Update()
    {
        GetVisibility();
        HandleVisibility();
    }
}
