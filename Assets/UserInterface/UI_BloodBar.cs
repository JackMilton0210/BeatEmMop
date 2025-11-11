using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BloodBar : MonoBehaviour
{
	GameObject BloodSystemObject;
	public UnityEngine.UI.Slider bloodSlider;

	// Start is called before the first frame update
	void Start()
    {
		BloodSystemObject = FindFirstObjectByType<BloodSystem>().gameObject;
	}

    // Update is called once per frame
    void Update()
    {
		bloodSlider.value = BloodSystemObject.GetComponent<BloodSystem>().CollectedBloodPercentage();
	}
}
