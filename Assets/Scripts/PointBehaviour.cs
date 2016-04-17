using UnityEngine;

public class PointBehaviour : MonoBehaviour {

	[SerializeField]
	private int pointValue = 3;

	[SerializeField]
	private GameObject glitter;

	void OnTriggerEnter (Collider collider) {
		if (collider.gameObject.tag == "Player") {
			Player player = collider.gameObject.GetComponent<Player>();
			player.AssignScore(pointValue);
			Instantiate(glitter, gameObject.transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
