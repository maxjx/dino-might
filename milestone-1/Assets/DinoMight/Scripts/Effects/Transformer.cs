using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformer : MonoBehaviour
{
    // change y position of this to obj position
    public void ChangeYPosition(Transform obj)
    {
        transform.position = new Vector2(obj.position.x, transform.position.y);
    }
}
