using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class DevOptions : MonoBehaviour
{
    [SerializeField] bool DevMode;
    [SerializeField] bool KillEnemies;
    [SerializeField] bool ArrowBlockers;
    [SerializeField] bool FillBlood;
    [SerializeField] PlayerInputHandler.InputMode Dev_InputMode;

    public bool GetDevMode() { return DevMode; }
    public bool GetArrowBlockers() { if(DevMode) return ArrowBlockers;return false; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Input Mode
        Component.FindFirstObjectByType<PlayerInputHandler>().inputMode = Dev_InputMode;

        //Kill All Enemies
        if (DevMode)
        {
            if (Input.GetKeyDown(KeyCode.K) || KillEnemies)
            {
                Component.FindFirstObjectByType<EnemyHandler>().KillAllEnemies();
                KillEnemies = false;
            }


            if (FillBlood)
            {
                if(!Component.FindFirstObjectByType<PowerUp>().IsActive())
                    Component.FindFirstObjectByType<Collected_BloodResource>().Increase(10);
                FillBlood = false;
            }
        }

    }
}
