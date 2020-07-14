using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script is on player but will not call ontriggerenter bc its colliders are not triggers. 
// PlayerHealth calls ManualTransition which transitions to the same scene, when sceneName is null.
public class PlayerTransition : LevelTransition
{

    // Start is called before the first frame update
    void Start()
    {
        if (sceneName == "null")
            sceneName = SceneManager.GetActiveScene().name;   
    }
}
