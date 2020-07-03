using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTerminal : Interact
{
    [SerializeField] private GameObject platform;
    [SerializeField] private GameObject displayON;
    [SerializeField] private GameObject displayOFF;

    protected override void TriggerAction()
    {
        platform.GetComponent<MovingPlatformWithLock>().toggleLock();

        SpriteRenderer sp = gameObject.GetComponent<SpriteRenderer>();
        sp.flipX = !sp.flipX;

        if (displayON.activeSelf)
        {
            displayON.SetActive(false);
            displayOFF.SetActive(true);
        }
        else
        {
            displayON.SetActive(true);
            displayOFF.SetActive(false);
        }
    }
}
