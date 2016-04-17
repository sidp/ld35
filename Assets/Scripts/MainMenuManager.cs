using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

	public static MainMenuManager instance;
	private GameObject uiMainMenu;

	public void Awake () {
		MainMenuManager.instance = this;
	}

	// Use this for initialization
	void Start () {
		uiMainMenu = GameObject.Find("UIMainMenu");
		uiMainMenu.SetActive(false);
		ShowMenu();
	}

	void Update () {
		if (Input.GetKeyDown("escape")) {
			Application.Quit();
		}
	}

	public void ShowMenu () {
		StartCoroutine (DelayedShowMenu());
	}

	IEnumerator DelayedShowMenu() {
		yield return new WaitForSeconds(4.8f);
		uiMainMenu.SetActive(true);
	}
}
