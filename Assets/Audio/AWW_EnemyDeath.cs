using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AWW_EnemyDeath : AudioWwise
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCondition = Component.FindFirstObjectByType<EnemyHandler>().GetEnemyDied();
        AudioUpdate();
	}
}
