using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType : MonoBehaviour
{

    [SerializeField] float timeInStage = 1;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        EnemyInput enemyInput = GetComponent<EnemyInput>();
        if(enemyInput != null)
        {


            

            //Update Time
            timeInStage += Time.deltaTime;


        }

    }
}
