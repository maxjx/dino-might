using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingSummonerCards : KingSummoner
{
    public override void Summon()
    {
        Instantiate(prefab, new Vector3(60f, 6f, 0f), transform.rotation);
        Instantiate(prefab, new Vector3(65f, 7f, 0f), transform.rotation);
        Instantiate(prefab, new Vector3(70f, 6f, 0f), transform.rotation);
    }
}
