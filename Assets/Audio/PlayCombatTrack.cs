using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCombatTrack : MonoBehaviour
{
    public uint combatTrackID;

    // Start is called before the first frame update
    void Start()
    {
        combatTrackID = AkSoundEngine.PostEvent("Play_150BPM", gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
