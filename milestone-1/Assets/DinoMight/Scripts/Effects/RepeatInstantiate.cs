using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatInstantiate : InstantiateEffect
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("instantiateEffect", 3f, 3f);
    }
}
