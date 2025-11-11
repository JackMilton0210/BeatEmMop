using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using UnityEngine;

public enum EncounterBehaviour { None, Default };
public class EnemyManager : MonoBehaviour
{
    [SerializeField] EncounterBehaviour currentEnemyBehaviours;

    int currentFight = 0;
    List<List<Vector2>> FightSpawns = new List<List<Vector2>>();
    void AddFight() { FightSpawns.Add(new List<Vector2>()); }
    void AddSpawn(int fight, Vector2 pos) { FightSpawns[fight].Add(pos); }


    [SerializeField] GameObject EnemyTemplate;
    [SerializeField] GameObject PlayerObject;
    List<GameObject> enemies = new List<GameObject>();

    public GameObject GetEnemy(int i) { return enemies[i]; }

    [SerializeField] bool DevMode = true;

    public int NumberOfEnemies()
    {
        return enemies.Count;
    }

    public void SpawnEnemy(int enemytype, Vector2 spawnPos)
    {

		//newEnemy.name = "Enemy";
		//newEnemy.
		//enemies.Add(newEnemy);

		GameObject copy = Instantiate(EnemyTemplate, spawnPos, Quaternion.identity);
        enemies.Add(copy);
        if (enemytype == 0)
        {
            copy.GetComponent<Health>().SetMaxHealth(30);
            Debug.Log(copy.GetComponent<Health>().maxHealth);
        }

        if(enemytype == 1)
        {

            //Set Max Health
            copy.GetComponent<Health>().SetMaxHealth(100);

            //Upscale Enemy
            copy.transform.localScale = copy.transform.localScale * 2;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        AddFight();
        AddSpawn(0, new Vector2(-20, 0));
    }

    void UpdateBehaviours()
    {
        //Choose enemy behavious
        switch (currentEnemyBehaviours)
        {
            case EncounterBehaviour.None: break;

            case EncounterBehaviour.Default:
                {
					
					for (int i = 0; i < enemies.Count; i++)
                    {
                        enemies[i].GetComponent<EnemyInput>().currentBehaivour = EnemyBehaviourType.Stationary;
                    }
					enemies[0].GetComponent<EnemyInput>().currentBehaivour = EnemyBehaviourType.MoveToPlayer;
					break;
                }
        }

    }

    //Remove enemies from scene when their health reaches 0
    void UpdateDeath()
    {
		for (int i = 0; i < enemies.Count; i++)
		{
			if (!enemies[i].GetComponent<Health>().alive)
			{
				Destroy(enemies[i]);
				enemies.RemoveAt(i);
			}
		}
	}

    // Update is called once per frame
    void Update()
    {
       
        //Kill All enemies in the current encounter on key press
		if (true)
		{
			if ((Input.GetKeyDown(KeyCode.T) && (DevMode)))
			{
				for(int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].GetComponent<Health>().ApplyDamage(1000);
                }
			}
		}



        UpdateBehaviours();
        UpdateDeath();



		
    }
}
