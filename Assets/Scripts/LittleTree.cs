using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleTree : MonoBehaviour
{
    private float time = 50f;
    private SpriteRenderer sprite;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float rangeDistance;
    private GameObject parent;

    [SerializeField]
    AudioClip dieTree;

    [SerializeField]
    AudioSource au;

    private Animator ai;
    // Start is called before the first frame update
    void Start()
    {
        //for when we need to use this later on
        //sprite = GetComponent<SpriteRenderer>();
        virtualCamera = GameObject.FindGameObjectWithTag("Cinemachine").GetComponent<CinemachineVirtualCamera>();
        ai = GetComponent<Animator>();

    }

    public bool WithinDistance(Transform littleTreeTransform) {
        return Mathf.Abs(parent.transform.position.magnitude - littleTreeTransform.position.magnitude) <
                rangeDistance;   
    }

    public void setParent(GameObject go)
    {
        parent = go;
    }


    public void Despawn(GameObject littleTree) {
        //Debug.Log("Destroying Little Tree");
        if (virtualCamera != null)
        {
            virtualCamera.Follow = parent.transform;
        }

        if (WithinDistance(transform))
        {
            parent.GetComponent<TreeController>().incrementCount();
            Destroy(littleTree);
        }
        else
        {
            GameManager.gameManager.UpdateTreesUsed();
            GameManager.gameManager.UpdateTreesLeft();
            au.PlayOneShot(dieTree, 1f);
            GetComponent<PlayerController>().Disable();
            ai.SetBool("dead", true);
        }
    }
}
