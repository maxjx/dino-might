using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardSummonFactory : MonoBehaviour
{
    public void Summon(string type)
    {
        switch (type)
        {
            case "ranged":
                gameObject.GetComponent<WizardSummonerRange>().Summon();
                break;
        }
    }
}
