using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public Transform playerSpawnPoint;
    public List<Transform> mobSpawnPoint;

    public void RespawnCharacter(GameObject obj, int mobNumber)
    {
        // Player's mobNumber = 0
        if (mobNumber == 0)
        {
            StartCoroutine(RespawnPlayer(obj));
        }
        else
        {
            StartCoroutine(RespawnMob(obj, mobNumber));
        }
    }

    public IEnumerator RespawnPlayer(GameObject obj)
    {
        yield return new WaitForSeconds(2);
        obj.transform.position = playerSpawnPoint.position;
        obj.SetActive(true);
    }

    public IEnumerator RespawnMob(GameObject obj, int mobNumber)
    {
        yield return new WaitForSeconds(5);
        Transform sp = mobSpawnPoint[mobNumber - 1];
        obj.transform.position = sp.position;
        obj.SetActive(true);
    }
}

