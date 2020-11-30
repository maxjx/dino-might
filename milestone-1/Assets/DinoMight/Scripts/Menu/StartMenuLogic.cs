using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using SimpleJSON;
using Newtonsoft.Json;

public class StartMenuLogic : MonoBehaviour {
    public GameObject transition;
    public Text errorMessage;
    public Button play, settings, credits, load, quit;
    [SerializeField] private GameObject loadMusic;

    public void Play() {
        Global.ResetGlobal();
        loadMusic.GetComponent<MusicLoader>().PlayMusic(1);
        transition.GetComponent<LevelLoader>().NextLevelAnimation("Introduction");
        Debug.Log(Global.playerId);
    }
    public void PlayImmediate() {
        loadMusic.GetComponent<MusicLoader>().PlayMusic(1);
        SceneManager.LoadScene(1);
        Debug.Log(Global.playerId);
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

        using (UnityWebRequest www = UnityWebRequest.Post("https://dinomight2.000webhostapp.com/backend/Load.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) {
                errorMessage.text = www.error;
            } else {
                if (www.downloadHandler.text.Contains("No saved data found")) {
                    errorMessage.text = www.downloadHandler.text;
                } else {
                    Debug.Log(www.downloadHandler.text);
                    var textArray = JSON.Parse(www.downloadHandler.text);
                    int index = textArray[0]["level"];
                    float x = textArray[0]["Xcoordinate"];
                    float y = textArray[0]["Ycoordinate"];
                    Global.playerMaxHealth = (int) textArray[0]["playerMaxHealth"];
                    Global.kickDmg = (int) textArray[0]["kickDmg"];
                    Global.fireballDmg = textArray[0]["fireballDmg"];
                    Global.canDash = textArray[0]["canDash"] == "1" ? true : false;
                    Global.questNumber = (int) textArray[0]["questNumber"];
                    Global.kingSpared = textArray[0]["kingSpared"] == "1" ? true : false;
                    Global.masterSpared = textArray[0]["masterSpared"] == "1" ? true : false;

                    GlobalSave gs = JsonUtility.FromJson<GlobalSave>(textArray[0]["choices"]);
                    Global.priorities = gs.priorities;
                    Global.challenges = gs.challenges;
                    Global.habits = gs.habits;
                    Global.playedMonologueList = gs.playedMonologueList;

                    // Global.imageAPath = System.Text.Encoding.ASCII.GetBytes(textArray[0]["imageAPath"].ToString());
                    // Global.imageBPath = System.Text.Encoding.ASCII.GetBytes(textArray[0]["imageBPath"].ToString());
                    // Global.imageCPath = System.Text.Encoding.ASCII.GetBytes(textArray[0]["imageCPath"].ToString());

                    Global.NPCCanvasDict = JsonConvert.DeserializeObject<Dictionary<string, int>>(textArray[0]["NPCCanvasDict"]);
                    Global.NPCDialogueDict = JsonConvert.DeserializeObject<Dictionary<string, int>>(textArray[0]["NPCDialogueDict"]);

                    transition.GetComponent<LevelLoader>().NextLevelAnimationLoad(index, x, y);
                    loadMusic.GetComponent<MusicLoader>().PlayMusic(index);
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
