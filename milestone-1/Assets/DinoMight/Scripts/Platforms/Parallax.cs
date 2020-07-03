using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length;
    private float startPos;
    public GameObject camera;
    [Range(0, 1)] public float parallaxAmount;      // 0 means moves with camera, 1 means does not move with camera

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = camera.transform.position.x * (1-parallaxAmount);      // distance moved relative to camera
        float dist = camera.transform.position.x * parallaxAmount;          // position from startPos

        transform.position = new Vector2(startPos + dist, transform.position.y);

        if (temp > startPos + length)
        {
            startPos += length;     // transforms image by length when camera reaches the end
        }
        else if (temp < startPos - length)
        {
            startPos -= length;
        }
    }
}
