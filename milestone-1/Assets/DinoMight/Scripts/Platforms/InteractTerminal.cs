using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTerminal : Interact
{
    [SerializeField] private GameObject platform;

    protected override void TriggerAction()
    {
        platform.GetComponent<MovingPlatformWithLock>().toggleLock();
    }
}
