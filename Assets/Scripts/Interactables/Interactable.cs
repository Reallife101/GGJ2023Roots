using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private bool triggerActive = false;
    public abstract void interact();

    private GameObject currentInside;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.GetComponent<PlayerController>().getIsDisabled())
        {
            triggerActive = true;
            currentInside = other.gameObject;
            
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.GetComponent<PlayerController>().getIsDisabled())
        {
            triggerActive = false;
            
        }
    }

    private void Update()
    {
        if (currentInside != null && currentInside.GetComponent<PlayerController>().getIsDisabled())
        {
            return;
        }
        // Replace keycode later
        if (triggerActive)
        {
            //Add somesort of display later


            if (Input.GetKeyDown(KeyCode.E))
            {
                interact();
            }
        }

    }

}
