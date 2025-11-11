using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stage { Before, During, After };
public class Encounter : MonoBehaviour
{
    public Vector2 CameraLockPoint;
    Vector2 CameraLockRange;
    List<EnemyData> SpawnData = new List<EnemyData>();
    public Stage currentStage = Stage.Before;
    Level level;

    public bool cameraNearLock = false;

    public void AssignLevel(Level arg_level)
    {
        level = arg_level;
    }

    public struct EnemyData
    {
        public EnemyData(int type, Vector2 spawn) { EnemyType = type; SpawnPos = spawn; }
        public int EnemyType;
        public Vector2 SpawnPos;
    }
    

    public void AddEnemy(Vector2 spawnPos, int enemyType)
    {
        SpawnData.Add(new EnemyData(enemyType, spawnPos));
    }

    // Start is called before the first frame update
    void Start()
    {
            
        //AddEnemy(new Vector2(-15, 0), 0);
        //AddEnemy(new Vector2(-13, -2), 0);
        //Debug.Log("EnemyAdded");
        
    }

    // Update is called once per frame
    void Update()
    {

        //If in before stage
        if (currentStage == Stage.Before)
        {
			//If camera near lock
			if (cameraNearLock)
            {
                //Lock Camera

                //Set Current Stage
                currentStage = Stage.During;

                //Spawn Enemies
                for (int i = 0; i < SpawnData.Count; i++)
                {
                    GetComponent<EnemyManager>().SpawnEnemy(SpawnData[i].EnemyType, SpawnData[i].SpawnPos);
                }
            }
        }

        //if stage during
        if(currentStage == Stage.During)
        {
            //If current enemies == 0
            if (GetComponent<EnemyManager>().NumberOfEnemies() == 0)
            {
                currentStage = Stage.After;
            }
        }

        if (currentStage == Stage.After)
        {
            //unlock camera



        }
    }
}
