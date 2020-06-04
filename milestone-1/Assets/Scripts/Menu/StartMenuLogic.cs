using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuLogic : MonoBehaviour {
    public GameObject transition;
    public void Play() {
        transition.GetComponent<LevelLoader>().NextLevelAnimation(1);
    }
    public void Quit() {
        Application.Quit();
    }
}
