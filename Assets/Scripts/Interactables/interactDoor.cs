using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactDoor : Interactable
{

    [SerializeField]
    private SwitchPlatform[] platforms;

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
        first_flip();
    }

    public override void interact()
    {
        flipSwitch();
    }

    private void flipSwitch()
    {

        bool canFlip = true;
        
        foreach (SwitchPlatform platform in platforms)
        {
            if (platform.moveInProgress)
            {
                canFlip = false;
            }
        }

        if (canFlip)
        {
            switchOn = !switchOn;
            foreach (SwitchPlatform platform in platforms)
            {
                platform.TogglePlatform();
            }

            au.PlayOneShot(ac, .8f);
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
    private void first_flip()
    {
        au.PlayOneShot(ac, .8f);
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
