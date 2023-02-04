using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    [SerializeField] private GameObject littleTree;
    [SerializeField] private GameObject spawnPoint;

    private bool spawned;

    void Start()
    {
        //I'm guessing we are going to need this when we switch between Big Tree Statue and normal Tree 
        //SpriteRenderer treeSprtite = GetComponent<SpriteRenderer>();
        spawned = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) {

            SpawnLittleTree();
        }
    }


    private void SpawnLittleTree() {
        if (!spawned)
        {
            ChangeSprite();
            Instantiate(littleTree, spawnPoint.transform.position, Quaternion.identity);
            spawned = true;
        }
        else 
        { 
            DespawnLittleTree();
            spawned = false;
        }
    }

    private void DespawnLittleTree() {
        littleTree.GetComponent<LittleTree>().Despawn();
    }

    private void ChangeSprite()
    {

        // This is for changing the sprite of the tree

    }





}
