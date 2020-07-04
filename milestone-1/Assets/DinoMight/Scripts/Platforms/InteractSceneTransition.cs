using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSceneTransition : Interact
{
    public int index = -1;
    public string sceneName = "null";
    public GameObject levelTransitionObj;

    protected override void TriggerAction()
    {
        if (name != "null")
        {
            levelTransitionObj.GetComponent<LevelLoader>().NextLevelAnimation(sceneName);
        }
        else if (index != -1)
        {
            levelTransitionObj.GetComponent<LevelLoader>().NextLevelAnimation(index);
        }
        else
        {
            Debug.Log("Invalid level input!");
        }
    }
}
