using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    GameObject PlayerObject;
    GameObject BloodObject;
    
    [SerializeField] TextMeshProUGUI healthTextField;
    [SerializeField] TextMeshProUGUI bloodTextField;
    [SerializeField] UnityEngine.UI.Slider bloodBarSlider;
    [SerializeField] UnityEngine.UIElements.Slider bloodBarSlider1;
    

	[SerializeField] string healthStillText = "Health ";
    string healthText;

    [SerializeField] string bloodStillText = "Blood ";
    string bloodText;

    void GetObjects()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("PlayerObject");
        BloodObject = GameObject.FindGameObjectWithTag("BloodObject");
    }


    // Start is called before the first frame update
    void Start()
    {
        GetObjects();
    }

    // Update is called once per frame
    void Update()
    {

        healthText = PlayerObject.GetComponent<Health>().currentHealth.ToString();
        healthTextField.text = healthStillText + healthText;

        bloodText = BloodObject.GetComponent<Blood>().CurrentBloodPercentage.ToString();
        bloodTextField.text = bloodStillText + bloodText + "%";
        bloodBarSlider.value = BloodObject.GetComponent<Blood>().CurrentBloodPercentage/100;

	}
}
