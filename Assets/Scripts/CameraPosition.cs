using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour {

	[SerializeField]
	private GameObject playerObject;
	[SerializeField]
	private GameObject lookAtObject;
	[SerializeField]
	private bool followLookAtObject = true;

	private Player player;
	[SerializeField]
	private GameObject spotlightObject;
	private Light spotlight;
	private float initialSpotlightAngle;
	[SerializeField]
	private float spotlightAngleWhenPlayerIsBig = 110.0f;
	
	private float initialZ;
	[SerializeField]
	private float yOffset = 2.0f;
	[SerializeField]
	private float zWhenPlayerIsBig = -8.0f;
	
	private float initialFieldOfView;
	[SerializeField]
	private float fieldOfViewWhenPlayerIsBig = 115.0f;

	private float shakeIntensity = 0.0f;
	private float shakeDecay = 0.0f;

	private float spotlightShakeIntensity = 0.0f;
	private float spotlightShakeDecay = 0.0f;

	private Camera cameraComponent;

	void Start () {
		if (playerObject != null) {
			player = playerObject.GetComponent<Player>();
			initialZ = transform.position.z;
		}

		cameraComponent = GetComponent<Camera>();
		initialFieldOfView = cameraComponent.fieldOfView;

		if (spotlightObject != null) {
			spotlight = spotlightObject.GetComponent<Light>();
			initialSpotlightAngle = spotlight.spotAngle;
		}
	}

	void FixedUpdate () {
		if (lookAtObject != null) {
			Vector3 positionToFollow = lookAtObject.transform.position;
			Vector3 newPosition = transform.position;

			// Follow object
			if (followLookAtObject) {
				newPosition.x += (positionToFollow.x - newPosition.x) * 0.5f;
				newPosition.y += (positionToFollow.y + yOffset - newPosition.y) * 0.5f;
			}

			if (cameraComponent != null) {
				if (player != null && player.isBig) {
					newPosition.z += (zWhenPlayerIsBig - newPosition.z) * 0.2f;
					cameraComponent.fieldOfView += (fieldOfViewWhenPlayerIsBig - cameraComponent.fieldOfView) * 0.2f;
					spotlight.spotAngle += (spotlightAngleWhenPlayerIsBig - spotlight.spotAngle) * 0.2f;
				} else {
					newPosition.z += (initialZ - newPosition.z) * 0.2f;
					cameraComponent.fieldOfView += (initialFieldOfView - cameraComponent.fieldOfView) * 0.2f;
					spotlight.spotAngle += (initialSpotlightAngle - spotlight.spotAngle) * 0.2f;
				}
			}

			transform.LookAt(lookAtObject.transform);
			spotlightObject.transform.LookAt(lookAtObject.transform);

			// Spotlight shake
			if (spotlightShakeIntensity > 0.0f) {
				spotlightObject.transform.rotation = new Quaternion(
					spotlightObject.transform.rotation.x + Random.Range (-spotlightShakeIntensity, spotlightShakeIntensity) * 0.05f,
					spotlightObject.transform.rotation.y + Random.Range (-spotlightShakeIntensity, spotlightShakeIntensity) * 0.05f,
					spotlightObject.transform.rotation.z + Random.Range (-spotlightShakeIntensity, spotlightShakeIntensity) * 0.05f,
					spotlightObject.transform.rotation.w + Random.Range (-spotlightShakeIntensity, spotlightShakeIntensity) * 0.05f);

				spotlightShakeIntensity -= spotlightShakeDecay;
			}

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

			if (followLookAtObject) {
				// Set position
				transform.position = newPosition;
			}

		}
	}

	public void Shake (float intensity = 1.0f, float decay = 0.1f) {
		shakeIntensity = intensity;
		shakeDecay = decay;
		spotlightShakeIntensity = intensity * 0.25f;
		spotlightShakeDecay = decay * 0.4f;
	}
	
	public void ShakeSpotlight (float intensity = 1.0f, float decay = 0.1f) {
		spotlightShakeIntensity = intensity;
		spotlightShakeDecay = decay;
	}
}
