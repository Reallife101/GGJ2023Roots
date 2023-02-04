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

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        if (time >= 0) {
            transform.position += Vector3.right * Time.deltaTime; 
        }


    }


    public void Despawn(GameObject litteTree) { 
            Debug.Log("Destroying Little Tree");
            Destroy(litteTree);
    }
}
