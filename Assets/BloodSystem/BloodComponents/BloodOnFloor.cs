using NUnit.Framework.Internal.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodOnFloor : MonoBehaviour
{

    [SerializeField] Sprite child_sprite_0;

    void ApplySprite()
    {
		int num_of_children = transform.childCount;

		for (int i = 0; i < num_of_children; i++)
		{
            //Generate Random Number for random sprite

            //Apply Sprite
            transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = child_sprite_0;
            transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            transform.GetChild(i).transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		}
	}

    // Start is called before the first frame update
    void Start()
    {
        ApplySprite();
	}

    void OperateOnObject(GameObject arg_object)
    {
        //Child Object to operate on
        GameObject child = arg_object;

        //Get Percentage of Uncollected Blood
        float percentage = Component.FindFirstObjectByType<BloodSystem>().UncollectedBloodPercentage();

        //apply new alpha
        float new_alpha = percentage;

        //
        Color current_color = child.GetComponent<SpriteRenderer>().color;
        current_color.a = new_alpha;
        child.GetComponent<SpriteRenderer>().color = current_color;

    }

    //List of all children object
    void OperateOnChildren()
    {
        int num_of_children = transform.childCount;

        for(int i = 0; i < num_of_children; i++)
        {
            OperateOnObject(transform.GetChild(i).gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        OperateOnChildren();
    }
}
