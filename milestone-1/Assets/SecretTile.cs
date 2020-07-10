using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretTile : MonoBehaviour
{
    [SerializeField] private GameObject tiles;

    private void OnColisionEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            tiles.SetActive(true);
        }
    }
}
