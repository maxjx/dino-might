using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Respawner : MonoBehaviour
{
    public Transform playerSpawnPoint;
    public CinemachineVirtualCameraBase playerVcam;
    public List<Transform> mobSpawnPoint;

    public void RespawnCharacter(GameObject obj, int spawnPointNumber)
    {
        // Player's spawnPointNumber = 0
        if (spawnPointNumber == 0)
        {
            StartCoroutine(RespawnPlayer(obj));
        }
        else
        {
            StartCoroutine(RespawnMob(obj, spawnPointNumber));
        }
    }

    public IEnumerator RespawnPlayer(GameObject obj)
    {
        yield return new WaitForSeconds(2);
        // To "teleport" the camera
        playerVcam.gameObject.SetActive(false);
        
        obj.transform.position = playerSpawnPoint.position;
        obj.SetActive(true);

        playerVcam.gameObject.SetActive(true);
    }

    public IEnumerator RespawnMob(GameObject obj, int spawnPointNumber)
    {
        yield return new WaitForSeconds(5);
        Transform sp = mobSpawnPoint[spawnPointNumber-1];
        obj.transform.position = sp.position;
        obj.SetActive(true);
    }

    public void RespawnThing(GameObject obj)
    {
        StartCoroutine(RespawnThingCoroutine(obj));
    }

    private IEnumerator RespawnThingCoroutine(GameObject obj)
    {
        obj.SetActive(false);
        yield return new WaitForSeconds(7);
        obj.SetActive(true);
    }
}

