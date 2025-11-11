using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptStager : MonoBehaviour
{
    //Prompts
    [SerializeField] Prompt ThrowablePrompt;
    [SerializeField] Prompt RagePrompt;
    [SerializeField] Prompt CleanPrompt;

    [SerializeField] EnvironmentThrowable throwableObject;
    float ThrowableTriggerDistance = 2;

    bool cleanActivated = false;
    bool rageActivated = false;
    bool throwableActivated = false;

    bool cleanFinished = false;
    bool rageFinished = false;
    bool throwableFinished = false;

    //Begin Opening and Closing of each Prompt
    void OpenThrowable() { ThrowablePrompt.SetStateToOpening(); }
    void CloseThrowable() { ThrowablePrompt.SetStateToClosing(); }
    void OpenRage() { RagePrompt.SetStateToOpening(); }
    void CloseRage() { RagePrompt.SetStateToClosing(); }
    void OpenClean() { CleanPrompt.SetStateToOpening(); }
    void CloseClean() { CleanPrompt.SetStateToClosing(); }

    //Starting Data and Positions for each of the prompts
    void SetAllTransformPoints()
    {
        ThrowablePrompt.SetTransformPoints();
        RagePrompt.SetTransformPoints();
        CleanPrompt.SetTransformPoints();
    }
    void CloseAll()
    {
        ThrowablePrompt.SetStateToClosed();
        RagePrompt.SetStateToClosed();
        CleanPrompt.SetStateToClosed();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetAllTransformPoints();
        CloseAll();
    }


    bool FirstEnemyKilled()
    {
        if (Component.FindFirstObjectByType<EnemyHandler>().GetEnemyDied())
            return true;
        return false;
        
    }
    bool SecondEncounterStarted()
    {
        //return (Input.GetKeyDown(KeyCode.Period));
        if (Component.FindFirstObjectByType<EnemyHandler>().GetEnemyExists(1))
        {
            GameObject _enemy = Component.FindFirstObjectByType<EnemyHandler>().GetEnemy(1);
            if (_enemy)
                if (_enemy.GetComponent<EnemyHealth>().IsAlive())
                    return true;
        }
        return false;

    }
    bool NearThrowable()
    {
        //return (Input.GetKeyDown(KeyCode.Slash));
        Vector3 _diff = throwableObject.transform.position - Component.FindFirstObjectByType<PlayerComponent>().transform.position;
        float distancePlayerToThrowable = _diff.magnitude;
        if (distancePlayerToThrowable < ThrowableTriggerDistance)
            return true;
        else
            return false;
	}

    bool CleanButtonPressed()
    {
        if (Component.FindFirstObjectByType<PlayerInputHandler>().IN.mop) return true;
        return false;
    }

    bool RageButtonPressed()
    {
        if (Input.GetKey(KeyCode.F)) return true;
        if (Input.GetKey("joystick 1 button 2")) return true;
        return false;
    }
    bool ThrowButtonPressed()
    {
        if (Component.FindFirstObjectByType<PlayerInputHandler>().IN.attack) return true;
        return false;
    }

            

    void HandleClean()
    {
        //If the clean has not been activated
        if (!cleanActivated)
        {
            if (FirstEnemyKilled())
            {
                cleanActivated = true;
                OpenClean();
            }
        }
        //If the clean has been activated then check for button pressed
        else
        {
            if (!cleanFinished)
            {
                if (CleanButtonPressed())
                {
                    cleanFinished = true;
                    CloseClean();
                }
            }
        }
    }

    void HandleRage()
    {
        if (!rageActivated)
        {
            if (SecondEncounterStarted())
            {
                rageActivated = true;
                OpenRage();
            }
        }
        else
        {
            if (!rageFinished)
            {
                if (RageButtonPressed())
                {
                    rageFinished = true;
                    CloseRage();
                }
            }
        }
    }

    void HandleThrow()
    {
        if (!throwableActivated)
        {
            if (NearThrowable())
            {
                throwableActivated = true;
                OpenThrowable();
            }
        }
        else
        {
            if (!throwableFinished)
            {
                if (ThrowButtonPressed())
                {
                    throwableFinished = true;
                    CloseThrowable();
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {

        HandleClean();
        HandleRage();
        HandleThrow();

    }
}
