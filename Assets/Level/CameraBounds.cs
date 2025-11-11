using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{

    GameObject CameraObject;
    [SerializeField] float ViewWidth = 16;
    [SerializeField] float ViewHeight = 9;
    [SerializeField] Vector3 Offset = Vector3.zero;
    enum Side { Top, Bottom, Left, Right };
    [SerializeField] Side side = Side.Left;

    // Start is called before the first frame update
    void Start()
    {
        CameraObject = Component.FindFirstObjectByType<Camera>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        switch (side)
        {
            case Side.Top:      Offset.x = 0; Offset.y = ViewHeight / 2; break;
            case Side.Bottom:   Offset.x = 0; Offset.y = -ViewHeight / 2;break;
            case Side.Left:     Offset.x = -ViewWidth / 2; Offset.y = 0; break;
            case Side.Right:    Offset.x = ViewWidth / 2; Offset.y = 0; break;
        }
        Offset.z = 1;
        transform.position = CameraObject.transform.position + Offset;
    }
}
