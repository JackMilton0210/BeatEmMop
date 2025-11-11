using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AWW_DeathSFX : AudioWwise
{
    // Update is called once per frame
    void Update()
    {
        StartCondition = Component.FindFirstObjectByType<CharacterHealth>().alive == false;
        AudioUpdate();
    }
}
