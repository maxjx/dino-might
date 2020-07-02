using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class LockedDoor : MonoBehaviour
{
    private bool isLocked = true;
    [SerializeField] GameObject otherDoor;

    public void Toggle()
    {
        isLocked = !isLocked;
        LockedDoor door = otherDoor.GetComponent<LockedDoor>();
        if (!isLocked && door.isCurrentlyLocked())
        {
            door.Toggle();
        }
    }

    private void OnTriggerStay2D(Collider2D player) {
        if (isLocked)
        {
            return;
        }

        if (Input.GetKeyDown("c"))
        player.transform.position = otherDoor.transform.position;
    }

    public bool isCurrentlyLocked() {
        return isLocked;
    }

    public void ManualTrigger()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = otherDoor.transform.position;
    }
}
