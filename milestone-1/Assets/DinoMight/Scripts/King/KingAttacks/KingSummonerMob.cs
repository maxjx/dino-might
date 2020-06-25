using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingSummonerMob : KingSummoner {
    public override void Summon() {
        StartCoroutine(SummonChickenDelay());
    }

	private IEnumerator SummonChickenDelay() {
		yield return new WaitForSeconds(1.5f);
		Instantiate(prefab, new Vector3(55f, 2.75f, 0f), transform.rotation);
		Instantiate(prefab, new Vector3(60f, 2.75f, 0f), transform.rotation);
		Instantiate(prefab, new Vector3(65f, 2.75f, 0f), transform.rotation);
		Instantiate(prefab, new Vector3(69f, 2.75f, 0f), transform.rotation);
		Instantiate(prefab, new Vector3(72f, 2.75f, 0f), transform.rotation);
	}
}
