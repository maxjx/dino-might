using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardSummonerCannon : KingSummoner
{

    public override void Summon()
    {
        Instantiate(prefab, new Vector3(5.76f, -14f, 0f), transform.rotation);
        Instantiate(prefab, new Vector3(-1.76f, -14f, 0f), transform.rotation);
    }
}
