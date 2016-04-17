using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour {

	[SerializeField]
	private GameObject lookAtObject;
	private Player player;
	
	private float initialZ;
	[SerializeField]
	private float yOffset = 2.0f;
	[SerializeField]
	private float zWhenPlayerIsBig = -8.0f;
	
	private float initialFieldOfView;
	[SerializeField]
	private float fieldOfViewWhenPlayerIsBig = 110.0f;

	private float shakeIntensity = 0.0f;
	private float shakeDecay = 0.0f;
	private Quaternion originalRotation;

	private Camera cameraComponent;

	void Start () {
		player = lookAtObject.GetComponent<Player>();
		initialZ = transform.position.z;
		
		cameraComponent = GetComponent<Camera>();
		initialFieldOfView = cameraComponent.fieldOfView;
	}

	void FixedUpdate () {
		if (lookAtObject != null) {
			Vector3 positionToFollow = lookAtObject.transform.position;
			Vector3 newPosition = transform.position;
			
			// Follow player
			newPosition.x += (positionToFollow.x - newPosition.x) * 0.5f;
			newPosition.y += (positionToFollow.y + yOffset - newPosition.y) * 0.5f;

			if (player.isBig) {
				newPosition.z += (zWhenPlayerIsBig - newPosition.z) * 0.2f;
				cameraComponent.fieldOfView += (fieldOfViewWhenPlayerIsBig - cameraComponent.fieldOfView) * 0.2f;
			} else {
				newPosition.z += (initialZ - newPosition.z) * 0.2f;
				cameraComponent.fieldOfView += (initialFieldOfView - cameraComponent.fieldOfView) * 0.2f;
			}

			transform.LookAt(lookAtObject.transform);

			// Camera shake
			if (shakeIntensity > 0.0f) {
				newPosition.x += Random.Range (-shakeIntensity, shakeIntensity);
				newPosition.y += Random.Range (-shakeIntensity / 2, shakeIntensity / 2);

				transform.rotation = new Quaternion(
					transform.rotation.x + Random.Range (-shakeIntensity, shakeIntensity) * 0.05f,
					transform.rotation.y + Random.Range (-shakeIntensity, shakeIntensity) * 0.05f,
					transform.rotation.z + Random.Range (-shakeIntensity, shakeIntensity) * 0.05f,
					transform.rotation.w + Random.Range (-shakeIntensity, shakeIntensity) * 0.05f);

				shakeIntensity -= shakeDecay;
			}

			// Set position
			transform.position = newPosition;

		}
	}

	public void Shake (float intensity = 1.0f, float decay = 0.1f) {
		shakeIntensity = intensity;
		shakeDecay = decay;
	}
}
