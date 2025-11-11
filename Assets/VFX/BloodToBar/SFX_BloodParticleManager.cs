using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_BloodParticleManager : MonoBehaviour
{
    public GameObject spawner;
    [SerializeField] Vector3 Offset = new Vector3(0, 0, 5);

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public GameObject Spawn(Vector2 _arg_spawn_pos)
    {
        Vector3 _pos = _arg_spawn_pos;
        Vector3 _offset = Offset;
        GameObject _new = Instantiate(spawner, _pos + _offset, Quaternion.identity);
        _new.GetComponent<SFX_BloodParticle>().Moving = true;
        _new.transform.parent = transform;

        return _new;
	}

    // Update is called once per frame
    void Update()
    {

    }
}
