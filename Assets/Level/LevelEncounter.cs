using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEncounter : MonoBehaviour
{
    //Objects & Components
    GameObject PlayerObject;
    LevelCamera levelCam;

    //Parent Level
    public GameLevel ParentLevel;

    //Progression State Of the Encounter
    public enum EncounterState { Waiting, Entered, InProgress, Completed, Failed, Exit };
    [SerializeField] private EncounterState currentEncounterState = EncounterState.Waiting;
    public EncounterState GetCurrentState() { return currentEncounterState; }

    bool VerticalEncounter = false;
    public void SetVertical(bool arg_bool) { VerticalEncounter = arg_bool; }


    //Encounter Data
    Vector3 EncounterPosition = Vector3.zero;            //Position of the Encounter Within the Scene
    float StartRange = 1;
    Vector3 CameraLockPoint = Vector3.zero;             //Position of the Camera Lock Point Relative to Encounter
    //Constructor
    public void Construct(Vector3 arg_encounterPos, float arg_startRange, Vector3 arg_lockPoint)
    {
        EncounterPosition = arg_encounterPos;
        StartRange = arg_startRange;
        CameraLockPoint = arg_lockPoint;
    }

    List<Vector3> EnemySpawns = new List<Vector3>();    //List of Enemy Spawn Data
    public void AddEnemy(Vector3 arg_spawnPos)
    {
        EnemySpawns.Add(arg_spawnPos);
    }







    //Testing
    [SerializeField] bool Test_EnemiesDead = false;
    [SerializeField] bool Test_PlayerDead = false;



    /// <summary>
    /// Base Functions
    /// Functions Used to Calculate Variables or execute orders required by the encounter
    /// </summary>
    

    //Returns true if the player is within the encounter range
	bool PlayerNear()
	{
		//Vars
		Vector3 _player_pos = PlayerObject.transform.position;
		Vector3 _encounter_pos = EncounterPosition;
		float _range = StartRange;

		//Calculate Vars
		Vector3 _distance = Vector2.zero;
		_distance.x = _player_pos.x - _encounter_pos.x;
		_distance.z = _player_pos.z - _encounter_pos.z;
		float _mag = _distance.magnitude;

		//If Player Distance is less than encounter range then start the encounter
		return (_mag < _range);

	}
    //Spawns Enemies according to the spawn Data
    void SpawnEnemies()
    {
        for(int i = 0; i < EnemySpawns.Count; i++)
        {
            //Spawn enemy at pos
            GetComponent<EnemyHandler>().SpawnEnemy(EncounterPosition + EnemySpawns[i]);
        }
    }
    //Returns True when there are no more enemies alive in the encounter
    bool EnemiesDefeated()
    {
        //return Test_EnemiesDead;
        return GetComponent<EnemyHandler>().AllEnemiesDefeated();
    }
    //Returns true when the player has been killed
    bool PlayerDefeated()
    {
        return Test_PlayerDead;
    }
    //Lock the Camera to this encounters lock position
    void LockCamera()
    {
        levelCam.SetCurrentLockPosition(EncounterPosition + CameraLockPoint);
        levelCam.SetCameraState(LevelCamera.CameraState.Locked);
	}
    //Unlock the camrea from the level lock point
    void UnlockCamera()
    {
        levelCam.SetCameraState(LevelCamera.CameraState.FollowPlayer);
    }
    //
    void DownPhaseCamera()
    {
        levelCam.SetCameraState(LevelCamera.CameraState.DownPhase);
    }






    /// <summary>
    /// State Functions
    /// One function for each state in the encounter state machine
    /// </summary>

	//Once player is in range move to the Entered State
	void StateWaiting()
	{
		//Once the player is close enough, Switch to Entered State 
		if (PlayerNear()) currentEncounterState = EncounterState.Entered;
	}
	//Spawn the Encounters Enemies then move to InProgress State
	void StateEntered()
	{
        //Lock Camera
        LockCamera();

        //If the encounter is vertical then set camera to DownPhase
        if (VerticalEncounter)
            DownPhaseCamera();

		//Spawn Enemies
		SpawnEnemies();
		//Move to InProgress State
		currentEncounterState = EncounterState.InProgress;
	}
    //Main loop of the encounter
    void StateInProgress()
    {
        //If there are no more enemies left in the encounter then move to completed state
        if (EnemiesDefeated()) currentEncounterState = EncounterState.Completed;

        //If the player is killed then move to the Failed State
        if (PlayerDefeated()) currentEncounterState = EncounterState.Failed;

    }
    //
    void StateCompleted()
    {
        //Unlock Camera
        UnlockCamera();
        //Move to Exit State
        currentEncounterState = EncounterState.Exit;
    }
    void StateFailed()
    {
        
    }
    void StateExit()
    {

    }

	//Update the Encounter State Based on Current Game Data
	void HandleState()
    {
        switch (currentEncounterState)
        {
            case EncounterState.Waiting:    StateWaiting();     break;
            case EncounterState.Entered:    StateEntered();     break;
            case EncounterState.InProgress: StateInProgress();  break;
            case EncounterState.Completed:  StateCompleted();   break;
            case EncounterState.Failed:     StateFailed();      break;
            case EncounterState.Exit:       StateExit();        break;
                
        }
    }

    public void UpdateEncounter()
    {
        HandleState();
    }

    
    
    



    //Get Objects and Components Used by this Component
    void Setup()
    {
        PlayerObject = Component.FindFirstObjectByType<PlayerComponent>().gameObject;
        levelCam = Component.FindFirstObjectByType<LevelCamera>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateEncounter();
    }
}
