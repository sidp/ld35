using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	[SerializeField]
	private GameObject playerObject;
	
	[SerializeField]
	private GameObject scoreTextObject;
	private Text scoreText;
	
	[SerializeField]
	protected int score = 0;
	private int pickupScore = 0;
	private int distanceScore = 0;
	private float playerStartedFromX;
	private float playerFurtherestX;

	// Use this for initialization
	void Start () {
		playerStartedFromX = playerObject.transform.position.x;
		scoreText = scoreTextObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateScore(); 
	}
	
	void UpdateScore () {
		if (playerObject.transform.position.x > playerFurtherestX) {
			playerFurtherestX = playerObject.transform.position.x;
		}

		float distanceScoreBasis = playerFurtherestX - playerStartedFromX;
		distanceScore = (int) distanceScoreBasis;
		score = distanceScore + pickupScore;

		scoreText.text = "SCORE: " + score + " pts";
	}

	void AddScore (int score) {
		pickupScore += score;
	}
}
