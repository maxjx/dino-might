using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int pooledAmount;

        public List<GameObject> pooledObjectList;
    }

    public static ObjectPooler Instance;
    public List<Pool> pools;
    [Tooltip("Allows dynamic creation of objects when the all other objects in the pool are fully utilised")]
    public bool poolsCanGrow = true;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // For each type of object listed in pools,
        foreach (Pool pool in pools)
        {
            // Make a list and instantiate and store the gameobjects
            List<GameObject> newPooledObjectList = new List<GameObject>();

            for (int x = 0; x < pool.pooledAmount; x++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                newPooledObjectList.Add(obj);
            }

            pool.pooledObjectList = newPooledObjectList;
        }
    }

    // Acts like Instantiate() but by identifying the prefab with its tag as provided in the ObjectPooler instance
    public GameObject SpawnFromPool(string tag, Vector2 position, Quaternion rotation)
    {
        Pool pool = SearchTag(tag);

        if (pool == null)
        {
            Debug.LogWarning("orhor here got no such tag called " + tag + " leh");
            return null;
        }

        List<GameObject> existingPooledObjectList = pool.pooledObjectList;

        // foreach object in the list with the tag
        foreach (GameObject obj in existingPooledObjectList)
        {
            // If obj is inactive, return obj to be spawned
            if (!obj.activeInHierarchy)
            {
                obj.transform.position = position;
                obj.transform.rotation = rotation;
                obj.SetActive(true);
                return obj;
            }
        }

        // If no object in the pool is inactive and available for use, 
        // then check if the pool is able to grow to allow instantiation of more objects.
        if (poolsCanGrow)
        {
            GameObject objectToSpawn = Instantiate(pool.prefab);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

            existingPooledObjectList.Add(objectToSpawn);
            return objectToSpawn;
        }
        else    // the pool cannot grow
        {
            GameObject objectToRespawn = existingPooledObjectList[0];
            objectToRespawn.transform.position = position;
            objectToRespawn.transform.rotation = rotation;
            objectToRespawn.SetActive(true);

            return objectToRespawn;
        }
    }

    // Returns Pool with the associated tag from the list of pools
    Pool SearchTag(string tag)
    {
        foreach (Pool pool in pools)
        {
            if (pool.tag == tag)
            {
                return pool;
            }
        }

        return null;
    }
}
