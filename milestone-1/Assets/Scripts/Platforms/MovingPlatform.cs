using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Make the platform move between 2 selected positions
public class MovingPlatform : MonoBehaviour {
    public Transform pos1, pos2;
    public float moveAmount;
    private Transform nextPosition;
    private Transform currPosition;

    void Start()
    {
        nextPosition = pos2;
        currPosition = pos1;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x == pos1.position.x &&
            transform.position.y == pos1.position.y) {
            nextPosition = pos2;
            currPosition = pos1;
        } else if (transform.position.x == pos2.position.x &&
            transform.position.y == pos2.position.y) {
            nextPosition = pos1;
            currPosition = pos2;
        }
        transform.position = Vector2.MoveTowards(transform.position,
            nextPosition.position, moveAmount * Time.deltaTime);
    }

    void OnDrawGizmos() {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
