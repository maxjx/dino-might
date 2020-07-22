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
        //byte[] bytearr = File.ReadAllBytes(Application.dataPath + "/Resources/" + imageID + ".png");
        Texture2D tex = new Texture2D(1, 1);
        //ImageConversion.LoadImage(tex, bytearr);
        //GetComponent<RawImage>().texture = tex;
        switch (imageID)
        {
            case "imageA":
                if (Global.imageAPath != null)
                {
                    ImageConversion.LoadImage(tex, Global.imageAPath);
                    GetComponent<RawImage>().texture = tex;
                }
                break;
            case "imageB":
                if (Global.imageBPath != null)
                {
                    ImageConversion.LoadImage(tex, Global.imageBPath);
                    GetComponent<RawImage>().texture = tex;
                }
                break;
            case "imageC":
                if (Global.imageCPath != null)
                {
                    ImageConversion.LoadImage(tex, Global.imageCPath);
                    GetComponent<RawImage>().texture = tex;
                }
                break;
            default:
                break;
        }
    }
}
