using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioWwise : MonoBehaviour
{

	//[Header("Wwise Events")]
	[SerializeField] private AK.Wwise.Event AudioEvent;

	[HideInInspector] public bool StartCondition = false;
	[HideInInspector] public bool StartConditionLastFrame = false;
	[HideInInspector] public bool EndCondition = false;

	void StartPlaying()
	{
		AudioEvent.Post(gameObject);
	}
	void EndPlaying()
	{

	}

	protected void HandlePlaying()
	{

		if (StartCondition == true)
		{
			if (StartConditionLastFrame == false)
			{
				StartPlaying();
			}
		}

		StartConditionLastFrame = StartCondition;

	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

	protected void AudioUpdate()
	{
		HandlePlaying();
	}

    // Update is called once per frame
    void Update()
    {
		
    }
}
