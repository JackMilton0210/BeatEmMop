using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VFX_Effect : MonoBehaviour
{
    float Timer = 0;
    float Duration = (float)14.0f / 24.0f;
    enum VFX_State { Unstarted, Active, Finished }
    VFX_State vfxState = VFX_State.Unstarted;
    public enum FX_Rotation { None, Left, Right };
    FX_Rotation fxRotation = FX_Rotation.None;


    public void Spawn()
    {
        vfxState = VFX_State.Active;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    public void Despawn()
    {
        vfxState = VFX_State.Finished;
        GetComponent<SpriteRenderer>().color = Color.clear;
    }
    public bool isFinished()
    {
        if (vfxState == VFX_State.Finished)
            return true;
        return false;
    }

    public void SetRotation(Direction direction)
    {
        //Set the rotation of the effect
        switch (direction)
        {

            case Direction.Left:
                {
                    Vector3 _rot = transform.rotation.eulerAngles;
                    _rot.z = 90;
                    transform.rotation = Quaternion.Euler(_rot);
                    break;
                }
            case Direction.Right:
                {
                    Vector3 _rot = transform.rotation.eulerAngles;
                    _rot.z = -90;
                    transform.rotation = Quaternion.Euler(_rot);
                    break;
                }
        }
    }



    public void SetRotation(int rot_int)
    {
        switch (rot_int)
        {
            case 0: fxRotation = FX_Rotation.None; break;
            case 1: fxRotation = FX_Rotation.Left; break;
            case 2: fxRotation = FX_Rotation.Right; break;
        }

        //Set the rotation of the effect
        switch (fxRotation)
        {
            case FX_Rotation.Left:
                {
                    Vector3 _rot = transform.rotation.eulerAngles;
                    _rot.z = 90;
                    transform.rotation = Quaternion.Euler(_rot);
                    break;
                }
            case FX_Rotation.Right:
                {
                    Vector3 _rot = transform.rotation.eulerAngles;
                    _rot.z = -90;
                    transform.rotation = Quaternion.Euler(_rot);
                    break;
                }
        }
    }
    void HandleDuration()
    {
        if (vfxState == VFX_State.Active)
        {
            Timer += Time.deltaTime;
            if (Timer > Duration)
            {
                Despawn();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleDuration();
    }
}

