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

    void Start()
    {
        if (player != null)
        {
            pm = player.GetComponent<playerMovement>();
            attack = player.GetComponent<Attack>();
            health = player.GetComponent<PlayerHealth>();
        }

        if (startDialoguesEvent == null)
            startDialoguesEvent = new UnityEvent();
        if (endDialoguesEvent == null)
            endDialoguesEvent = new UnityEvent();

        // Choose dialogue canvas from list
        if (dialogueCanvases.Count > 1)
        {
            int currCanvas = GetCanvasIndex();
            int prevCanvas = -1;
            Global.NPCCanvasDict.TryGetValue(DM_tag, out prevCanvas);
            Debug.Log(DM_tag + prevCanvas);
            if (currCanvas != prevCanvas)
            {
                RecordDialogueIdInGlobal(0);    // reset dialogue id
                Global.NPCCanvasDict[DM_tag] = currCanvas;      // record
            }
            dialogueCanvas = dialogueCanvases[currCanvas];
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
                Debug.LogWarning("DMTag for DialogueManager is null. Recording this to Global might cause undesirable side effects.");
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Global.questNumber = 2;
        }
    }

    // Find the right canvas to use using the list of task values
    public int GetCanvasIndex()
    {
        int currCanvas = dialogueCanvasesTaskValue.IndexOf(Global.questNumber);
        if (currCanvas != -1)
        {
            return currCanvas;
        }
        else
        {
            // quest number must be nearest smaller value to use that canvas
            for (int i = 0; i < dialogueCanvasesTaskValue.Count; i++)
            {
                if (Global.questNumber < dialogueCanvasesTaskValue[i])
                {
                    return i;
                }
            }
            return dialogueCanvasesTaskValue.Count - 1;
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

    public void StartDialogue()
    {
        startDialoguesEvent.Invoke();

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
        if (player != null)
        {
            pm.ToggleStartStopMovement();
            attack.enabled = !attack.enabled;
            immune = !immune;
            health.Immunity(immune);
        }
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

    // Used by Dialogue to imitate 2 way conversation with no choices
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

    // Used when making choices in dialogues
    // identifier variable is used to identify the choice made, which is then recorded to global for summary.
    public void UpdateGlobalChoice(string CategoryIdentifier)
    {
        switch (int.Parse(CategoryIdentifier[0].ToString()))
        {
            case 1:
                Global.priorities.Add(CategoryIdentifier.Remove(0, 1));//.identifier);
                break;
            case 2:
                Global.challenges.Add(CategoryIdentifier.Remove(0, 1));//.identifier);
                break;
            case 3:
                Global.habits.Add(CategoryIdentifier.Remove(0, 1));//.identifier);
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
