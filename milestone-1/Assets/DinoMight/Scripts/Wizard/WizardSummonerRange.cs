using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardSummonerRange : KingSummoner
{
    public GameObject leftPos;
    public GameObject rightPos;
    public float attackSpeed;
    public float attackSpacing;
    private Vector3 wizardPosition;


    public override void Summon()
	{
        wizardPosition = GetComponent<Transform>().position;
        StartCoroutine(SummonRangeLeft());
        StartCoroutine(SummonRangeRight());
    }

    private IEnumerator SummonRangeLeft()
    {
        float xPos = wizardPosition.x - 0.5f;
        float xLimit = leftPos.transform.position.x;

        while (xPos > xLimit)
        {
            Instantiate(prefab, new Vector3(xPos, -16.5f, 0f), transform.rotation);
            xPos -= attackSpacing;
            yield return new WaitForSeconds(attackSpeed);
        }
    }

    private IEnumerator SummonRangeRight()
    {
        float xPos = wizardPosition.x + 0.5f;
        float xLimit = rightPos.transform.position.x;

        while (xPos < xLimit)
        {
            Instantiate(prefab, new Vector3(xPos, -16.5f, 0f), transform.rotation);
            xPos += attackSpacing;
            yield return new WaitForSeconds(attackSpeed);
        }
    }
}
