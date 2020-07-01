using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractLockedDoor : Interact
{
    [SerializeField] private Key.KeyType keyType;
    [SerializeField] private GameObject noKeyMessage;

    protected override void TriggerAction()
    {
        LockedDoor door = gameObject.GetComponent<LockedDoor>();
        if (!door.isCurrentlyLocked())
        {
            return;
        }

        KeyHolder keyHolder = player.gameObject.GetComponent<KeyHolder>();
        if (keyHolder.KeyTypeIsPresent(keyType))
        {
            gameObject.GetComponent<LockedDoor>().Toggle();
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
