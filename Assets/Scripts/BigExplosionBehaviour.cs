using UnityEngine;

public class BigExplosionBehaviour : MonoBehaviour {

	void Start () {
		CameraPosition cameraPosition = Camera.main.GetComponent<CameraPosition> ();
		if (cameraPosition != null) {
			cameraPosition.Shake(2.0f, 0.02f);
			cameraPosition.ShakeSpotlight(3.0f, 0.01f);
		}
	}
}
