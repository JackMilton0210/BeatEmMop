using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] int numberOfEncounters = 3;
    [SerializeField] int CurrentEncounter = 0;
    public List<Encounter> Encounters = new List<Encounter>();
    [SerializeField] Camera activeCamera;
    [SerializeField] GameObject PlayerObject;
    [SerializeField] GameObject BloodObject;
    bool level_finished = false;


    public bool LevelDone()
    {
        Debug.Log(level_finished);
        return level_finished;
    }

    // Start is called before the first frame update
    void Start()
    {

        //Encounter 0
        Encounter encounter0 = gameObject.AddComponent<Encounter>();
        encounter0.AssignLevel(this);
        encounter0.AddEnemy(new Vector2(-16, 0), 0);
        encounter0.AddEnemy(new Vector2(-14, -2), 0);
        Encounters.Add(encounter0);

        //Encounter 1
        Encounter encounter1= gameObject.AddComponent<Encounter>();
        encounter1.AssignLevel(this);
        encounter1.AddEnemy(new Vector2(4, 2), 0);
        encounter1.AddEnemy(new Vector2(5, 0), 0);
        encounter1.AddEnemy(new Vector2(4, -2), 0);
        Encounters.Add(encounter1);

        //Encounter 2
        Encounter encounter2 = gameObject.AddComponent<Encounter>();
        encounter2.AssignLevel(this);
        encounter2.AddEnemy(new Vector2(20, 0), 1);
        Encounters.Add(encounter2);
    }

    // Update is called once per frame
    void Update()
    {

		Debug.Log(CurrentEncounter);

		//When All encounters are complete
		if (CurrentEncounter == numberOfEncounters)
		{
			//End the level
			level_finished = true;
			Debug.Log("FIN");
		}

        //If player is dead, end level
        if (PlayerObject.GetComponent<Health>().alive == false)
        {
            level_finished = true;
        }

        //If blood level reaches max end level
        if (BloodObject.GetComponent<Blood>().AtMaxBlood())
        {
            level_finished = true;
        }

        //If the Camera is Near the Lock Point
        //if (Input.GetKeyDown(KeyCode.M))
        Debug.Log(CurrentEncounter);
		if (activeCamera.GetComponent<CameraManager>().NearLockPoint(CurrentEncounter))
        {
			Debug.Log("setCameraLock");
			Encounters[CurrentEncounter].cameraNearLock = true;
        }

        //If the number of enemies drops below 0 then move to after
		if (Encounters[CurrentEncounter].currentStage == Stage.After)
		{
			CurrentEncounter++;
		}

		

	}
}
