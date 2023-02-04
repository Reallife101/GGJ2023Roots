using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer platformSR;
    [SerializeField]
    Collider2D platformCollider;
    [SerializeField]
    float openTime;

    public void openDoor()
    {
        StartCoroutine("WaitAndEnable", openTime);
    }

    public void disableDoor()
    {
        StartCoroutine("WaitAndDisable", openTime);
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
    }

    IEnumerator WaitAndEnable(float waitTime)
    {
        float timeElapsed = 0;
        yield return new WaitForSeconds(waitTime / 2);
        while (timeElapsed < waitTime / 2)
        {
            platformSR.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, timeElapsed / waitTime));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        platformSR.color = new Color(1, 1, 1, 1);
        platformCollider.enabled = true;
    }
}
