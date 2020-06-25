using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingSummonFist : KingSummoner
{
    public override void Summon() {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (player != null) {
			Transform playerInfo = player.GetComponent<Transform>();
			float pos = playerInfo.position.x;
			StartCoroutine(summonFist(prefab, pos));
		}
    }

	private IEnumerator summonFist(Transform _fist, float pos) {
		Instantiate(_fist, new Vector3(pos, 7.05f, 0f), transform.rotation);
		yield return new WaitForSeconds(0.4f);
		Instantiate(_fist, new Vector3(pos + Random.Range(1.5f, 5f), 7.05f, 0f), transform.rotation);
		yield return new WaitForSeconds(0.4f);
		Instantiate(_fist, new Vector3(pos - Random.Range(1.5f, 5f), 7.05f, 0f), transform.rotation);
	}
}
