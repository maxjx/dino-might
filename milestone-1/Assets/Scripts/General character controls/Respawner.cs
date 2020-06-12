using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Respawner : MonoBehaviour
{
    public List<Transform> playerSpawnPoints;
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
        
        // Find respawn point based on distance from player
        Vector3 closestPoint = playerSpawnPoints[0].position;
        float diff = obj.transform.position.x - closestPoint.x;
        float shortestDisplacement = diff < 0 ? -diff : diff;
        foreach (Transform point in playerSpawnPoints)
        {
            float pointdiff = obj.transform.position.x - point.position.x;
            float pointdisplacement = pointdiff < 0 ? -pointdiff : pointdiff;
            if (pointdisplacement <= shortestDisplacement)
            {
                closestPoint = point.position;
                shortestDisplacement = pointdisplacement;
            }
        }
        obj.transform.position = closestPoint;
        obj.SetActive(true);

        // To "teleport" the camera
        playerVcam.gameObject.SetActive(true);
    }

    public IEnumerator RespawnMob(GameObject obj, int spawnPointNumber)
    {
        yield return new WaitForSeconds(7);
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
        yield return new WaitForSeconds(5);
        obj.SetActive(true);
    }
}

