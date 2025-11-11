using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    
    List<Vector2> LevelLockPoints = new List<Vector2>();
	public void AddLockPoint(float x, float y)
	{
        LevelLockPoints.Add(new Vector2(x, y));
	}

    [SerializeField] bool CloseToLock = false;
	[SerializeField] [Range(0,2)]  int LockedPoint = 0;
    [SerializeField] float LockPointRange = 2;

    [SerializeField] GameObject level;

    public bool NearLockPoint(int lockPoint)
    {
        float dist = (transform.position.x - LevelLockPoints[lockPoint].x) * (transform.position.x - LevelLockPoints[lockPoint].x);
        
		if (dist < LockPointRange)
        {
			Debug.Log(lockPoint);
			return true;
        }
        else
        {
            return false;
        }
    }

	// Start is called before the first frame update
	void Start()
    {
        GetComponent<CameraController>().SetFollowPlayer();

        AddLockPoint(-20, 0);
        AddLockPoint(0, 0);
        AddLockPoint(20, 0);
        
    }

    void TestMethod()
    {
		//when close to a level lock
		if (CloseToLock)
		{
			GetComponent<CameraController>().SetLocked(LevelLockPoints[LockedPoint]);
		}
		else
		{
			GetComponent<CameraController>().SetFollowPlayer();
		}
	}

    // Update is called once per frame
    void Update()
    {
        for(int i = 0;  i < LevelLockPoints.Count; i++)
        {
            if (NearLockPoint(i))
            {
                GetComponent<CameraController>().SetLocked(LevelLockPoints[i]);
            }
        }

        
        if(level.GetComponent<EnemyManager>().NumberOfEnemies() == 0)
        {
            GetComponent<CameraController>().SetFollowPlayer();
        }

    }
}
