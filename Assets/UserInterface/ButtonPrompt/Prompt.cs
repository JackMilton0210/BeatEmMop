using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prompt : MonoBehaviour
{

    public enum PromptState { Open, Closed, Opening, Closing };
    [SerializeField] PromptState currentState;

    public void SetNewState(PromptState new_state) { currentState = new_state; }
    public void SetStateToOpen() { currentState = PromptState.Open; }
    public void SetStateToClosed() { currentState = PromptState.Closed; }
    public void SetStateToOpening() { currentState = PromptState.Opening; }
    public void SetStateToClosing() { currentState = PromptState.Closing; }

    public bool IsOpen() { return (currentState == PromptState.Open);}
    public bool IsClosed() { return (currentState == PromptState.Closed); }
    public bool IsOpening() { return (currentState == PromptState.Opening); }
    public bool IsClosing() { return (currentState == PromptState.Closing); }

    float UpPoint;
    float DownPoint;
    float DownPositionOffset = 200;

    float MoveUpTime = 1;
    float MoveDownTime = 1;
    float CurrentMoveTime = 0;

    EasingFunctions.Ease MoveUpEase = EasingFunctions.Ease.Linear;
    EasingFunctions.Ease MoveDownEase = EasingFunctions.Ease.InQuad;

    void MoveUp()
    {
        //Get Eased Time Value
        float _time = EasingFunctions.ApplyEase(MoveUpEase, 0, MoveUpTime, CurrentMoveTime);
        //Get New Y Position
        float _newPos = Mathf.Lerp(DownPoint, UpPoint, _time);
        //Apply New Position
        Vector3 pos = transform.position;
        pos.y = _newPos;
        transform.position = pos;
    }
    bool FinishedMovingUp()
    {
        if(transform.position.y >= UpPoint)
        {
            
            return true;
        }
        return false;
    }
    void MoveDown()
    {
        float _time = EasingFunctions.ApplyEase(MoveDownEase, 0, MoveDownTime, CurrentMoveTime);

        float _newPos = Mathf.Lerp(UpPoint, DownPoint, _time);

        Vector3 pos = transform.position;
        pos.y = _newPos;
        transform.position = pos;
    }
    bool FinishedMovingDown()
    {
		if (transform.position.y <= DownPoint)
		{
			return true;
		}
		return false;
	}

	void UpdateOpen() {
        CurrentMoveTime = 0;
		transform.position = new Vector3(transform.position.x, UpPoint, transform.position.z);
	}
	
    void UpdateClosed() { 
        CurrentMoveTime = 0;
		transform.position = new Vector3(transform.position.x, DownPoint, transform.position.z);
	}
    void UpdateOpening() {
        CurrentMoveTime += Time.deltaTime;
        MoveUp();
        if (FinishedMovingUp()) SetStateToOpen();
    }
    void UpdateClosing() {
        CurrentMoveTime += Time.deltaTime;
        MoveDown();
        if (FinishedMovingDown()) SetStateToClosed();
    }

	void UpdateSwitch()
    {
        switch (currentState)
        {
            case PromptState.Open:    UpdateOpen();    break;
            case PromptState.Closed:  UpdateClosed();  break;
            case PromptState.Opening: UpdateOpening(); break;
            case PromptState.Closing: UpdateClosing(); break;
                
        }
    }

    public void SetTransformPoints()
    {
		UpPoint = transform.position.y;
		DownPoint = UpPoint - DownPositionOffset;
	}

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSwitch();
    }
}
