using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField] GameObject EnemyTemplate;
    [SerializeField] List<GameObject> Enemies = new List<GameObject>();
    [SerializeField] Sprite _square_sprite;
    [SerializeField] RuntimeAnimatorController _animator_controller;

    public bool GetEnemyExists(int i) {
        if(Enemies != null)
            if(Enemies.Count >= i + 1)
                //if (Enemies[i] != null) 
                return true; 
        return false;
    }
    public GameObject GetEnemy(int i) { return Enemies[i]; }


    public RuntimeAnimatorController GetAnimatorController() { return _animator_controller; }

    float EnemyDepth = 5;

    //True if an enemy died this frame
    bool EnemyDied = false;
    public bool GetEnemyDied() { return EnemyDied; }
    float EnemyDiedTimer = 0;
    float EnemyDiedDuration = 0.1f;

    GameObject PlayerObject;

    public bool AllEnemiesDefeated()
    {
        if (Enemies.Count <= 0)
            return true;
        return false;
    }

    //Spawn an enemy at the desired location
    public void SpawnEnemy(Vector2 arg_spawnPos)
    {
       
        //Create GameObject
	    GameObject _new_enemy = new GameObject();

        //Add EnemyObject Tag
        _new_enemy.tag = "EnemyObject";

		//Add Components
		_new_enemy.AddComponent<BoxCollider2D>();
        _new_enemy.AddComponent<SpriteRenderer>();
		_new_enemy.AddComponent<EnemyComponent>();

		//Modify HitBox Size
		_new_enemy.GetComponent<BoxCollider2D>().size = new Vector2(2, 6);

        //Change Object Name
        _new_enemy.name = "Enemy " + (Enemies.Count + 1);

        //Set Enemy Position
        _new_enemy.transform.position = new Vector3(arg_spawnPos.x, arg_spawnPos.y, EnemyDepth);

		//Add GameObject to the enemy list
		Enemies.Add(_new_enemy);


		//__________________MODIFY VISUALS WILL NEED TO CHANGE
		_new_enemy.GetComponent<SpriteRenderer>().sprite = _square_sprite;
		_new_enemy.GetComponent<Transform>().localScale = new Vector3(0.41f, 0.41f, 0.41f);
		//-----------------
	}


	//Update Enemy Behaviours
	private void HandleEnemies()
    {
        for(int i = 0; i < Enemies.Count; i++)
        {
            //Set Enemy
            GameObject _enemy = Enemies[i];

			

            //Handle Enemy Behaviour
			_enemy.GetComponent<EnemyInputHandler>().SetCurrentBehaviour(EnemyInputHandler.EnemyBehaviour.HalfApproachPlayer); 
            if (i == 0)
			    _enemy.GetComponent<EnemyInputHandler>().SetCurrentBehaviour(EnemyInputHandler.EnemyBehaviour.ApproachPlayer);



            //Handle Death
            //if(_enemy.GetComponent<EnemyHealth>().IsAlive() == false)
            if(_enemy.GetComponent<CharacterController>().IsKnockStateStandingUp())
            {
                Enemies.Remove(_enemy);
                _enemy.gameObject.SetActive(false);
                //Destroy(_enemy);
                EnemyDied = true;
            }
                

		}
    }

    public void KillAllEnemies()
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].GetComponent<EnemyHealth>().DamageHealth(1000);
            Enemies[i].GetComponent<EnemyHealth>().DamageStance(1000);
        }
    }

	// Start is called before the first frame update
	void Start()
    {
        PlayerObject = Component.FindFirstObjectByType<PlayerComponent>().gameObject;
    }

    void HandleEnemyDiedTimer()
    {
        if (EnemyDied)
        {
            EnemyDiedTimer += Time.deltaTime;
            if(EnemyDiedTimer >= EnemyDiedDuration)
            {
                EnemyDied = false;
                EnemyDiedTimer = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleEnemies();
        HandleEnemyDiedTimer();
        
        
        
        //Spawn Enemy On Key Press
        if (Input.GetKeyDown(KeyCode.H))
        {
            SpawnEnemy(new Vector2(PlayerObject.transform.position.x + 5, 0));
        }   
    }
}
