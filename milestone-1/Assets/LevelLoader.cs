using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    public Animator transition;

    public void NextLevelAnimation(int index) {
        StartCoroutine(LoadLevel(index));
    }

    IEnumerator LoadLevel(int levelIndex) {
        transition.SetTrigger("start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelIndex);
    }
}
