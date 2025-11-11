using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoundManager : MonoBehaviour
{
	[SerializeField] bool BoundsVisible = false;
	List<BoxCollider2D> bounds = new List<BoxCollider2D>();
	public List<BoxCollider2D> GetBounds() { return bounds; }
	public int NumOfBoundObjects() {  return bounds.Count; }
	public BoxCollider2D Bound(int i ) { return bounds[i]; }


	void AddBound(GameObject arg_object)
	{
		arg_object.AddComponent<LevelBound>();
	}
	void AddBoxCollider(GameObject arg_object)
	{
		arg_object.AddComponent<BoxCollider2D>();
	}
	void SetTransparency(GameObject arg_object, float arg_opacity)
	{
		arg_object.GetComponent<SpriteRenderer>().color = new Vector4(0.2f, 0.2f, 0.2f, arg_opacity);
	}
	void HandleVisibility(GameObject arg_object, bool arg_visibility)
	{
		arg_object.GetComponent<SpriteRenderer>().enabled = arg_visibility;
	}

	// Start is called before the first frame update
	void Start()
    {
		for(int i = 0;i < transform.childCount;i++)
		{
			//For This Child
			GameObject _child = transform.GetChild(i).gameObject;

			//Add Bounds Component
			AddBound(_child);
			//Set colour to transparent
			SetTransparency(_child, 0.3f);
			//Add BoxCollider
			AddBoxCollider(_child);


			bounds.Add(_child.GetComponent<BoxCollider2D>());

		}
    }

    

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < transform.childCount; i++)
		{
			HandleVisibility(transform.GetChild(i).gameObject, BoundsVisible);
		}
    }
}
