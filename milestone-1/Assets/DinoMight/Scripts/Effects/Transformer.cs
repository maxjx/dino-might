using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformer : MonoBehaviour
{
    // change position of this to obj position
    public void ChangePosition(Transform obj)
    {
        transform.position = obj.position;
    }
}
