using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTeleport : Interact
{
    public ReEnable mainVCam;       // Component to reenable player's virtual camera
    [SerializeField] private GameObject otherPortal;

    protected override void TriggerAction()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = otherPortal.transform.position;
        if (mainVCam != null)
            mainVCam.ReEnableWithDelay(0.02f);
    }
}
