using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat_Attack : MonoBehaviour
{

    [SerializeField] Vector2 HitboxBaseSize = Vector2.one;
    Vector2 HitBoxActualSize = Vector2.one;

    BoxCollider2D damage_collider;

    bool inAttack = false;
    [SerializeField] float Duration = 1;
    float Timer = 0;

    //Testing
    [SerializeField] bool testing = true;
    SpriteRenderer hitbox_sprite;

    public void BeginAttack()
    {

        if (!inAttack)
        {

            if(testing)
			    Debug.Log("Attack Start");



			inAttack = true;
            
            damage_collider.size = HitBoxActualSize;

            //Testing

        }
    }


    void UpdateAttack()
    {
        if (inAttack)
        {


            if(Timer >= Duration)
            {
                EndAttack();
            }
            Timer += Time.deltaTime;
        }
    }

    void EndAttack()
    {

        if (inAttack)
        {
            if (testing)
                Debug.Log("Attack End");

            Timer = 0;
            inAttack = false;
        }

    }

    // Start is called before the first frame update
    void Start()
    {

		HitBoxActualSize = HitboxBaseSize;

		damage_collider = gameObject.AddComponent<BoxCollider2D>();

        //Testing
        if (testing)
        {
            hitbox_sprite = gameObject.AddComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            BeginAttack();
        }

		UpdateAttack();


        //Testing
        if (testing)
        {
            //Match rectangle sprite to box collider
            //hitbox_sprite.size = HitBoxActualSize;
            GetComponent<BoxCollider2D>().size = HitBoxActualSize;

        }

    }
}
