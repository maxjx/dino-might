using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    void DeactivateThis()
    {
        gameObject.SetActive(false);
    }
}
