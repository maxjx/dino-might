using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public Transform firePoint;     // Point at which the bulletPrefab appears
    public GameObject bulletPrefab; // Type of attack prefab

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Creates a bulletPrefab object at the position and rotation of the firePoint
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
