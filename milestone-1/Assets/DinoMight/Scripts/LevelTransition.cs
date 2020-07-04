using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour {
    public int index = -1;
    public string sceneName = "null";
    public GameObject levelTransitionObj;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            if (name != "null")
            {
                levelTransitionObj.GetComponent<LevelLoader>().NextLevelAnimation(sceneName);
            }
            else if (index != -1)
            {
                levelTransitionObj.GetComponent<LevelLoader>().NextLevelAnimation(index);
            }
            else
            {
                Debug.Log("Invalid level input!");
            }
        }
    }

    public void ManualTransition()
    {
        if (name != "null")
        {
            levelTransitionObj.GetComponent<LevelLoader>().NextLevelAnimation(sceneName);
        }
        else if (index != -1)
        {
            levelTransitionObj.GetComponent<LevelLoader>().NextLevelAnimation(index);
        }
        else
        {
            Debug.Log("Invalid level input!");
        }
    }
}
