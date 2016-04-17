using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour {

	void Start () {
		Destroy(gameObject, 5.0f);

		CameraPosition cameraPosition = Camera.main.GetComponent<CameraPosition> ();
		if (cameraPosition != null) {
			cameraPosition.Shake(1.0f, 0.05f);
		}
	}
}
