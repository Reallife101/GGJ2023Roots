using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactDoor : Interactable
{
    [SerializeField] 
    private bool switchOn = false;

    [SerializeField]
    private GameObject[] offDefault;
    [SerializeField]
    private GameObject[] onDefault;

    [SerializeField]
    AudioClip ac;
    [SerializeField]
    AudioSource au;

    void Start()
    {
        flipSwitch();
    }

    public override void interact()
    {
        switchOn = !switchOn;
        flipSwitch();
    }

    private void flipSwitch()
    {
        au.PlayOneShot(ac, .95f);
        foreach (GameObject go in offDefault)
        {
            doorScript sd = go.GetComponent<doorScript>();
            
            if (sd)
            {
                if (switchOn)
                {
                    sd.openDoor();
                }
                else
                {
                    sd.disableDoor();
                }

            }
            else
            {
                go.SetActive(switchOn);
            }

        }

        foreach (GameObject go in onDefault)
        {
            doorScript sd = go.GetComponent<doorScript>();

            if (sd)
            {
                if (!switchOn)
                {
                    sd.openDoor();
                }
                else
                {
                    sd.disableDoor();
                }

            }
            else
            {
                go.SetActive(!switchOn);
            }
        }
    }
}
