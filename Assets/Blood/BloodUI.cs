using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodUI : MonoBehaviour
{

    [SerializeField] GameObject CollectedBloodObject;
    CollectedBlood m_CollectedBlood;
    Slider m_Slider;

    

    // Start is called before the first frame update
    void Start()
    {
        m_CollectedBlood = CollectedBloodObject.GetComponent<CollectedBlood>();
        m_Slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {

        m_Slider.value = m_CollectedBlood.CurrentBlood / m_CollectedBlood.MaxBlood;

    }
}
