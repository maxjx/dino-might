using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDrawing : MonoBehaviour
{
    public string imageID;      // A,B,C

    // Start is called before the first frame update
    void Start()
    {
        switch (imageID)
        {
            case "A":
                Texture a = Resources.Load<Texture>(Global.imageAPath);
                if (a!=null)
                {
                    GetComponent<RawImage>().texture = a;
                }
                break;
            case "B":
                Texture b = Resources.Load<Texture>(Global.imageBPath);
                if (b!=null)
                {
                    GetComponent<RawImage>().texture = b;
                }
                break;
            case "C":
                Texture c = Resources.Load<Texture>(Global.imageCPath);
                if (c!=null)
                {
                    GetComponent<RawImage>().texture = c;
                }
                break;
            default:
                break;
        }
    }
}
