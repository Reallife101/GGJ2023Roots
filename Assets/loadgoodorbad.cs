using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadgoodorbad : MonoBehaviour
{
    public float deathCount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (UIManager.UImanager.deathCount > deathCount)
        {
            SceneManager.LoadScene("badEnd");
        }
        else
        {
            SceneManager.LoadScene("goodEnd");
        }
    }
}
