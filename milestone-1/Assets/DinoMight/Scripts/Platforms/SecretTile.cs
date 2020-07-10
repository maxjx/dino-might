using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretTile : MonoBehaviour
{
    public GameObject tiles;

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D other)
    {  
        tiles.SetActive(true);
    }
}
