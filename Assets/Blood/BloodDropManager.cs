using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDropManager : MonoBehaviour
{

    [SerializeField] GameObject PlayerObject;
    [SerializeField] Sprite BloodDropSprite;

    [SerializeField] float TotalMoveTime = 1;

    struct Drop
    {
        Vector2 position;
        float currentTime;

    }

    void SpawnDrop()
    {
        GameObject newDrop = new GameObject();
        //newDrop.AddComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SpawnDrop();
        }
    }
}
