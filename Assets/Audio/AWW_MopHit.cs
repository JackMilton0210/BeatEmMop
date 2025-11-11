using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AWW_MopHit : AudioWwise
{
    //Attack Component
    [SerializeField] PlayerCombat attackComp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCondition = Component.FindFirstObjectByType<PlayerCombat>().GetHitCollision();
        AudioUpdate();
    }
}
