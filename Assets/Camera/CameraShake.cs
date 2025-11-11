using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 ShakeImpulse = Vector3.zero;
    Vector3 ShakeVector = Vector3.zero;
    [SerializeField] Vector3 DisplayShake;
    public Vector3 GetShakeVector() { return ShakeVector; }

    float ShakeDuration = 0.1f;
    float ShakeTime = 0;

    bool Shaking = false;

    float Max_X = 5;
    float Max_Y = 5;
	EasingFunctions.Ease Ease_X = EasingFunctions.Ease.Linear;
	EasingFunctions.Ease Ease_Y = EasingFunctions.Ease.Linear;

    void GenerateShakeVector(float normalized_time)
    {
        //Time Value between 0-1
        float t = normalized_time;
        
        //Get Decreasing X and Y values
        float x = Max_X - (Max_X * t);
        float y = Max_Y - (Max_Y * t);

        //Clamp
        if (x < 0) x = 0;
        if (y < 0) y = 0;

        //Generate a random Vector2
        Vector2 _rand = new Vector2(Random.Range(-100,100), Random.Range(-100,100));
        //Scale Down to 1
        _rand /= 1000;
        //Normalize
        _rand = _rand / _rand.magnitude;
        //Scale inversely to time
        _rand *= (1-t);



        ShakeImpulse.x = _rand.x/2;
        ShakeImpulse.y = _rand.y/4;
        //ShakeImpulse.y = 0;
        ShakeImpulse.z = 0;

        ShakeVector += ShakeImpulse * 0.1f * (1-t);

    }

	void StartShake()
    {
        Shaking = true;
        ShakeVector = Vector3.zero;
        ShakeTime = 0;
    }
    void UpdateShake()
    {
        if (Shaking)
        {
            ShakeTime += Time.deltaTime;

            GenerateShakeVector(ShakeTime / ShakeDuration);

            if(ShakeTime > ShakeDuration)
            {
                Shaking = false;
                ShakeVector = Vector3.zero;
                ShakeTime = 0;
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

	private void FixedUpdate()
	{
		//Dev Option to shake the camera
		if (Component.FindFirstObjectByType<DevOptions>().GetDevMode())
			if (Input.GetKeyDown(KeyCode.P))
			{
				StartShake();
			}

		

		//Update Camera Shake
		UpdateShake();

		//Update Display Vector
		DisplayShake = ShakeVector;
	}

	// Update is called once per frame
	void Update()
    {
		if (Component.FindFirstObjectByType<PlayerHealth>().DidHealthDropThisFrame())
		{
			StartShake();
		}
	}
}
