using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlipper : MonoBehaviour
{
    [SerializeField] bool FlipDefault = false;

    Vector3 BaseScale = Vector3.one;
    [HideInInspector] public bool flip = false;

    // Start is called before the first frame update
    void Start()
    {
        BaseScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

        //Determine if sprite should flip based on movement
        {
            MovementController movement = GetComponent<MovementController>();
            if (movement == null)
                return;

            if (movement.moving_left)
                flip = !FlipDefault;
            if (movement.moving_right)
                flip = FlipDefault;
        }


        //Flip the sprite
        {
            Vector3 newScale = BaseScale;
            if (flip)
                newScale.x = BaseScale.x * -1;
            transform.localScale = newScale;
        }
    }
}
