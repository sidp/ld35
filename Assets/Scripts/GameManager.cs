using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	[SerializeField]
	private GameObject playerObject;
	private Player player;
	
	[SerializeField]
	private GameObject scoreTextObject;
	private Text scoreText;
	
	[SerializeField]
	private GameObject lifeTextObject;
	private Text lifeText;

	private GameObject uiGameOver;

	public bool gameOver = false;

	[SerializeField]
	protected int score = 0;
	private int pickupScore = 0;
	private int distanceScore = 0;

	private float playerStartedFromX;
	private float playerFurtherestX;

	public void Awake () {
		GameManager.instance = this;
	}

	void Start () {
		player = playerObject.GetComponent<Player>();
		playerStartedFromX = playerObject.transform.position.x;
		scoreText = scoreTextObject.GetComponent<Text>();
		lifeText = lifeTextObject.GetComponent<Text>();
		uiGameOver = GameObject.Find("UIGameOver");
		uiGameOver.SetActive(false);
	}

	void Update () {
		UpdateGameState(); 
	}

	void UpdateGameState () {
		if (!gameOver) {
			if (playerObject.transform.position.x > playerFurtherestX) {
				playerFurtherestX = playerObject.transform.position.x;
			}

			float distanceScoreBasis = playerFurtherestX - playerStartedFromX;
			distanceScore = (int) distanceScoreBasis;
			score = distanceScore + pickupScore;

			scoreText.text = "SCORE: " + score + " pts";
			lifeText.text = "DYNAMITE RESISTANCE: " + Mathf.Round(player.health);
		}
	}

	public void AddScore (int score) {
		if (!gameOver) {
			pickupScore += score;
		}
	}

	public void EndGame () {
		gameOver = true;
		StartCoroutine (DelayedEndGame());
	}

	IEnumerator DelayedEndGame() {
		yield return new WaitForSeconds(2.6f);
		uiGameOver.SetActive(true);
	}
}
