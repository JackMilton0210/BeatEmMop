using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AWW_ThrowStop : AudioWwise
{


	// Update is called once per frame
	void Update()
    {
        
        EnvironmentManager throwables = Component.FindFirstObjectByType<EnvironmentManager>();
        
        
        if (throwables.GetOffscreenIncreased())
            StartCondition = true;

        AudioUpdate();
    }
}
