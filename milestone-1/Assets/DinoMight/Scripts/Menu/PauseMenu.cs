using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using Newtonsoft.Json;

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
        AudioManager.Instance.PauseEffect();
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        AudioManager.Instance.UnpauseEffect();
    }

    public void LoadMenu() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        levelTransition.GetComponent<LevelLoader>().NextLevelAnimation(0);
    }

    public void ResetLevel() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        levelTransition.GetComponent<LevelLoader>().NextLevelAnimation(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator SaveForm() {
        GlobalSave globalSave = new GlobalSave();
        string choicesToJson = JsonUtility.ToJson(globalSave);
        Debug.Log(choicesToJson);

        string NPCDialogueDictJson = JsonConvert.SerializeObject(Global.NPCDialogueDict);
        string NPCCanvasDictJson = JsonConvert.SerializeObject(Global.NPCCanvasDict);
        Debug.Log(NPCCanvasDictJson);
        Debug.Log(NPCDialogueDictJson);        

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
        // form.AddBinaryData("imageAPath", Global.imageAPath);
        // form.AddBinaryData("imageBPath", Global.imageBPath);
        // form.AddBinaryData("imageCPath", Global.imageCPath);
        form.AddField("NPCDialogueDict", NPCDialogueDictJson);
        form.AddField("NPCCanvasDict", NPCCanvasDictJson);


        using (UnityWebRequest www = UnityWebRequest.Post("https://dinomight2.000webhostapp.com/backend/Save.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) {
                informText.text = www.error;
            } else {
                if (www.isDone) {
                    if (www.downloadHandler.text.Contains("Successful update"))
                    {
                        informText.text = "Successful update!";
                        www.Dispose();
                    } 
                    else if (www.downloadHandler.text.Contains("Successful save"))
                    {
                        informText.text = "Successful save!";
                        www.Dispose();
                    }
                    else
                    {
                        informText.text = www.downloadHandler.text;
                    }
                }
            }
            www.Dispose();
        }
    }
}