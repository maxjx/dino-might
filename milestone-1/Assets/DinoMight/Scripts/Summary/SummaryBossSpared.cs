using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SummaryBossSpared : MonoBehaviour
{
    public GameObject kingImage;
    public GameObject masterImage;
    private TextMeshProUGUI textBox;

    // Start is called before the first frame update
    void Start()
    {
        textBox = GetComponent<TextMeshProUGUI>();
        if (!Global.kingSpared && !Global.masterSpared)
        {
            textBox.text = "<<You have no friends because you said bye to the King and Master T^T>>";
            kingImage.SetActive(false);
            masterImage.SetActive(false);
        }
        else
        {
            if (Global.kingSpared)
            {
                kingImage.SetActive(true);
            }
            if (Global.masterSpared)
            {
                masterImage.SetActive(true);
            }
            textBox.text = "Your new friends XD";
        }
    }
}
