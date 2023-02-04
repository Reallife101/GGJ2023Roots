using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactPlatform : Interactable
{
    [SerializeField]
    private SwitchPlatform[] platforms;

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
        }
    }
}
