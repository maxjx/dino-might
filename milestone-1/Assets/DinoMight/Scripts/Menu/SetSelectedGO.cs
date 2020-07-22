using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetSelectedGO : MonoBehaviour
{
    public bool onEnable = true;
    public bool onAnyKey = false;

    void OnEnable()
    {
        if (onEnable)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
    }

    void Update()
    {
        if (onAnyKey)
        {
            if (Input.anyKeyDown)
            {
                gameObject.GetComponent<Button>().onClick.Invoke();
            }
        }
    }
}
