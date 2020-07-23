using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCollider : MonoBehaviour
{
    public int questNumber;     // quest number less than this value will not disable the collider

    // Start is called before the first frame update
    void Start()
    {
        if (questNumber < Global.questNumber)
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
