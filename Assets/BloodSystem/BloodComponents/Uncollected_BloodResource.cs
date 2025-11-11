using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uncollected_BloodResource : BloodResource
{
    // Start is called before the first frame update
    void Start()
    {
		Min = 0;
		Max = 10;
		Current = 0;
	}

    // Update is called once per frame
    void Update()
    {
        Limit();
    }
}
