using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Key : MonoBehaviour
{
    [SerializeField] private KeyType keyType;

    public enum KeyType {
        Yellow
    }

    public KeyType getKeyType() {
        return keyType;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player") {
            return;
        }

        KeyHolder keyHolder = other.gameObject.GetComponent<KeyHolder>();

        if (keyHolder == null)
        {
            return;
        }

        keyHolder.AddKey(keyType);
        Destroy(gameObject);
    }
}
