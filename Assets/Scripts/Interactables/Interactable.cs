using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private bool triggerActive = false;
    public abstract void interact();

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.GetComponent<PlayerController>().getIsDisabled())
        {
            triggerActive = true;
            
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
