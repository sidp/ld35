using UnityEngine;

public class GlitterBehaviour : MonoBehaviour {

	private AudioSource audioSource;

	void Start () {
		audioSource = GetComponent<AudioSource>();
		Destroy(gameObject, 2.0f);

		audioSource.pitch = Random.Range(0.99f, 1.02f);
		audioSource.Play();
	}
}
