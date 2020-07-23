﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

[Serializable]
public class PauseMenu : MonoBehaviour {
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject levelTransition;
    public Text informText;
    public GameObject player;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Save() {
        Global.Xcoordinate = player.transform.position.x;
        Global.Ycoordinate = player.transform.position.y;
        StartCoroutine(SaveForm());
    }

    public void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void LoadMenu() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        levelTransition.GetComponent<LevelLoader>().NextLevelAnimation(0);
    }

    public void QuitGame() {
        Application.Quit();
    }

    IEnumerator SaveForm() {
        GlobalSave globalSave = new GlobalSave();
        string choicesToJson = JsonUtility.ToJson(globalSave);
        Debug.Log(choicesToJson);

        // DictStringInt NPCDialogueDict = Global.NPCDialogueDict;

        WWWForm form = new WWWForm();
        form.AddField("playerId", Global.playerId);
        form.AddField("playerLevel", SceneManager.GetActiveScene().buildIndex);
        form.AddField("playerHealth", Global.playerHealth);
        form.AddField("Xcoordinate", Global.Xcoordinate.ToString());
        form.AddField("Ycoordinate", Global.Ycoordinate.ToString());
        form.AddField("playerMaxHealth", Global.playerMaxHealth);
        form.AddField("kickDmg", Global.kickDmg);
        form.AddField("fireballDmg", Global.fireballDmg);
        form.AddField("canDash", Global.canDash?1:0);
        form.AddField("questNumber", Global.questNumber);
        form.AddField("kingSpared", Global.kingSpared?1:0);
        form.AddField("masterSpared", Global.masterSpared?1:0);
        form.AddField("choices", choicesToJson);
        form.AddBinaryData("imageAPath", Global.imageAPath);
        form.AddBinaryData("imageBPath", Global.imageBPath);
        form.AddBinaryData("imageCPath", Global.imageCPath);
        form.AddField("choices", choicesToJson);


        using (UnityWebRequest www = UnityWebRequest.Post("https://dinomight.000webhostapp.com/backend/Save.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) {
                informText.text = www.error;
            } else {
                if (www.isDone) {
                    if (www.downloadHandler.text.Contains("Successful")) {
                        informText.text = www.downloadHandler.text;
                        www.Dispose();
                    } else {
                        informText.text = www.downloadHandler.text;
                    }
                }
            }
            www.Dispose();
        }
    }
}