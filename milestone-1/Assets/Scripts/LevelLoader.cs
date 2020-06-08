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
    public void NextLevelAnimationLoad(int sceneIndex, float x, float y) {
        StartCoroutine(LoadLevelLoad(sceneIndex, x, y));
    }

    IEnumerator LoadLevel(int levelIndex) {
        transition.SetTrigger("start");
        yield return new WaitForSeconds(1);
        transition.SetTrigger("End");
        SceneManager.LoadSceneAsync(levelIndex);
        yield break;
    }
    IEnumerator LoadLevelLoad(int levelIndex, float x, float y) {
        transition.SetTrigger("start");
        yield return new WaitForSeconds(1);
        transition.SetTrigger("End");
        SceneManager.LoadScene(levelIndex);
        // GameObject player = GameObject.FindWithTag("Player");
        // player.transform.position = new Vector3(x, y, 0);
        // yield break;
    }
}
