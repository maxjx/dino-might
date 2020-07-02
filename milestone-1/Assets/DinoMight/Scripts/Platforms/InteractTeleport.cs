using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTeleport : Interact
{
    [SerializeField] private GameObject otherPortal;
    protected override void TriggerAction()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = otherPortal.transform.position;
    }
}
