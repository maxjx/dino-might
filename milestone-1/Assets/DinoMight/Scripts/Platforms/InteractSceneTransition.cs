using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSceneTransition : Interact
{
    public int index = -1;
    public string sceneName = "null";
    public GameObject levelTransitionObj;
    public bool byTask = false;            // if true, scene transition will only be allowed after a quest/task number is cleared/obtained
    public int taskNumberToClear;
    public DialogueManager dm;              // to say you cant enter if task number is not reached

    private bool dialogueStarted = false;

    protected override void TriggerAction()
    {
        if (!byTask || (byTask && (Global.questNumber >= taskNumberToClear)))
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

            if (GetComponent<MusicPlayer>() != null)
            {
                GetComponent<MusicPlayer>().ManualTransition();
            }
            else
            {
                Debug.Log("No music player component");
            }
        }
        else if (byTask && (Global.questNumber < taskNumberToClear))
        {
            if (dm != null)
            {
                if (!dialogueStarted)
                {
                    dm.StartDialogue();
                    dialogueStarted = true;
                }
                else
                {
                    dialogueStarted = false;
                }
            }
        }
    }

    protected override void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            base.OnTriggerExit2D(collider);
            dialogueStarted = false;
        }
    }
}
