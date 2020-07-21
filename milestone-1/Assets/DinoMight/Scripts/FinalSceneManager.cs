using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSceneManager : MonoBehaviour
{
    public GameObject kingNPC;
    public GameObject kingSprite;       // no dialogue
    public GameObject masterNPC;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckSparedBosses()
    {
        masterNPC.SetActive(true);

        // bool kingSpared = Global.kingSpared;
        // bool masterSpared = Global.masterSpared;
        // if (kingSpared && !masterSpared)
        // {
        //     kingNPC.SetActive(true);
        // }
        // else if (kingSpared && masterSpared)
        // {
        //     kingSprite.SetActive(true);
        //     masterNPC.SetActive(true);
        // }
        // else if (!kingSpared && masterSpared)
        // {
        //     masterNPC.SetActive(true);
        // }
    }

    public void EndSparedBosses()
    {
        masterNPC.SetActive(false);
        kingNPC.SetActive(false);
        kingNPC.SetActive(false);
    }
}
