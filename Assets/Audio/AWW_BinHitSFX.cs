using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AWW_BinHitSFX : AudioWwise
{
    public AK.Wwise.Event stopLoop;

    // Update is called once per frame
    void Update()
    {
        StartCondition = Component.FindFirstObjectByType<EnvironmentManager>().GetCollidedIncreased();
        if (Component.FindFirstObjectByType<EnvironmentManager>().GetCollidedIncreased())
        {
            stopLoop.Post(gameObject);
        }
        AudioUpdate();
    }
}
