using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlatePlatform : MonoBehaviour
{
    [SerializeField]
    private bool switchOn = false;
    private int playersOnSwitch;
    [SerializeField]
    private SwitchPlatform[] platforms;
    void Start()
    {
        playersOnSwitch = 0;
    }

    private void flipSwitch()
    {
        foreach (SwitchPlatform platform in platforms)
        {
            platform.TogglePlatform();
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
            if (switchOn && playersOnSwitch < 1)
            {
                switchOn = false;
                flipSwitch();
            }

        }
    }
}
