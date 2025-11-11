using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AWW_ThrowableFade : AudioWwise
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		EnvironmentManager throwables = Component.FindFirstObjectByType<EnvironmentManager>();


		if (throwables.GetOffscreenIncreased())
			StartCondition = true;

		AudioUpdate();
	}
}
