using UnityEngine;
using System.Collections;

public class DustBehaviour : MonoBehaviour {

	void Start () {
		ParticleSystem ps = GetComponent<ParticleSystem>();
		ps.Clear();
		ps.Play();
		Destroy(gameObject, 2.0f);
	}
}
