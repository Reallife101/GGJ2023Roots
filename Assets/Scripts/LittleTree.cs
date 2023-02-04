using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LittleTree : MonoBehaviour
{
    private float time = 5f;
    private SpriteRenderer sprite;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        //for when we need to use this later on
        //sprite = GetComponent<SpriteRenderer>();
        virtualCamera = GameObject.FindGameObjectWithTag("Cinemachine").GetComponent<CinemachineVirtualCamera>();
        Debug.Log("starrt tree");
        Debug.Log(virtualCamera.name);

    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        if (time >= 0) {
            transform.position += Vector3.right * Time.deltaTime; 
        }


    }

    public void setParent(GameObject go)
    {
        parent = go;
    }


    public void Despawn() {

        if (virtualCamera != null)
        {
            Debug.Log("parent cinemachine");
        
            virtualCamera.Follow = parent.transform;
        }

        Destroy(gameObject);
    }

}
