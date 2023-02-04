using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crumblePlatform : MonoBehaviour
{
    [SerializeField]
    GameObject platformSR;
    [SerializeField]
    Collider2D platformCollider;

    private bool startDisable = false;

    private float touchTime = 1f;
    private float respawnTime = 3f;


    public void platformTouch()
    {
        if (!startDisable)
        {
            startDisable = true;
            StartCoroutine("WaitAndDisable", touchTime);
        }
    }

    IEnumerator WaitAndDisable(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        platformSR.SetActive(false);
        platformCollider.enabled = false;
        StartCoroutine("WaitAndEnable", respawnTime);
    }

    IEnumerator WaitAndEnable(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        platformSR.SetActive(true);
        platformCollider.enabled = true;
        startDisable = false;
    }
}
