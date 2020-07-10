using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public string DM_tag;    // this gameobject's own tag, make sure it is unique
    public List<Canvas> dialogueCanvases;   // Each dialogue canvas stores the dialogues that form 1 whole conversation. 
    public List<int> dialogueCanvasesTaskValue;     // Each Value corresponds to the task number that the same element in dialogue canvases corresponds to.
    public GameObject NPCCamera;            // Optional, depends on whether there is a need for zooming in
    public GameObject player;
    public bool recordConvo = true;
    [Space]
    public UnityEvent startDialoguesEvent;    // invokes these events when start dialogues triggered
    public UnityEvent endDialoguesEvent;    // invokes these events when end dialogues triggered

    private Canvas dialogueCanvas;           // This dialogueCanvas refers to the chosen 1 that fits the story at this point in time.
    private int previousCanvasId = 0;           // to know when to reset dialogue id in global
    private Animator dialogueBackground;
    private TextMeshProUGUI nameBox;         // TMPro textbox for name
    //private Animator escapeButton;
    private Dialogue currentDialogue;
    private List<Dialogue> dialogueList = new List<Dialogue>();    // list of dialogues under canvas that are entry points to the dialogue thread
    private DialogueTrigger npcTrigger;     //DEPENDENCY

    private playerMovement pm;
    private Attack attack;
    private PlayerHealth health;
    private bool immune = false;            // to toggle health
    private bool nameDisplayed = false;

    void Awake()
    {
        pm = player.GetComponent<playerMovement>();
        attack = player.GetComponent<Attack>();
        health = player.GetComponent<PlayerHealth>();

        if (endDialoguesEvent == null)
            endDialoguesEvent = new UnityEvent();
        // }

        // void Start()
        // {
        // Choose dialogue canvas from list
        // Canvas to use: Global.questNumber == 0 || 1 - canvas[0], 2 || 3 - canvas[1], ...
        if (dialogueCanvases.Count > 1)
        {
            // switch (Global.questNumber)
            // {
            //     case 0:
            //     case 1:
            //         dialogueCanvas = dialogueCanvases[0];
            //         break;
            //     case 2:
            //         dialogueCanvas = dialogueCanvases[1];
            //         // Restart dialogue since new dialogue
            //         RecordDialogueIdInGlobal(0);
            //         break;
            //     case 3:
            //         dialogueCanvas = dialogueCanvases[1];
            //         break;
            //     default:
            //         break;
            // }
            int canvasIndex = GetCanvasIndex();
            if (canvasIndex != previousCanvasId)
            {
                RecordDialogueIdInGlobal(0);
            }
            dialogueCanvas = dialogueCanvases[canvasIndex];
            previousCanvasId = canvasIndex;
        }
        else
        {
            dialogueCanvas = dialogueCanvases[0];
        }

        // Set up references under dialogue canvas
        foreach (Transform child in dialogueCanvas.transform)
        {
            if (child.gameObject.CompareTag("DialogueBackground"))
            {
                dialogueBackground = child.GetComponent<Animator>();
                continue;
            }
            if (child.gameObject.CompareTag("NameTextbox"))
            {
                nameBox = child.GetComponent<TextMeshProUGUI>();
                continue;
            }
            // if (child.gameObject.CompareTag("EscapeButton"))
            // {
            //     escapeButton = child.GetComponent<Animator>();
            //     continue;
            // }
        }

        // set up link to Global
        if (recordConvo)
        {
            // Ensure tag is appropriate for recording purposes
            if (DM_tag == "")
            {
                Debug.LogWarning("Tag for DialogueManager is null. Recording this to Global might cause undesirable side effects.");
            }

            // Initialise list of dialogues
            foreach (Transform child in dialogueCanvas.transform)       // Transform works as an enumerable apparently
            {
                if (child.gameObject.CompareTag("DialogueEntryPoint"))
                {
                    // only dialogues that are tagged will be needed
                    dialogueList.Add(child.GetComponent<Dialogue>());
                }
            }

            // Find latest dialogue id from global dictionary and add keyvaluepair if not present
            int currDialogueId = 0;
            if (!Global.NPCDialogueDict.TryGetValue(DM_tag, out currDialogueId))
            {
                // this NPC's key is not in the dictionary
                Global.NPCDialogueDict.Add(DM_tag, 0);     // 0 is the first Dialogue in the dialogueList
            }
            // else currDialogueId is now the value in the global dictionary

            // Load dialogue into currentDialogue with dialogue id which has been determined
            try
            {
                currentDialogue = dialogueList[currDialogueId];
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("{0} , means dialogueList is empty so dialogues under canvas need to be tagged as DialogueEntryPoints", e);
            }
        }
        else
        {
            // Not recording convo to Global, get first DialogueEntryPoint in the canvas
            foreach (Transform child in dialogueCanvas.transform)
            {
                if (child.gameObject.CompareTag("DialogueEntryPoint"))
                {
                    currentDialogue = child.GetComponent<Dialogue>();
                    break;
                }
            }
        }
    }

    public int GetCanvasIndex()
    {
        int index = dialogueCanvasesTaskValue.IndexOf(Global.questNumber);
        if (index != -1)
        {
            return index;
        }
        else
        {
            // quest number must be nearest value but not smaller to use that canvas
            for (int i = 0; i < dialogueCanvasesTaskValue.Count; i++)
            {
                if (Global.questNumber < dialogueCanvasesTaskValue[i])
                {
                    return i;
                }
            }
            return dialogueCanvasesTaskValue.Count;
        }
    }

    public void StartDialogue(DialogueTrigger trigger)
    {
        startDialoguesEvent.Invoke();

        if (npcTrigger == null)
        {
            npcTrigger = trigger;
        }

        dialogueBackground.SetTrigger("entry");
        //escapeButton.SetTrigger("entry");

        // Pan camera into NPC view
        if (NPCCamera != null)
        {
            NPCCamera.SetActive(true);
        }

        // Disables/Enables player
        ToggleEnablePlayer();
        currentDialogue.NextSentence();
    }

    void ToggleEnablePlayer()
    {
        pm.ToggleStartStopMovement();
        attack.enabled = !attack.enabled;
        immune = !immune;
        health.Immunity(immune);
    }

    public void EndDialogues()
    {
        if (recordConvo)
        {
            RecordDialogueIdInGlobal(-1);
        }

        //escapeButton.SetTrigger("exit");

        // clear dialogue box
        currentDialogue.EndDialogue();
        ToggleDisplayName("");          // Blank for NPC name textbox, should be ending dialogue anyway

        dialogueBackground.SetTrigger("exit");

        // Pan camera back to normal view
        if (NPCCamera != null)
        {
            NPCCamera.SetActive(false);
        }
        ToggleEnablePlayer();

        if (npcTrigger != null)
        {
            npcTrigger.TurnOnPrompt();
        }

        endDialoguesEvent.Invoke();
    }

    public void ToggleDisplayName(string NPCName)
    {
        if (nameDisplayed)
        {
            nameBox.text = "";
            nameDisplayed = false;
        }
        else
        {
            nameBox.text = NPCName;
            nameDisplayed = true;
        }
    }

    public void SwitchName(string name)
    {
        if (nameDisplayed)
        {
            nameBox.text = name;
        }
        else
        {
            nameBox.text = name;
            nameDisplayed = true;
        }
    }

    // used by Dialogue script to tell this manager that that Dialogue is the current and latest Dialogue triggered
    public void UpdateDialogueRef(Dialogue d)
    {
        currentDialogue = d;
    }

    public void UpdateGlobalChoice(int category)
    {
        switch (category)
        {
            case 1:
                Global.priorities++;
                break;
            case 2:
                Global.challenges++;
                break;
            case 3:
                Global.habits++;
                break;
            default:
                break;
        }
    }

    void RecordDialogueIdInGlobal(int id)
    {

        // id is to be determined
        if (id < 0)
        {
            int currDialogueId = id;
            // Determine current dialogue id
            for (int i = 0; i < dialogueList.Count; i++)
            {
                // ds.gameObject.GetInstanceId() == currentDialogue.gameObject.GetInstanceId()
                if (dialogueList[i] == currentDialogue)
                {
                    currDialogueId = i;
                    break;
                }
            }

            // Could not find current dialogue in list
            if (currDialogueId < 0)
            {
                throw new System.ArgumentOutOfRangeException("ah end dialogue liao but current dialogue wasn't found in the the initialised list leh");
            }

            // Update global dialogue dictionary with current dialogue's id
            Global.NPCDialogueDict[DM_tag] = currDialogueId;
        }
        else
        {
            // Update global dialogue dictionary with current dialogue's id
            Global.NPCDialogueDict[DM_tag] = id;
        }
    }
}
