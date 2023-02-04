using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleTree : MonoBehaviour
{
    private float time = 50f;
    private SpriteRenderer sprite; 
    // Start is called before the first frame update
    void Start()
    {
        //for when we need to use this later on
        //sprite = GetComponent<SpriteRenderer>();

    }

    private void Awake()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        if (time >= 0) {
            gameObject.transform.position += Vector3.right; 
        }


    }



    public void Despawn() { 
            Debug.Log("Destroying Little Tree");
            Destroy(gameObject);
    }
}
