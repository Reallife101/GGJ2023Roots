using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crumblePlatformChild : MonoBehaviour
{
    [SerializeField]
    private crumblePlatform cp;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        cp.platformTouch();
    }
}
