using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHolder : MonoBehaviour
{
    private List<Key.KeyType> keyList;
    private void Start()
    {
        keyList = new List<Key.KeyType>();
    }

    public void AddKey(Key.KeyType key)
    {
        keyList.Add(key);
        Debug.Log(keyList);
    }

    public void DeleteKey(Key.KeyType key)
    {
        if (keyList.Contains(key))
        {
            keyList.Remove(key);
        }
    }

    public bool KeyTypeIsPresent(Key.KeyType key)
    {
        return keyList.Contains(key);
    }
}
