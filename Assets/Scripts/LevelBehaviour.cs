using UnityEngine;

public class LevelBehaviour : MonoBehaviour {

	[SerializeField]
	GameObject dustObject;
	
	void OnCollisionEnter (Collision collision) {
		foreach (ContactPoint contact in collision.contacts) {
			if (collision.gameObject.tag == "Player"
					&& collision.relativeVelocity.magnitude > 5.0f
					&& dustObject != null) {
				Vector3 pos = contact.point;
				Vector3 rot = new Vector3(90.0f, contact.normal.y, contact.normal.z);
				pos -= contact.normal.normalized * 0.5f;
				GameObject dust = (GameObject) Instantiate(dustObject, pos, Quaternion.Euler(rot));
				ParticleSystem ps = dust.GetComponent<ParticleSystem>();
				ps.Play();

				Player player = collision.gameObject.GetComponent<Player>();
				if (player.isBig) {
					CameraPosition cameraPosition = Camera.main.GetComponent<CameraPosition> ();
					if (cameraPosition != null) {
						cameraPosition.Shake(0.25f, 0.02f);
					}
				}
			}
        }
	}
}
