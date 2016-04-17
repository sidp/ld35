using UnityEngine;
using System.Collections.Generic;

struct LevelPart
{
	public GameObject part;
	public float width;
	public float height;
	
	public LevelPart(GameObject p, float w, float h) {
		part = p;
		width = w;
		height = h;
	}
}

public class LevelGenerator : MonoBehaviour {

	[SerializeField]
	private GameObject player;
	private Collider playerCollider;

	[SerializeField]
	private GameObject[] levelParts;
	private List<LevelPart> parts = new List<LevelPart>();
	[SerializeField]
	private GameObject startPart;
	
	private float levelWidth = 0.0f;
	private float levelHeight = 0.0f;

	private float partWidth = 97f; // 94.25f
	private float partHeight = 44.5f; // 60.0f

	void Start () {
		for (int i = 0; i < levelParts.Length; i++)
		{
			parts.Add(new LevelPart(
				levelParts[i],
				partWidth, // todo: calculate this based on the actual mesh
				partHeight
			));
		}

		levelWidth = partWidth;
		levelHeight = partHeight;

		playerCollider = player.GetComponent<SphereCollider>();
	}

	void Update () {
		if (playerCollider.bounds.center.x > levelWidth - 80.0f) {
			// Time to add another part to the level
			int index = Random.Range(0, parts.Count);
			LevelPart newPart = parts[index];

			Vector3 position = new Vector3(
				levelWidth + partWidth / 2.0f - 22.75f,
				-levelHeight - partHeight / 2.0f + 6.7f,
				0.0f
			);

			Vector3 rotation = new Vector3(0.0f, 180.0f, 0.0f);

			Instantiate(newPart.part, position, Quaternion.Euler(rotation));

			levelWidth += partWidth;
			levelHeight += partHeight;
		}	
	}
}
