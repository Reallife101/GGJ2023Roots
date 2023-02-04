using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetChildren : MonoBehaviour
{
    [SerializeField] int resetAmount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetComponent<TreeController>())
        {
            collision.GetComponent<TreeController>().numChildrenLeft = resetAmount;
        }
    }
}
