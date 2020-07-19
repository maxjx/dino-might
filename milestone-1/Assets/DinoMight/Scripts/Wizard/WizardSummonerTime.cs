using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardSummonerTime : KingSummoner
{
    public float effectTime;
    private GameObject player;

    public override void Summon()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            StartCoroutine(SlowTime());
        }
    }

    private IEnumerator SlowTime()
    {
        Instantiate(prefab, new Vector3(2.06f, -12.6f, 0f), transform.rotation);
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        yield return new WaitForSeconds(effectTime);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }
}
