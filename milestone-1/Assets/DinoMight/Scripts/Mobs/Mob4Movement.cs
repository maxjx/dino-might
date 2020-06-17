using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob4Movement : MonoBehaviour
{
    public float speed = 1f;
    public Transform topEnd;
    public Transform bottomEnd;

    private bool movingDown = false;
    private float step;             // speed * fixed delta time

    // Start is called before the first frame update
    void Start()
    {
        step = speed * Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 currPos = transform.position;
        Vector2 topPos = topEnd.position;
        Vector2 bottomPos = bottomEnd.position;
        if (currPos.y >= topPos.y)
        {
            movingDown = true;
        }
        else if (currPos.y <= bottomPos.y)
        {
            movingDown = false;
        }
        // else mob is in between top and bottom ends and direction of travel should not change

        if (movingDown)
        {
            transform.position = Vector2.MoveTowards(transform.position, bottomPos, step);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, topPos, step);
        }
    }
}
