using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour {
    public int index;
    public GameObject levelTransitionObj;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            levelTransitionObj.GetComponent<LevelLoader>().NextLevelAnimation(index);
        }
    }

    public void ManualTransition()
    {
        levelTransitionObj.GetComponent<LevelLoader>().NextLevelAnimation(index);
    }
}
