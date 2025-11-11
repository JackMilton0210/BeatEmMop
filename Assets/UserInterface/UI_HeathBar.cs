using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UI_HeathBar : MonoBehaviour
{
    GameObject PlayerObject;
    public UnityEngine.UI.Slider healthSlider;
    

    // Start is called before the first frame update
    void Start()
    {
        PlayerObject = FindFirstObjectByType<PlayerComponent>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
		healthSlider.value = PlayerObject.GetComponent<PlayerHealth>().CurrentHealthPercentage();
	}
}
