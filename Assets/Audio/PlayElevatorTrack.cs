using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayElevatorTrack : MonoBehaviour
{
    public static uint elevatorTrackID;

    public AK.Wwise.Event startMusic;

    public static bool hasPosted = false;

    // Start is called before the first frame update
    void Start()
    {
        //elevatorTrackID = AkSoundEngine.PostEvent("Play_Elevator_Track_01", gameObject);
        PostMusic();
        AkSoundEngine.SetState("MainStateGroup", "MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PostMusic()
    {
        if (!hasPosted)
        {
            startMusic.Post(gameObject);
            hasPosted = true;
        }
    }
}
