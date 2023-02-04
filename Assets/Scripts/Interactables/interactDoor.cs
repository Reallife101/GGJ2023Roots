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
        foreach (GameObject go in offDefault)
        {
            go.SetActive(switchOn);
        }

        foreach (GameObject go in onDefault)
        {
            go.SetActive(!switchOn);
        }
    }
}
