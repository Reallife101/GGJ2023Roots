using Cinemachine;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    [SerializeField] private GameObject littleTree;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private GameObject spawnedLittleTree;

    private bool spawned;

    void Start()
    {
        //I'm guessing we are going to need this when we switch between Big Tree Statue and normal Tree 
        //SpriteRenderer treeSprtite = GetComponent<SpriteRenderer>();
        spawned = false;
        virtualCamera = GameObject.FindGameObjectWithTag("Cinemachine").GetComponent<CinemachineVirtualCamera>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) {

            SpawnLittleTree();
            GetComponent<PlayerController>().ToggleInput();
            Debug.Log(virtualCamera.name);
        }
    }


    private void SpawnLittleTree() {
        if (!spawned)
        {
            ChangeSprite();
            spawnedLittleTree = Instantiate(littleTree, spawnPoint.position, Quaternion.identity);
            spawnedLittleTree.GetComponent<LittleTree>().setParent(gameObject);

            if (virtualCamera != null)
            {
                virtualCamera.Follow = spawnedLittleTree.transform;
            }
        }
        else 
        {
            DespawnLittleTree();
        }

        toggleFreezePosition();
        spawned = !spawned;
    }

    private void DespawnLittleTree() {
        spawnedLittleTree.GetComponent<LittleTree>().Despawn(spawnedLittleTree);
        spawnedLittleTree = null;
    }

    private void ChangeSprite()
    {

        // This is for changing the sprite of the tree

    }

    private void toggleFreezePosition()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb.constraints == RigidbodyConstraints2D.FreezeRotation)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }




}
