using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnvironmentManager : MonoBehaviour
{
    //Throwables
    [SerializeField] List<GameObject> throwables = new List<GameObject>();
    [SerializeField] int MovingLastFrame;
    [SerializeField] int MovingThisFrame;
    [SerializeField] bool MovingIncreased;
    public bool GetMovingIncreased() { return MovingIncreased; }

    [SerializeField] int CollidedLastFrame;
    [SerializeField] int CollidedThisFrame;
    [SerializeField] bool CollidedIncreased;
    public bool GetCollidedIncreased() { return CollidedIncreased; }

    [SerializeField] int OffscreenLastFrame;
    [SerializeField] int OffscreenThisFrame;
    [SerializeField] bool OffscreenIncreased;
    public bool GetOffscreenIncreased() { return OffscreenIncreased; }




    public void UpdateThrowableHitEnemy()
    {

        int temp = 0;
        for(int i = 0; i < throwables.Count; i++)
        {
            if (throwables[i].GetComponent<EnvironmentThrowable>().EnemyHitByThrowable())
                temp++;
        }
        CollidedThisFrame = temp;

        CollidedIncreased = (CollidedThisFrame > CollidedLastFrame);

        CollidedLastFrame = CollidedThisFrame;

    }

    public void UpdateOffscreen()
    {
        int temp = 0;
        for(int i = 0; i < throwables.Count; i++)
        {
            if (throwables[i].GetComponent<EnvironmentThrowable>().IsOffscreen())
                temp++;
        }
        OffscreenThisFrame = temp;

        OffscreenIncreased = (OffscreenThisFrame > OffscreenLastFrame);

        OffscreenLastFrame = OffscreenThisFrame;

    }
    
    public void UpdateMoving()
    {
        //Update the number of throwables moving this frame
        int temp = 0;
        for(int i = 0; i < throwables.Count; i++)
        {
            if (throwables[i].GetComponent<EnvironmentThrowable>().IsMoving())
                temp++;
        }
        MovingThisFrame = temp;

        //Compare with last frame
        MovingIncreased = (MovingThisFrame > MovingLastFrame);

        //Update new Last frame
        MovingLastFrame = MovingThisFrame;
    }

    void AddChildObjectsToThrowablesList()
    {
        throwables.Clear();
		for (int i = 0; i < transform.childCount; i++)
		{
			throwables.Add(transform.GetChild(i).gameObject);
		}
	}

    // Start is called before the first frame update
    void Start()
    {
        AddChildObjectsToThrowablesList();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateThrowableHitEnemy();
        UpdateMoving();
        UpdateOffscreen();

        if (MovingIncreased)
            Debug.Log("MovingIncresed");

        if (CollidedIncreased)
            Debug.Log("CollidedIncreased");

        if (OffscreenIncreased)
            Debug.Log("OffscreenIncreased");

     
    }
}
