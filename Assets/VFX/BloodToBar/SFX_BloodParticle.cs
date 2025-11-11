using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_BloodParticle : MonoBehaviour
{

    public GameObject targetObject;
    private Vector3 targetPosition;
    public float MoveSpeed;
    public bool Moving = false;
    public Vector3 TargetOffset = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Moving)
        {
			targetPosition = targetObject.transform.position + TargetOffset;
			float deltatime = Time.deltaTime;

            Vector3 directionToMove = targetPosition - transform.position;
            directionToMove = directionToMove.normalized * deltatime * MoveSpeed;
            float MaxDistance = Vector3.Distance(transform.position, targetPosition);
            transform.position = transform.position + Vector3.ClampMagnitude(directionToMove, MaxDistance);
            if (transform.position == targetPosition)
            {
                Destroy(gameObject);
            }
        }
    }
}
