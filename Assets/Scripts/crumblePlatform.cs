using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crumblePlatform : MonoBehaviour
{
    [SerializeField]
    GameObject platform;

    private bool startDisable = false;

    private float touchTime = 3f;
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
        platform.SetActive(false);
        StartCoroutine("WaitAndEnable", respawnTime);
    }

    IEnumerator WaitAndEnable(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        platform.SetActive(true);
        startDisable = false;
    }
}
