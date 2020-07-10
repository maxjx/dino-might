using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOnCollision : Deactivate
{
    public LayerMask collisionLayers;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collisionLayers == (collisionLayers | (1 << collider.gameObject.layer)))
            gameObject.GetComponent<Animator>().SetTrigger("hit");
    }
}
