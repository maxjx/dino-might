using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineAreaToggler : MonoBehaviour
{
    public GameObject switchToCameraInCollider;

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            switchToCameraInCollider.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        switchToCameraInCollider.SetActive(false);
    }
}
