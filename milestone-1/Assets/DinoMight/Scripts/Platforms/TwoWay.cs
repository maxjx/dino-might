﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoWay : MonoBehaviour
{
    private PlatformEffector2D effector;
    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.S)) {
            effector.rotationalOffset = 180f;
            StartCoroutine(Reset());
        }

        // Bug resolved in the charactercontroller2D script
        if(Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) {
            effector.rotationalOffset = 0f;
        }
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(0.3f);
        effector.rotationalOffset = 0f;
    }
}
