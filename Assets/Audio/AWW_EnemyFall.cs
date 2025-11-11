using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AWW_EnemyFall : AudioWwise
{
    // Update is called once per frame
    void Update()
    {
        StartCondition = Component.FindFirstObjectByType<EnvironmentManager>().GetCollidedIncreased();
        AudioUpdate();
    }
}
