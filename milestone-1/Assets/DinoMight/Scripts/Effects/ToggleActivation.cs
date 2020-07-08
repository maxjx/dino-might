using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActivation : MonoBehaviour
{
    public bool activated = false;
    public float delayedDeactivateDuration = 1.5f;
    public bool delaying = false;
    
    private float timer = 0;

    public void Activate()
    {
        activated = true;
        gameObject.SetActive(true);
    }

    void FixedUpdate()
    {
        if (delaying)
        {
            timer += Time.fixedDeltaTime;
            if (timer >= delayedDeactivateDuration)
            {
                delaying = false;
                timer = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (activated && Input.anyKeyDown && !delaying)
        {
            activated = false;
            gameObject.SetActive(false);
        }
    }
}