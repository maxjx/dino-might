using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Respawner : MonoBehaviour
{
    public static Respawner Instance { get; private set; }
    public List<Transform> playerSpawnPoints;       // List of points should be in the order of the direction the player is travelling, smallest being at the start
    public CinemachineVirtualCameraBase playerVcam;
    public List<Transform> mobSpawnPoint;

    void Awake()
    {
        Instance = this;
    }

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
        yield return new WaitForSeconds(0.02f);
        // To "teleport" the camera
        playerVcam.gameObject.SetActive(false);
        
        // Find respawn point based on distance from player
        Vector3 closestPoint = playerSpawnPoints[0].position;
        float pointdiff = obj.transform.position.x - closestPoint.x;    // Distance from player to respawn point
        float shortestDisplacement = pointdiff < 0 ? -pointdiff : pointdiff;
        for (int i = 1; i < playerSpawnPoints.Count; i++)
        {
            Transform point = playerSpawnPoints[i];
            float currPointDiff = obj.transform.position.x - point.position.x;
            // If distance between points have different signs, take the previous closest point
            if ((pointdiff < 0 && currPointDiff > 0) || (pointdiff > 0 && currPointDiff < 0))
            {
                break;
            }

            float pointdisplacement = currPointDiff < 0 ? -currPointDiff : currPointDiff;
            if (pointdisplacement <= shortestDisplacement)
            {
                closestPoint = point.position;
                shortestDisplacement = pointdisplacement;
            }   
            else
            {
                // Since the first and current respawn points (rp) are in the same direction from the player, 
                // and the current respawn point is further than the first respawn point,
                // then the rest of the rp should be further and there is no need to check anymore.
                break;
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

