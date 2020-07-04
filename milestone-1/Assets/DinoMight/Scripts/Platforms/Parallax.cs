using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    // private float length;
    // private float startPos;
    // public GameObject camera;
    public Vector2 parallaxAmount;      // 0 means moves with camera, 1 means does not move with camera

    // // Start is called before the first frame update
    // void Start()
    // {
    //     startPos = transform.position.x;
    //     length = GetComponent<SpriteRenderer>().bounds.size.x;
    // }

    // // Update works better than fixed update ??
    // void FixedUpdate()
    // {
    //     float temp = camera.transform.position.x * (1 - parallaxAmount);      // distance moved relative to camera
    //     float dist = camera.transform.position.x * parallaxAmount;          // position from startPos

    //     transform.position = new Vector2(startPos + dist, transform.position.y);

    //     if (temp > startPos + length)
    //     {
    //         startPos += length;     // transforms image by length when camera reaches the end
    //     }
    //     else if (temp < startPos - length)
    //     {
    //         startPos -= length;
    //     }
    // }

    // public bool infiniteHorizontal = true;
    // public bool infiniteVertical = false;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private float textureUnitSizeX;
    private float textureUnitSizeY;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
        textureUnitSizeY = texture.height / sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxAmount.x, deltaMovement.y * -parallaxAmount.y);
        lastCameraPosition = cameraTransform.position;

        // if (infiniteHorizontal)
        // {
        //     if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
        //     {
        //         float offsetPosX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
        //         transform.position = new Vector3(cameraTransform.position.x + offsetPosX, transform.position.y);
        //     }
        // }
        // if (infiniteVertical)
        // {
        //     if (Mathf.Abs(cameraTransform.position.y - transform.position.y) >= textureUnitSizeY)
        //     {
        //         float offsetPosY = (cameraTransform.position.y - transform.position.y) % textureUnitSizeY;
        //         transform.position = new Vector3(cameraTransform.position.x, transform.position.y + offsetPosY);
        //     }
        // }
    }
}
