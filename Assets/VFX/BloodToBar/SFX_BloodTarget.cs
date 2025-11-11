using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_BloodTarget : MonoBehaviour
{
    public GameObject CanvasBloodIconObject;
    public Vector3 CanvasBloodBarPosition;

    // Start is called before the first frame update
    void Start()
    {
        //CanvasBloodBarObject = Component.FindFirstObjectByType<Canvas>().gameObject;
    }

    void MoveToBarPosition()
    {
        CanvasBloodBarPosition = CanvasBloodIconObject.GetComponent<RectTransform>().position;
        Vector3 _canvas_pos = CanvasBloodBarPosition;
        Vector3 _world_pos = Camera.main.ScreenToWorldPoint(_canvas_pos);
        transform.position = _world_pos;
    }

    // Update is called once per frame
    void Update()
    {
        MoveToBarPosition();
    }
}
