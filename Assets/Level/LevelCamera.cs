using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCamera : MonoBehaviour
{

	//Objects and Components
	GameObject PlayerObject;

	//Camera State
	public enum CameraState { FollowPlayer, Locked, DownPhase };
    [SerializeField] CameraState currentCameraState = CameraState.FollowPlayer;
    public void SetCameraState(CameraState arg_state) { currentCameraState = arg_state; }

    public bool IsCameraLocked() { if (currentCameraState == CameraState.Locked) return true; return false; }

    //Camera Variables
    [SerializeField] float TrailSpeed = 1;
    [SerializeField] float TrailEaseDistance = 5;
	[SerializeField] EasingFunctions.Ease TrailEase = EasingFunctions.Ease.Linear;
	[SerializeField] float LockSpeed = 1;
	[SerializeField] float LockEaseDistance = 5;
	[SerializeField] EasingFunctions.Ease LockEase = EasingFunctions.Ease.Linear;
    [SerializeField] float DownSpeed = 1;
    [SerializeField] float DownEaseDistance = 5;
    [SerializeField] EasingFunctions.Ease DownEase = EasingFunctions.Ease.Linear;

	//Level Data
	[SerializeField] Vector3 CurrentLockPosition = Vector3.one;
    public void SetCurrentLockPosition(Vector3 arg_lockPos) { CurrentLockPosition = arg_lockPos; }

    //Height the camera should be at when trailing the player
    float ActiveHeight = -0.44f;

    /// <summary>
    /// Basic Functions
    /// </summary>
    
    void MoveTowards(Vector3 arg_currentPos, Vector3 arg_targetPos, float arg_speed, EasingFunctions.Ease arg_easeFunc, float arg_easeDist)
    {
        //Vars
        float _current = arg_currentPos.x;
        float _target = arg_targetPos.x;
        float _speed = arg_speed;
        float _easeDist = arg_easeDist;
        EasingFunctions.Ease _easeFunc = arg_easeFunc;

        //Calculate Vars
        float _distance_to_target = Mathf.Sqrt(Mathf.Pow(_current - _target, 2));
        float _speed_mod = EasingFunctions.ApplyEase(_easeFunc, 0, _easeDist, _distance_to_target);
        float _new_speed = _speed * _speed_mod;

        float _new_pos = Mathf.MoveTowards(_current, _target, _new_speed * Time.deltaTime);
        

        //Apply
        Vector3 return_pos = transform.position;
        return_pos.x = _new_pos;
        transform.position = return_pos;
    }
	void MoveTowardsDown(Vector3 arg_currentPos, Vector3 arg_targetPos, float arg_vert_speed, EasingFunctions.Ease arg_easeFunc, float arg_easeDist)
	{

		//Vars
		float _current = arg_currentPos.y;
		float _target = arg_targetPos.y;
		float _speed = arg_vert_speed;
		float _easeDist = arg_easeDist;
		EasingFunctions.Ease _easeFunc = arg_easeFunc;

		//Calculate Vars
		float _distance_to_target = Mathf.Sqrt(Mathf.Pow(_current - _target, 2));
		float _speed_mod = EasingFunctions.ApplyEase(_easeFunc, 0, _easeDist, _distance_to_target);
		float _new_speed = _speed * _speed_mod;

		float _new_pos = Mathf.MoveTowards(_current, _target, _new_speed * Time.deltaTime);


		//Apply
		Vector3 return_pos = transform.position;
		return_pos.y = _new_pos;
		transform.position = return_pos;

	}

	void TrailPlayer()
    {
        //Vars
        Vector3 _camera_pos = transform.position;
        Vector3 _player_pos = PlayerObject.transform.position;
        float _move_speed = TrailSpeed;
        float _easeDist = TrailEaseDistance;
        EasingFunctions.Ease _ease = TrailEase;

        //Move
        MoveTowards(_camera_pos, _player_pos, _move_speed, _ease, _easeDist);
		MoveTowardsDown(_camera_pos, new Vector3(0,ActiveHeight,0), _move_speed, _ease, _easeDist);
	}

    void DownPhase()
    {
        
        Vector3 _camera_pos = transform.position;
        Vector3 _player_pos = PlayerObject.transform.position;

		//Horizontal Vars
		float _horz_move_speed = TrailSpeed;
        float _ease_horz_dist = TrailEaseDistance;
        EasingFunctions.Ease _horz_ease = TrailEase;
        MoveTowards(_camera_pos, _player_pos, _horz_move_speed, _horz_ease, _ease_horz_dist);

        //Vertical Vars
        float _vert_move_speed = DownSpeed;
        float _ease_vert_dist = DownEaseDistance;
        EasingFunctions.Ease _vert_ease = DownEase;
        MoveTowardsDown(_camera_pos, _player_pos, _vert_move_speed, _vert_ease, _ease_vert_dist);

        ActiveHeight = -10;

    }
    
    void Lock()
    {
        //Vars
        Vector3 _camera_pos = transform.position;
        Vector3 _lock_pos = CurrentLockPosition;
        float _move_speed = LockSpeed;
        float _easeDist = LockEaseDistance;
        EasingFunctions.Ease _ease = LockEase;

        //Move
        MoveTowards(_camera_pos, _lock_pos, _move_speed, _ease, _easeDist);
        MoveTowardsDown(_camera_pos, _lock_pos, _move_speed, _ease, _easeDist);
    }

    /// <summary>
    /// State Functions
    /// </summary>

    void StateFollowPlayer()
    {
        TrailPlayer();
    }
    void StateDownPhase()
    {
        DownPhase();
    }
    void StateLock()
    {
        Lock();
    }

    void HandleCameraState()
    {
        switch (currentCameraState)
        {
            case CameraState.FollowPlayer:      StateFollowPlayer();            break;
            case CameraState.DownPhase:         StateDownPhase();   break;
            case CameraState.Locked:            StateLock();                    break;    
        }
    }









    void Setup()
    {
        PlayerObject = Component.FindFirstObjectByType<PlayerComponent>().gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

	void ApplyShake(Vector3 shake)
	{
		Vector3 _pos = transform.position;
		_pos += shake;
		transform.position = _pos;
	}

	// Update is called once per frame
	void Update()
    {
        HandleCameraState();
		ApplyShake(GetComponent<CameraShake>().GetShakeVector());
	}
}
