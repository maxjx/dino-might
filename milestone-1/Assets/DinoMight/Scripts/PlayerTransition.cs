using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script is on player but will not call ontriggerenter bc its colliders are not triggers.
public class PlayerTransition : LevelTransition
{

    // Start is called before the first frame update
    void Start()
    {
        if (sceneName == "null")
            sceneName = SceneManager.GetActiveScene().name;   
    }
}
