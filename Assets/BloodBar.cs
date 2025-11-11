using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBar : MonoBehaviour
{

    Blood bloodComp;
    float MaxScale;

    void GetComponents()
    {
        bloodComp = GameObject.FindWithTag("BloodObject").GetComponent<Blood>();
        if (!bloodComp) Debug.Log("Error - BloodBar::Get BloodComponent");
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponents();
        MaxScale = transform.localScale.y;

    }

    void ScaleBar()
    {
        Vector3 newScale = transform.localScale;
        newScale.y = bloodComp.CurrentBloodPercentage * MaxScale;
        transform.localScale = newScale;

    }

    // Update is called once per frame
    void Update()
    {
        


    }
}
