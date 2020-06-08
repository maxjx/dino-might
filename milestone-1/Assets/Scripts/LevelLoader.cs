using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    public Animator transition;
    public GameObject screen;
    public int sceneIndex;

    public void NextLevelAnimation(int index) {
        StartCoroutine(LoadLevel(index));
        Debug.Log(Global.playerLevel);
    }
    public void NextLevelAnimationTest(int sceneIndex) {
        StartCoroutine(LoadLevel(sceneIndex));
    }
    IEnumerator LoadLevel(int levelIndex) {
        transition.SetTrigger("start");
        yield return new WaitForSeconds(1);
        transition.SetTrigger("End");
        SceneManager.LoadSceneAsync(levelIndex);
        yield break;
    }
}
