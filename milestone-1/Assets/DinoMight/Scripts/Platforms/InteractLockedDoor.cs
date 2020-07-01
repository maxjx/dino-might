using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractLockedDoor : Interact
{
    [SerializeField] private Key.KeyType keyType;
    [SerializeField] private GameObject noKeyMessage;
    private LockedDoor door;

    private void Awake()
    {
        door = gameObject.GetComponent<LockedDoor>();
    }
    protected override void TriggerAction()
    {

        if (!door.isCurrentlyLocked())
        {
            return;
        }

        KeyHolder keyHolder = player.gameObject.GetComponent<KeyHolder>();

        if (keyHolder.KeyTypeIsPresent(keyType))
        {
            door.Toggle();
            door.ManualTrigger();
            keyHolder.DeleteKey(keyType);
        }
        else
        {
            StartCoroutine(ErrorMessage());
        }
    }

    private IEnumerator ErrorMessage()
    {
        noKeyMessage.SetActive(true);
        yield return new WaitForSeconds(10f);
        noKeyMessage.SetActive(false);
    }
}
