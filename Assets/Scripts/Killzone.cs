using UnityEngine;

public class Killzone : MonoBehaviour {

	void OnTriggerEnter (Collider collider) {
		if (collider.gameObject.tag == "Player") {
			Player player = collider.gameObject.GetComponent<Player>();
			player.KillInstantly();
		}
	}
}
