using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugInteractable : Interactable
{
    public override void interact()
    {
        Debug.Log(gameObject.name+" was interacted with.");
    }
}
