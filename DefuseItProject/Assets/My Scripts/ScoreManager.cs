using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

	public ModeManager modeManager;
	public WireManager wireManager;

	public int curScore;
	public int highScore;
	public float timeLeft;
	public bool enableTimer;
	public bool debug;


	public Text curScoreText;
	public Text highScoreText;
	public Text timeLeftText;
	public Text ModeText;
	public GameObject gameOverPanel;
	public GameObject colorPanel;
	public GameObject leftPanel;
	public GameObject rightPanel;

	void Start() {
		Time.timeScale = 1.0f;

		if(curScoreText == null) {
			Debug.LogWarning("Current Score Text Object Missing!");
		}
		if(wireManager == null) {
			wireManager = GetComponent<WireManager>();
		}
		if(modeManager == null) {
			modeManager = GetComponent<ModeManager>();
		}

	}

	void LateUpdate() {
		if(enableTimer) {
			if(timeLeft <= 0) {
				GameOver();
			}
			if(debug) {
				Debug.Log("Timer Going!" + timeLeft);
			}
			timeLeft -= Time.deltaTime;
			timeLeftText.text = "Time Left: " + Mathf.Round(timeLeft);
		}
	}

	public void InitialSetupUI() {
		leftPanel.SetActive(true);
		colorPanel.SetActive(true);
		rightPanel.SetActive(true);
		gameOverPanel.SetActive(false);

		if(modeManager.gameMode == ModeManager.GameMode.Practice) {
			curScoreText.enabled = false;
			highScoreText.enabled = false;
			timeLeftText.enabled = false;
			enableTimer = false;
			ModeText.text = "Practice Mode";
		}
		else if(modeManager.gameMode == ModeManager.GameMode.Normal) {
			curScoreText.enabled = true;
			highScoreText.enabled = true;
			timeLeftText.enabled = true;
			curScoreText.text = "Score: 0";
			timeLeft = 120;
			timeLeftText.text = "Time Left: " + timeLeft;
			highScore = PlayerPrefs.GetInt("NormalHS");
			highScoreText.text = "High Score: " + highScore;
			ModeText.text = "Normal Mode";
			enableTimer = true;
		}
		else if(modeManager.gameMode == ModeManager.GameMode.Medium) {
			curScoreText.enabled = true;
			highScoreText.enabled = true;
			timeLeftText.enabled = true;
			curScoreText.text = "Score: 0";
			timeLeft = 60;
			timeLeftText.text = "Time Left: " + timeLeft;
			highScore = PlayerPrefs.GetInt("MediumHS");
			highScoreText.text = "High Score: " + highScore;
			ModeText.text = "Medium Mode";
			enableTimer = true;
		}
		else if(modeManager.gameMode == ModeManager.GameMode.Hard) {
			curScoreText.enabled = true;
			highScoreText.enabled = true;
			timeLeftText.enabled = true;
			curScoreText.text = "Score: 0";
			timeLeft = 120;
			timeLeftText.text = "Time Left: " + timeLeft;
			highScore = PlayerPrefs.GetInt("HardHS");
			highScoreText.text = "High Score: " + highScore;
			ModeText.text = "Hard Mode";
			enableTimer = true;
		}
		else if(modeManager.gameMode == ModeManager.GameMode.Special) {
			curScoreText.enabled = true;
			highScoreText.enabled = false;
			timeLeftText.enabled = false;
			enableTimer = false;
			curScoreText.text = "Score: 0";
			ModeText.text = "Special Mode";
		}

		wireManager.Setup();
	}

	public void AdjustCurPoints(int points) {
		if(modeManager.gameMode != ModeManager.GameMode.Special || modeManager.gameMode != ModeManager.GameMode.Practice) {
			curScore += points;
		}
		if(curScoreText.enabled) {
			curScoreText.text = "Score: "  + curScore.ToString();
		}
	}

	public void ChangeHighScore(int highScorePts) {
		if(modeManager.gameMode == ModeManager.GameMode.Practice) {
			return;
		}
		else if(modeManager.gameMode == ModeManager.GameMode.Normal) {
			highScore = highScorePts;
			highScoreText.text = "High Score: " + highScore.ToString();
			PlayerPrefs.SetInt("NormalHS", highScore);
		}
		else if(modeManager.gameMode == ModeManager.GameMode.Medium) {
			highScore = highScorePts;
			highScoreText.text = "High Score: " + highScore.ToString();
			PlayerPrefs.SetInt("MediumHS", highScore);
		}
		else if(modeManager.gameMode == ModeManager.GameMode.Hard) {
			highScore = highScorePts;
			highScoreText.text = "High Score: " + highScore.ToString();
			PlayerPrefs.SetInt("HardlHS", highScore);
		}
		else if(modeManager.gameMode == ModeManager.GameMode.Special) {
			return;
		}
	}

	public void GameOver() {
		gameOverPanel.SetActive(true);
		colorPanel.SetActive(false);
		leftPanel.SetActive(false);
		rightPanel.SetActive(false);

		enableTimer = false;
		if(debug) {
			Debug.Log("Timer Stopped");
		}
		//Device Vibrate on Game Over...
		Handheld.Vibrate();
		Time.timeScale = 0.0f;
	}

	public void StartOver() {
		Time.timeScale = 1.0f;

		gameOverPanel.SetActive(false);
		colorPanel.SetActive(true);
		leftPanel.SetActive(true);
		rightPanel.SetActive(true);

		curScore = 0;
		curScoreText.text = "Score: " + curScore.ToString();
		if(modeManager.gameMode == ModeManager.GameMode.Normal) {
			highScore = PlayerPrefs.GetInt("NormalHS");
			timeLeft = 60;
			enableTimer = true;
		}
		else if(modeManager.gameMode == ModeManager.GameMode.Medium) {
			highScore = PlayerPrefs.GetInt("MediumHS");
			timeLeft = 60;
			enableTimer = true;
		}
		else if(modeManager.gameMode == ModeManager.GameMode.Hard) {
			highScore = PlayerPrefs.GetInt("HardHS");
			timeLeft = 120;
			enableTimer = true;
		}
		else {
			highScore = 0;
			timeLeft = 0;
			enableTimer = false;
		}

		wireManager.ResetAll();
	}

	public void Quit() {
		SceneManager.LoadScene("MainMenu");
	}



}
