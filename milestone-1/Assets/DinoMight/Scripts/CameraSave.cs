using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CameraSave : MonoBehaviour
{
    public RectTransform area;
    public string folder = "/DinoMight/Textures/Display images";
    public string name = "/savedImage.png";
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
        savingCamera.transform.position = area.rect.center;
        if (File.Exists(Application.dataPath + folder + name))
        {
            File.Delete(Application.dataPath + folder + name);
            Debug.Log("overwriting");
        }
        StartCoroutine(CoSave());
    }

    IEnumerator CoSave()
    {
        yield return new WaitForEndOfFrame();
        Debug.Log(Application.dataPath);

        RenderTexture.active = savingTexture;

        Texture2D savedTexture = new Texture2D(savingTexture.width, savingTexture.height);
        savedTexture.ReadPixels(new Rect(0, 0, savingTexture.width, savingTexture.height), 0, 0);
        savedTexture.Apply();

        var data = savedTexture.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + folder + name, data);
        if (File.Exists(Application.dataPath + folder + name))
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
        
    }
}
