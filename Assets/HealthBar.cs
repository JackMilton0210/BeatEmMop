using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
//using UnityEditor.Build.Content;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    public Sprite FullHeart;
    public Sprite HalfHeart;
    public float SpriteScale = 0.25f;
    public Vector2 BasePos = new Vector2(0, 0);
    public float Spacing = 1;
    float HeartBuzzLimit = 21;

    [SerializeField] GameObject PlayerObject;

    public float temp_health = 5;

    private GameObject[] bits;

    void CreateBits(int numOfBits)
    {
        bits = new GameObject[numOfBits];
        for(int i = 0; i < numOfBits; i++)
        {
            bits[i] = new GameObject();
            bits[i].name = "HealthNode";
            bits[i].transform.SetParent(transform, false);
            bits[i].AddComponent<SpriteRenderer>();
            bits[i].AddComponent<RectTransform>();

            bits[i].GetComponent<SpriteRenderer>().sprite = FullHeart;

            RectTransform newTransform = bits[i].GetComponent<RectTransform>();
            Vector3 object_offset = new Vector3(BasePos.x + (i * Spacing), BasePos.y, 0);
            newTransform.position = object_offset;
            newTransform.localScale = new Vector3(SpriteScale,SpriteScale,SpriteScale);
            
            bits[i].transform.position = newTransform.position;
            bits[i].transform.localScale = newTransform.localScale;

        }
    }

    void ConvertHealthToBits(float currentHealth)
    {
        float chunks = currentHealth / 2;
		
        for(int i = 0; i < bits.Length; i++)
        {
            bits[i].GetComponent<SpriteRenderer>().sprite = FullHeart;
        }

		for (int i = 0;i < bits.Length; i++)
        {
            //If health is over this bit x2 then 
            if(i < chunks - 1)
            {
                bits[i].GetComponent<SpriteRenderer>().enabled = true;
                
                if(chunks%1 == 0)
                {
                    bits[(int)chunks].GetComponent<SpriteRenderer>().sprite = FullHeart;
                }
                else
                {
                    bits[(int)chunks].GetComponent<SpriteRenderer>().sprite = HalfHeart;
                }
            }
            else
            {
                bits[i].GetComponent <SpriteRenderer>().enabled = false;
            }
        }
    }

    void MoveHeartsToPlayer()
	{
        Vector2 playerPos = PlayerObject.transform.position;

        for(int i = 0; i<bits.Length; i++)
        {
            Vector2 noise = Vector2.zero;
            if (PlayerObject.GetComponent<Health>().currentHealth < HeartBuzzLimit) {
                noise = new Vector2(Random.value, Random.value);
                noise /= 10;
            }

            Vector2 newPos = new Vector2(
                playerPos.x + BasePos.x + noise.x + (Spacing * i), 
                playerPos.y + BasePos.y + noise.y);
            bits[i].transform.position = newPos;
        }
	}

	// Start is called before the first frame update
	void Start()
    {
        CreateBits(6);
        
    }

    // Update is called once per frame
    void Update()
    {

		MoveHeartsToPlayer();
		ConvertHealthToBits(PlayerObject.GetComponent<Health>().currentHealth);
        Debug.Log("asda   s    s   sdasd");
    }
}
