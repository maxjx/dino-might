using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using SimpleJSON;

public class StartMenuLogic : MonoBehaviour {
    public GameObject transition;
    public Text errorMessage;
    public Button play, settings, credits, load, quit;

    public void Play() {
        transition.GetComponent<LevelLoader>().NextLevelAnimation(1);
    }
    public void PlayImmediate() {
        SceneManager.LoadScene(1);
    }

    public void Load() {
        play.interactable = false;
        settings.interactable = false;
        credits.interactable = false;
        load.interactable = false;
        StartCoroutine(LoadForm(Global.playerId.ToString()));
    }

    IEnumerator LoadForm(string playerId) {
        WWWForm form = new WWWForm();
        form.AddField("playerId", playerId);

        using (UnityWebRequest www = UnityWebRequest.Post("https://dinomight.000webhostapp.com/backend/Load.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) {
                errorMessage.text = www.error;
            } else {
                if (www.downloadHandler.text.Contains("No saved data found")) {
                    errorMessage.text = www.downloadHandler.text;
                } else {
                    var textArray = JSON.Parse(www.downloadHandler.text);
                    int index = textArray[0]["level"];
                    transition.GetComponent<LevelLoader>().NextLevelAnimation(index);
                }
            }
        }
        play.interactable = true;
        settings.interactable = true;
        credits.interactable = true;
        load.interactable = true;
    }

    public void Quit() {
        Application.Quit();
    }
}
