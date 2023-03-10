using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressurePlate : MonoBehaviour
{
    [SerializeField]
    private bool switchOn = false;
    private int playersOnSwitch;

    [SerializeField]
    private GameObject[] offDefault;
    [SerializeField]
    private GameObject[] onDefault;

    [SerializeField]
    AudioClip plate;

    [SerializeField]
    AudioSource au;

    void Start()
    {
        flipSwitch();
        playersOnSwitch = 0;
    }

    public void interact()
    {
        switchOn = !switchOn;
        flipSwitch();
    }

    private void flipSwitch()
    {
        au.PlayOneShot(plate, .4f);
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playersOnSwitch++;
            if (!switchOn)
            {
                switchOn = true;
                flipSwitch();
            }

        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playersOnSwitch--;
            if (switchOn && playersOnSwitch<1)
            {
                switchOn = false;
                flipSwitch();
            }

        }
    }

}
