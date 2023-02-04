using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeControl : MonoBehaviour
{

    public GameObject fadeEffect;

    public void StartFade()
    {
        Debug.Log("REEE");
        fadeEffect.SetActive(true);
    }
}
