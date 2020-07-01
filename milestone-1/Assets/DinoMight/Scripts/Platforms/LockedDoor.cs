using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class LockedDoor : MonoBehaviour
{
    private bool isLocked = true;
    [SerializeField] GameObject teleportTo;

    public void Toggle()
    {
        isLocked = !isLocked;
    }

    private void OnTriggerStay2D(Collider2D player) {
        if (isLocked)
        {
            return;
        }

        player.transform.position = teleportTo.transform.position;
    }

    public bool isCurrentlyLocked() {
        return isLocked;
    }
}
