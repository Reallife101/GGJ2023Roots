using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlatformScaler : MonoBehaviour
{
    [SerializeField] private Vector3 scale;
    [SerializeField] private BoxCollider2D platform;
    [SerializeField] private GameObject platformMesh;

    // Update is called once per frame
    void Update()
    {
        platform.size = scale;
        platformMesh.transform.localScale = scale;
    }
}