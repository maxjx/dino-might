using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateEffect : MonoBehaviour
{
    public GameObject effectPrefab;

    public void instantiateEffect()
    {
        Instantiate(effectPrefab, transform.position, transform.rotation);
    }
}
