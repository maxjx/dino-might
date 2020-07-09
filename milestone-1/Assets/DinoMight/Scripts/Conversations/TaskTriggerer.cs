using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For objects that complete a task based on interaction with player, eg stepping into tree in tut lvl
public class TaskTriggerer : MonoBehaviour
{
    private bool triggered = false;         // prevents double counting

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!triggered)
        {
            Global.questNumber++;
            triggered = true;
        }
    }
}
