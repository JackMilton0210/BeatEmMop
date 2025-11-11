using NUnit.Framework.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CreditsScript : MonoBehaviour
{

    bool VideoStarted = false;
    bool VideoFinished = false;

    string nextSceneString = "MainMenu";

    bool IsVideoStarted()
    {
        if (GetComponent<VideoPlayer>().isPlaying)
            return true;
        return false;
    }

    bool IsVideoFinished()
    {
        if (GetComponent<VideoPlayer>().isPlaying)
            return false;
        return true;
    }

    void MoveToNextScene()
    {
        SceneManager.LoadScene(nextSceneString);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!VideoStarted)
            VideoStarted = IsVideoStarted();


        if (VideoStarted)
            VideoFinished = IsVideoFinished();


        if (VideoStarted)
            if (VideoFinished)
            {
                MoveToNextScene();
            }

    }
}
