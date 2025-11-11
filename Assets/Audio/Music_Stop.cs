using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_Stop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.SetState("MainStateGroup", "None");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
