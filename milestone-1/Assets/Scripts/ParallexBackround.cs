using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallexBackround : MonoBehaviour {
    public GameObject mainCamera;
    private Transform cameraTransform;
    private Vector2 prevCameraPosition;
    private void start() {
        cameraTransform = mainCamera.transform;
        prevCameraPosition = cameraTransform.position;
    }

    private void FixedUpdate() {
        Vector2 deltaMovement_x = mainCamera.transform.position.x - prevCameraPosition.x;
        
        transform.position += deltaMovement;
        prevCameraPosition = mainCamera.transform.position;
    }
}
