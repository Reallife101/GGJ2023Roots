using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager UImanager;
    public int deathCount;

    private void Awake()
    {
        if (UImanager == null)
        {
            UImanager = this;
            DontDestroyOnLoad(this);
        }
    }
}
