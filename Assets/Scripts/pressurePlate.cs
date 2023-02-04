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
        foreach (GameObject go in offDefault)
        {
            go.SetActive(switchOn);
        }

        foreach (GameObject go in onDefault)
        {
            go.SetActive(!switchOn);
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
