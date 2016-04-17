using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	[SerializeField]
	private GameObject gameManagerObject;
	private GameManager gameManager;
	[SerializeField]
	private GameObject explosion;

	public float health = 3;

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

	private bool isAlive = true;
	public bool isBig = false;
	private bool isOnGround = true;

	Rigidbody rb;

	AudioSource audioSource;
	[SerializeField]
	private AudioClip playerBig;
	[SerializeField]
	private AudioClip playerSmall;

	public float size;
	private float easeTime = 0.0f;

	void Start () {
		gameManager = gameManagerObject.GetComponent<GameManager>();
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
		size = minSize;
		CheckIfOnGround();
	}

	void Update () {
		if (health <= 0.0f && isAlive) {
			isAlive = false;
			gameObject.GetComponent<Rigidbody>().isKinematic = true;
			gameManager.EndGame();
		}
	}

	void FixedUpdate () {
		CheckIfOnGround();

		if (isBig && Input.GetButtonUp("Jump")) {
			audioSource.Stop();
			audioSource.clip = playerSmall;
			audioSource.Play();
		}

		if (!isBig && Input.GetButtonDown("Jump")) {
			audioSource.Stop();
			audioSource.clip = playerBig;
			audioSource.Play();
		}

		if (isAlive) {
			isBig = Input.GetButton("Jump");
		}

		if (Input.GetButtonDown("Jump")) {
			CameraPosition cameraPosition = Camera.main.GetComponent<CameraPosition> ();
			if (cameraPosition != null) {
				cameraPosition.ShakeSpotlight(2.0f, 0.25f);
			}
		}

		if (isOnGround) {
			float forceX = Input.GetAxis("Horizontal") * 10.0f;
			// float forceX = 10.0f; // Always apply a little bit of force
			float forceY = 0.0f;

			if (isBig) {
				forceX = 0.0f;
				forceY = 0.0f;
			}

			if (Input.GetButtonDown("Jump")) {
				forceY = 20.0f;
			}

			Vector3 force = new Vector3(forceX, forceY, 0.0f);
			if (isAlive) {
				rb.AddForce(force);
			}
		}

		if (isBig && easeTime < 1.0f) {
			easeTime += 0.2f;
		}

		if (!isBig && easeTime > 0.0f) {
			easeTime -= 0.2f;
		}

		easeTime = Mathf.Clamp(easeTime, 0.0f, 1.0f);

		float localEaseTime = 0.5f - Mathf.Cos( easeTime * Mathf.PI ) / 2.0f;
		size = minSize + ((maxSize - minSize) * localEaseTime);

		rb.mass = minMass + (maxMass - minMass) * easeTime;
		rb.drag = minDrag + (maxDrag - minDrag) * easeTime;

		gameObject.transform.localScale = new Vector3(size, size, size);
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
	
	public void AssignScore(int score) {
		gameManager.AddScore(score);
	}
	
	public void Damage(float damage) {
		health -= damage;
	}

	public void KillInstantly() {
		health = 0;
	} 
}
