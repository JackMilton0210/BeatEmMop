using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AWW_Walking : AudioWwiseTimer
{

    [SerializeField] PlayerController movement;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StartCondition = Component.FindFirstObjectByType<PlayerController>().IsWalking();
        AudioUpdate();
    }
}
