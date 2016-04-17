using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHelpers : MonoBehaviour {

	public void ExitGame() {
		Application.Quit();
	}

	public void LoadScene(int level) {
		SceneManager.LoadScene(level);
	}
}
