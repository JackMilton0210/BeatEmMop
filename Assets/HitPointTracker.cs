using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPointTracker : MonoBehaviour
{
    [SerializeField] TagWrapper TrackerTag;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //COnvert to string
        string TrackerTagString = "";
        if (TrackerTag == TagWrapper.Player)
            TrackerTagString = "PlayerObject";
        if (TrackerTag == TagWrapper.Enemy)
            TrackerTagString = "EnemyObject";

        //Get Object
        GameObject trackedObject = GameObject.FindGameObjectWithTag(TrackerTagString);


        //Get object health
        Vector2 HitPosition = trackedObject.GetComponent<Attack>().CurrentHitPosition;
        Vector3 newTransform = new Vector3(HitPosition.x, HitPosition.y, 0);

        //Set this object transform to HitPosition
        transform.position = newTransform;
        
        
    }
}
