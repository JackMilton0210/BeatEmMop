using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class PunchEffectsManager : MonoBehaviour
{
    //[SerializeField] Image HitEffect;
    [SerializeField] GameObject VFX_Spark;
	[SerializeField] GameObject VFX_Circle; 
    [SerializeField] GameObject VFX_Sad;    
    [SerializeField] GameObject VFX_Angry;
    [SerializeField] GameObject VFX_Smile;
    [SerializeField] GameObject VFX_Blood;


    float EffectDepth = -5;
    Vector2 Offset = new Vector2(0, 0);
    List<GameObject> effects = new List<GameObject>();

    //Create a punch effect at the arg location
    public void SpawnEffect(Vector2 spawn_pos, Direction fx_direction, bool alive)
    {
        GameObject _new = null;

        if (!alive)
        {

            _new = Instantiate(VFX_Spark, transform);

        }
        else
        {
            _new = Instantiate(VFX_Sad, transform);
        }

        _new.GetComponent<VFX_Effect>().Spawn();
        Vector3 _pos = new Vector3(spawn_pos.x, spawn_pos.y, EffectDepth);

        Vector3 _offset = Offset;
        if (fx_direction == Direction.Left) _offset.x *= -1;
        _new.transform.position = _pos + _offset;

        if (!alive)
        _new.GetComponent<VFX_Effect>().SetRotation(fx_direction);

        effects.Add(_new);
    }

    void HandleFX()
    {
        for (int i = 0; i < effects.Count; i++)
        {
            if (effects[i].GetComponent<VFX_Effect>().isFinished())
            {
                GameObject _to_remove = effects[i];
                effects.Remove(_to_remove);
                _to_remove.SetActive(false);
                Destroy(_to_remove);

            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //Spawn effect on key press
        if (Input.GetMouseButtonDown(0))
        {
            if (Component.FindFirstObjectByType<DevOptions>().GetDevMode())
            {
                Direction player_dir = Component.FindFirstObjectByType<PlayerController>().facing;
                SpawnEffect(Camera.main.ScreenToWorldPoint(Input.mousePosition), player_dir, true);
            }
        }

        HandleFX();
    }
}
