using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crumblePlatform : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer platformSR;
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
        float timeElapsed = 0;
        while (timeElapsed < waitTime)
        {
            platformSR.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, timeElapsed / waitTime));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        platformSR.color = new Color(1, 1, 1, 0);
        platformCollider.enabled = false;
        StartCoroutine("WaitAndEnable", respawnTime);
    }

    IEnumerator WaitAndEnable(float waitTime)
    {
        float timeElapsed = 0;
        yield return new WaitForSeconds(waitTime / 2);
        while (timeElapsed < waitTime/2)
        {
            platformSR.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, timeElapsed / waitTime));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        platformSR.color = new Color(1, 1, 1, 1);
        platformCollider.enabled = true;
        startDisable = false;
    }

}
