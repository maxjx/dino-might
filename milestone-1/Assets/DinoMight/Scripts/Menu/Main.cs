using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main Instance;
    public backend backend;
    void Start()
    {
        Instance = this;
        backend = GetComponent<backend>();
    }
}
