﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CameraSave : MonoBehaviour
{
    public RectTransform area;
    // Application.dataPath = "...Assets/"
    public string name = "savedImage";
    public GameObject savedText;       // shows saved upon saving

    private RenderTexture savingTexture;      // render texture on which the camera will save the screen to
    private Camera savingCamera;

    // Start is called before the first frame update
    void Start()
    {
        savingCamera = GetComponent<Camera>();
        savingTexture = savingCamera.targetTexture;
    }

    public void SaveScreen()
    {
        savingCamera.aspect = area.sizeDelta.x / area.sizeDelta.y;
        savingCamera.transform.position = area.TransformPoint(0, 0, 0);
        savingCamera.transform.position = new Vector3(savingCamera.transform.position.x, savingCamera.transform.position.y, -10f);
        if (File.Exists(Application.dataPath + "/Resources/" + name + ".png"))
        {
            File.Delete(Application.dataPath + "/Resources/" + name + ".png");
            Debug.Log("overwriting");
        }
        StartCoroutine(CoSave());
    }

    IEnumerator CoSave()
    {
        yield return new WaitForEndOfFrame();

        RenderTexture.active = savingTexture;

        Texture2D savedTexture = new Texture2D(savingTexture.width, savingTexture.height);
        savedTexture.ReadPixels(new Rect(0, 0, savingTexture.width, savingTexture.height), 0, 0);
        savedTexture.Apply();

        var data = savedTexture.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/Resources/" + name + ".png", data);
        if (File.Exists(Application.dataPath + "/Resources/" + name + ".png"))
        {
            StartCoroutine(ShowSaved());
            RecordToGlobal();
        }
    }

    IEnumerator ShowSaved()
    {
        savedText.SetActive(true);
        yield return new WaitForSeconds(2f);
        savedText.SetActive(false);
    }

    public void ChangeSavedName(string newName)
    {
        name = newName;
    }

    void RecordToGlobal()
    {
        switch (name)
        {
            case "imageA":
                Global.imageAPath = name;
                break;
            case "imageB":
                Global.imageBPath = name;
                break;
            case "imageC":
                Global.imageCPath = name;
                break;
            default:
                break;
        }
    }
}