using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetSelectedGO : MonoBehaviour
{
    public bool onEnable = true;

    void OnEnable()
    {
        if (onEnable)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
    }
}
