using UnityEngine;

public class DynamiteBehaviour : MonoBehaviour {

	[SerializeField]
	private Light sparkLight;
	private float sparkLightIntensity;
	[SerializeField]
	private GameObject explosion;

	// Use this for initialization
	void Start () {
		sparkLightIntensity = sparkLight.intensity;
	}
	
	// Update is called once per frame
	void Update () {
		sparkLight.intensity = Random.Range(sparkLightIntensity - 0.25f, sparkLightIntensity + 0.25f);
	}

	void OnTriggerEnter (Collider collider) {
		if (collider.gameObject.tag == "Player") {
			Player player = collider.gameObject.GetComponent<Player>();
			player.Damage(1.0f);

			Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
