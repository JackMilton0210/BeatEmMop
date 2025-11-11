using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_1 : GameLevel
{
	override protected void AddEncounters ()
    {
		//First Encounter
		LevelEncounter encounter0 = gameObject.AddComponent<LevelEncounter>();
		encounter0.ParentLevel = this;
		encounter0.Construct(new Vector3(5, 0, 0), 1, new Vector3(0, 0, 0));
		encounter0.AddEnemy(new Vector3(3, -0.5f, 0));
		encounters.Add(encounter0);


		//Second Encounter - Three Enemies
		LevelEncounter encounter1 = gameObject.AddComponent<LevelEncounter>();
		encounter1.Construct(new Vector3(10, 0, 0), 1, new Vector3(0, 0, 0));
		encounter1.ParentLevel = this;
		encounter1.AddEnemy(new Vector3(5, -1.5f, 0));
		encounter1.AddEnemy(new Vector3(6, -1, 0));
		encounter1.AddEnemy(new Vector3(7, 0, 0));
		encounters.Add(encounter1);


		//Third Encounter - Enemy Spawns Behind the Player
		LevelEncounter encounter2 = gameObject.AddComponent<LevelEncounter>();
		encounter2.Construct(new Vector3(20, 0, 0), 1, new Vector3(0, 0, 0));
		encounter2.ParentLevel = this;
		encounter2.AddEnemy(new Vector3(5, 0, -0.5f));
		encounter2.AddEnemy(new Vector3(-7, -0.5f));
		encounters.Add(encounter2);


		//Fourth Encounter - Vertical Hallway
		LevelEncounter encounter3 = gameObject.AddComponent<LevelEncounter>();
		encounter3.Construct(new Vector3(40, 0, 0), 5, new Vector3(0, 0, 0));
		encounter3.ParentLevel = this;
		encounter3.SetVertical(true);
		encounter3.AddEnemy(new Vector3(2, -2, 0));
		encounter3.AddEnemy(new Vector3(6, -7, 0));
		encounter3.AddEnemy(new Vector3(6, -13, 0));
		encounters.Add(encounter3);

		//Fifth Encounter - After Hallway
		LevelEncounter encounter4 = gameObject.AddComponent<LevelEncounter>();
		encounter4.Construct(new Vector3(50, -10, 0), 5, new Vector3(0, 0, 0));
		encounter4.ParentLevel = this;
		encounter4.AddEnemy(new Vector3(5, 0, -2));
		encounter4.AddEnemy(new Vector3(7, -1, 0));
		encounter4.AddEnemy(new Vector3(8.5f, 1, -3));
		encounters.Add(encounter4);


		//Sizth Encounter - Final Encounter
		LevelEncounter encounter5 = gameObject.AddComponent<LevelEncounter>();
		encounter5.Construct(new Vector3(65, -10, 0), 3, new Vector3(0, 0, 0));
		encounter5.ParentLevel = this;
		encounter5.AddEnemy(new Vector3(8, -2, 0));
		encounter5.AddEnemy(new Vector3(6, -4, 0));
		encounter5.AddEnemy(new Vector3(5, 0, -1));
		encounter5.AddEnemy(new Vector3(-8, -2, 0));
		encounter5.AddEnemy(new Vector3(-6, -4, 0));
		encounter5.AddEnemy(new Vector3(-5, 0, -1));
		encounters.Add(encounter5);
	}

}
