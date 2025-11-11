using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioWwiseTimer : MonoBehaviour
{

	[SerializeField] private AK.Wwise.Event AudioEvent;

	[HideInInspector] public bool StartCondition = false;

	private bool Playing = false;
	[SerializeField] private float PlayTime = 1;
	private float CurrentTime = 0;

	void StartPlaying()
	{
		AudioEvent.Post(gameObject);
	}

	protected void HandlePlaying()
	{
		if (StartCondition)
		{
			if(CurrentTime >= PlayTime)
			{
				StartPlaying();
				CurrentTime = 0;
			}
		}


		CurrentTime += Time.deltaTime;
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
