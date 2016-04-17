using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	[SerializeField]
	private float minSize = 1.0f;
	[SerializeField]
	private float maxSize = 4.0f;
	[SerializeField]
	private float minMass = 0.5f;
	[SerializeField]
	private float maxMass = 10.0f;
	[SerializeField]
	private float minDrag = 0.0f;
	[SerializeField]
	private float maxDrag = 0.5f;

	public bool isBig = false;
	private bool isOnGround = true;

	Rigidbody rb;

	public float size;
	private float easeTime = 0.0f;

	void Start () {
		rb = GetComponent<Rigidbody>();
		size = minSize;
		CheckIfOnGround();
	}

	void Update () {
		
	}
	
	void FixedUpdate () {
		CheckIfOnGround();

		if (isOnGround) {
			// float forceX = Input.GetAxis("Horizontal") * movementAcceleration;
			float forceX = 0.75f; // Always apply a little bit of force

			if (isBig) {
				forceX = 0.0f;
			}

			Vector3 force = new Vector3(forceX, 0.0f, 0.0f);

			if (Input.GetKeyDown("space")) {
				force.y = 200.0f;
			}
			rb.AddForce(force);
		}

		isBig = Input.GetKey("space");

		if (isBig && easeTime < 1.0f) {
			easeTime += 0.2f;
		}

		if (!isBig && easeTime > 0.0f) {
			easeTime -= 0.2f;
		}

		float localEaseTime = 0.5f - Mathf.Cos( easeTime * Mathf.PI ) / 2.0f;
		size = minSize + ((maxSize - minSize) * localEaseTime);

		rb.mass = minMass + (maxMass - minMass) * easeTime;
		rb.drag = minDrag + (maxDrag - minDrag) * easeTime;

		gameObject.transform.localScale = new Vector3(size, size, size);

		// Keep at z 0
		Vector3 fixPosition = gameObject.transform.position;
		fixPosition.z = 0.0f;
		gameObject.transform.position = fixPosition;
	}

	void CheckIfOnGround() {
		Vector3 testForGroundAt = new Vector3(
			transform.position.x,
			transform.position.y - (size / 2.0f) - 0.4f,
			transform.position.z
		);

		isOnGround = Physics.Linecast(
			transform.position,
			testForGroundAt,
			1 << LayerMask.NameToLayer("Ground"));
	}
}
