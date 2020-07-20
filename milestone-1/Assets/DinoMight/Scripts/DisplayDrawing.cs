using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DisplayDrawing : MonoBehaviour
{
    public string imageID;      // A,B,C

    void Start()
    {
        byte[] bytearr = File.ReadAllBytes(Application.dataPath + "/Resources/" + imageID + ".png");
        Texture2D tex = new Texture2D(1, 1);
        ImageConversion.LoadImage(tex, bytearr);
        GetComponent<RawImage>().texture = tex;
    }
}
