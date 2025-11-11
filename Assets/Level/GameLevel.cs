using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLevel : MonoBehaviour
{
    //Objects and Components
    public LevelCamera levelCamera;



    //Encounters and Level State
    public int NumberOfCurrentEncounter = 0;                                                    //Number of the currently active encounter
    [SerializeField] protected List<LevelEncounter> encounters = new List<LevelEncounter>();    //List of encounters in the level
    LevelEncounter CurrentEncounter() { return encounters[NumberOfCurrentEncounter]; }          //Returns the currently active encounter
    
    public enum LevelState { Start, Free, InEncounter, Completed, Failed };             //Level States
    [SerializeField] LevelState currentLevelState = LevelState.Start;                                    //Current Level State


	/// <summary>
	/// Basic Functions
	/// 
	/// </summary>
    
    //Return true if the current encounter has been completed successfully
    bool CurrentEncounterCompleted()
    {
        if (CurrentEncounter().GetCurrentState() == LevelEncounter.EncounterState.Exit)
            return true;
        return false;
    }
    //Returns true if the current encounter has started
    bool EncounterStarted()
    {
        if (CurrentEncounter().GetCurrentState() == LevelEncounter.EncounterState.Entered)
            return true;
        if (CurrentEncounter().GetCurrentState() == LevelEncounter.EncounterState.InProgress)
            return true;
        return false;
	}
    //Return True if CurrentEncounterNumber has reached the end of the encounter list
    bool LastEncounterFinished()
    {
        if (NumberOfCurrentEncounter >= encounters.Count )
            return true;

        return false;

        
    }

    bool PlayerDefeated()
    {
        //GetPlayerHealth
        //return !Component.FindFirstObjectByType<PlayerHealth>().IsAlive();
        if (Component.FindFirstObjectByType<PlayerController>().IsKnocked())
            SetFadeIn();
        return Component.FindFirstObjectByType<PlayerController>().IsKnockStateStandingUp();
    }


	/// <summary>
	/// State Functions
	/// Behaviour of the level within each state
	/// 
	/// </summary>
    
    
    void StateStarted()
    {
        //Set CurrentEncounter to 0
        NumberOfCurrentEncounter = 0;
		//Move to Free state
		currentLevelState = LevelState.Free;
    }
    void StateFree()
    {
        if (LastEncounterFinished())
            currentLevelState = LevelState.Completed;

        if (EncounterStarted())
            currentLevelState = LevelState.InEncounter;

    }
    void StateInEncounter()
    {
        //Camera Locks to the Encounter's CameraLockPoint
        //If the current encounter has been completed
        if (CurrentEncounterCompleted())
        {
            //Move to Free State
            currentLevelState = LevelState.Free;
            //Move to Next encounter
            NumberOfCurrentEncounter++;
        }

        if (PlayerDefeated())
        {
            currentLevelState = LevelState.Failed;
        }

	}
    void StateCompleted()
    {
        SceneManager.LoadSceneAsync("WinScreen");
    }
    //On Player Death
    void StateFailed()
    {
        SceneManager.LoadSceneAsync("DeathScreen");
    }

	void HandleState()
    {

        switch (currentLevelState)
        {
            case LevelState.Start: StateStarted();  break;
            case LevelState.Free: StateFree(); break;
            case LevelState.InEncounter: StateInEncounter(); break;
            case LevelState.Completed: StateCompleted(); break;
            case LevelState.Failed: StateFailed(); break;

        }
    }

    public bool IsFree() { if (currentLevelState == LevelState.Free) return true; return false; }

    void UpdateCurrentEncounter()
    {
        //Update the current encounter
        if (NumberOfCurrentEncounter < encounters.Count)
            encounters[NumberOfCurrentEncounter].UpdateEncounter();
        else
            Debug.Log("Error:\tGameLevel::UpdateCurrentEncounter:\tOutside Of Encounter Range");
    }


    void SetFadeIn()
    {
        Component.FindFirstObjectByType<UI_Fader>().SetFadeIn();
    }
    void SetFadeOut()
    {
        Component.FindFirstObjectByType<UI_Fader>().SetFadeOut();
    }
    

    protected void Setup()
    {
        levelCamera = Component.FindFirstObjectByType<LevelCamera>();
    }

    protected virtual void AddEncounters()
    {
	}

	// Start is called before the first frame update
	protected void Start()
    {
        Setup();
        AddEncounters();
        SetFadeOut();
    }

    // Update is called once per frame
    protected void Update()
    {
		//Update the level state
		HandleState();
		//Update the current encounter
		UpdateCurrentEncounter();
        

    }
}
