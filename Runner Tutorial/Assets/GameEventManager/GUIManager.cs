using UnityEngine;

public class GUIManager : MonoBehaviour {

	public GUIText score, highscore, gameOverText, instructionsText, runnerText;
	
	public static int highestScore;
	
	public static GUIManager instance;
	
	void Start () {
		instance = this;
		GameEventManager.GameStart += GameStart;
		gameOverText.enabled = false;
		score.enabled = false;
		highscore.enabled = false;
	}

	void Update () {
		if(Input.GetButtonDown("Jump")){
			GameEventManager.TriggerGameStart();
		}
	}
	
	private void GameStart () {
		highestScore = 0;
		gameOverText.enabled = false;
		instructionsText.enabled = false;
		runnerText.enabled = false;
		enabled = false;
		score.enabled = true;
		highscore.enabled = true;
	}
	
	public static void SetScore(int score){
		instance.score.text = score.ToString();
	}
	
	public static void SetHighScore(int score) {
		if(highestScore < score) {
			highestScore = score;
			instance.highscore.text = "High Score: " + score.ToString();	
		}
	}
}