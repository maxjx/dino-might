using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingLogic : MonoBehaviour {

	public Transform player;
	public bool isFlipped = false;
	public GameObject king;

	public void LookAtPlayer() {
		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x > player.position.x && isFlipped) {
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = false;
		} else if (transform.position.x < player.position.x && !isFlipped) {
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = true;
		}
	}

	// Called on King_teleportStart animation frame
	public void Teleport() {
		king.transform.position = new Vector3(78f, 12f, 0f);
	}

	// Called on King_teleportEnd animation frame
	public void TeleportBack() {
		king.transform.position = new Vector3(68f, 2.68f, 0f);
	}
}
