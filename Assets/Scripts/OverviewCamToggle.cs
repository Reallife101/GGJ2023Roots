using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class OverviewCamToggle : MonoBehaviour
{
    private bool active = false;
    public CinemachineVirtualCamera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            active = !active;
            cam.enabled = active;
        }
    }
}
