using UnityEngine;
using System.Collections;

public class LevelBehaviour : MonoBehaviour {

	[SerializeField]
	GameObject dustObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter (Collision collision) {
		foreach (ContactPoint contact in collision.contacts) {
			Debug.DrawRay(contact.point, contact.normal, Color.white);

			if (collision.gameObject.tag == "Player"
					&& collision.relativeVelocity.magnitude > 5.0f
					&& dustObject != null) {
				Vector3 pos = contact.point;
				Vector3 rot = new Vector3(90.0f, contact.normal.y, contact.normal.z);
				pos -= contact.normal.normalized * 0.5f;
				GameObject dust = (GameObject) Instantiate(dustObject, pos, Quaternion.Euler(rot));
				/*
				Player player = collision.gameObject.GetComponent<Player>();
				dust.transform.localScale = new Vector3(player.size, player.size, player.size);*/
				ParticleSystem ps = dust.GetComponent<ParticleSystem>();
				ps.Play();
			}
        }

		
	}
}
