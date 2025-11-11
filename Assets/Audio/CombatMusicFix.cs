using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatMusicFix : MonoBehaviour
{
    public AK.Wwise.Event stopElevator;
    public AK.Wwise.Event stopCombat;

    public PlayCombatTrack combatTrack;

    private void Awake()
    {
        //stopCombat.Post(gameObject);
        //stopElevator.Post(gameObject);

        //AkSoundEngine.StopPlayingID(PlayElevatorTrack.elevatorTrackID);
        //AkSoundEngine.StopPlayingID(combatTrack.combatTrackID);
    }

    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.SetState("MainStateGroup", "Combat");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
