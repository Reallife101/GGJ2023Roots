using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactPlatform : Interactable
{
    [SerializeField]
    private SwitchPlatform[] platforms;
    [SerializeField] private GameObject on;
    [SerializeField] private GameObject off;
    private bool flipped = false;

    void Start()
    {
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
            foreach (SwitchPlatform platform in platforms)
            {
                platform.TogglePlatform();
            }
            flipped = !flipped;
            if (flipped)
            {
                on.SetActive(true);
                off.SetActive(false);
            }
            else
            {
                on.SetActive(false);
                off.SetActive(true);
            }
        }
    }
}
