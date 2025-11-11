using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

enum CameraState { Trailing, Locked }
public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject PlayerObject;
    [SerializeField] float MoveSpeed = 3;
	[SerializeField] CameraState currentState;
    [SerializeField] Vector2 PlayerOffset = new Vector2(5, 0);

	Vector2 TargetPosition = Vector2.zero;
    [SerializeField] Vector2 LockPosition = Vector2.zero;

    // Move Speed Modifier Vars
    [SerializeField] float ms_minimum = 0.05f;
    [SerializeField] float ms_maximum = 1;
    [SerializeField] float ms_distanceRange = 4;
    


    void FollowPlayer()
    {
        Vector2 offsetPlayerPos = new Vector2(
            PlayerObject.transform.position.x + PlayerOffset.x,
            PlayerObject.transform.position.y + PlayerOffset.y
            );
        transform.position = MoveToTarget(transform.position, offsetPlayerPos);
    }

    void Lock()
    {
        transform.position = MoveToTarget(transform.position, LockPosition);
    }

    Vector3 MoveToTarget(Vector2 currentPos, Vector2 targetPos)
    {
        Vector3 returnPos = currentPos;
        returnPos.z = -10;

        //Get current movespeed based on distance to the target position
        float ScaledMoveSpeed = MoveSpeed;
        float distanceToTarget = (targetPos.x - currentPos.x) * (targetPos.x - currentPos.x);

		//if (distanceToTarget < movespeedlimit)
        {
            ScaledMoveSpeed = MoveSpeed * Mathf.Lerp(ms_minimum, ms_maximum, distanceToTarget / ms_distanceRange);
        } 

        if(currentPos.x < targetPos.x)
        {

            if (currentPos.x + (ScaledMoveSpeed * Time.deltaTime) > targetPos.x)
            {
               returnPos.x = targetPos.x;
            }
            else
            {
                returnPos.x = currentPos.x + (ScaledMoveSpeed * Time.deltaTime);
            }

        }
        else if (currentPos.x > targetPos.x)
        {
            if(currentPos.x - (ScaledMoveSpeed *Time.deltaTime) < targetPos.x)
            {
                returnPos.x = targetPos.x;
            }
            else
            {
				returnPos.x = currentPos.x - (ScaledMoveSpeed * Time.deltaTime);
			}
            
        }
        
        return returnPos;
    }

    // Start is called before the first frame update
    void Start()
    {
        


    }

    public void SetFollowPlayer()
    {
        currentState = CameraState.Trailing;
    }
    public void SetLocked(Vector2 newLockPos)
    {
        currentState = CameraState.Locked;
        LockPosition = newLockPos;
    }

    void ApplyShake(Vector3 shake)
    {
        Vector3 _pos = transform.position;
        _pos += shake;
        transform.position = _pos;
    
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Camera Update Start");
        switch (currentState)
        {
            case CameraState.Locked:
                {
                    Lock();
                    break;
                }
            case CameraState.Trailing:
                {
                    FollowPlayer();
                    break;
                }
        }
        Debug.Log("Camera Update End");
        ApplyShake(GetComponent<CameraShake>().GetShakeVector());

    }
}
