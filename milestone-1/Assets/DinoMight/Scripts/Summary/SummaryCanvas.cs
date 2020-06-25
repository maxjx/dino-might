using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummaryCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowAllChildren());

    }

    IEnumerator ShowAllChildren()
    {
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.4f);
        }
    }
}
