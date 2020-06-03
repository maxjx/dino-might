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

        //public List<GameObject> pooledObjectList;
    }

    public static ObjectPooler Instance;
    public List<Pool> pools;
    [Tooltip("Allows dynamic creation of objects when the all other objects in the pool are fully utilised")]
    public bool poolCanGrow = true;
    public Dictionary<string, List<GameObject>> pooledObjectDictionary;
    //private Dictionary<string, Pool> poolDataDictionary;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        pooledObjectDictionary = new Dictionary<string, List<GameObject>>();
        //poolDataDictionary = new Dictionary<string, Pool>();

        // For each type of object listed in pools,
        foreach (Pool pool in pools)
        {
            // Make a list and instantiate and store the gameobjects
            List<GameObject> pooledObjectList = new List<GameObject>();

            for (int x = 0; x < pool.pooledAmount; x++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                pooledObjectList.Add(obj);
            }

            pooledObjectDictionary.Add(pool.tag, pooledObjectList);

            // Supplying the poolDataDictionary so that its data such as prefab and poolCanGrow can be referenced easily
            //poolDataDictionary.Add(pool.tag, pool);
        }
    }

    // Acts like Instantiate() but by identifying the prefab with its tag as provided in the ObjectPooler instance
    public GameObject SpawnFromPool(string tag, Vector2 position, Quaternion rotation)
    {
        if (!pooledObjectDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("orhor here got no such tag called " + tag + " leh");
            return null;
        }

        List<GameObject> pooledObjectList = pooledObjectDictionary[tag];

        // foreach object in the list with the tag
        foreach (GameObject obj in pooledObjectList)
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
        // if (pool.poolCanGrow)
        // {
        //     GameObject objectToSpawn = Instantiate(pool.prefab);
        //     return objectToSpawn;
        // }

        // There is no such tag, all objects in the pool are active and the pool cannot grow
        GameObject objectToSpawn = pooledObjectList[0];
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);
        return objectToSpawn;
    }
}
