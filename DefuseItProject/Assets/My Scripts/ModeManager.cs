using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ModeManager : MonoBehaviour {

	public ScoreManager scoreManager;
	public WireManager wireManager;

	public Transform wireHolder_HardMode;
	public Transform wireHolder_DefaultMode;

	public Camera mainCamera;

	public enum GameMode {
		Practice,
		Normal,
		Medium,
		Hard,
		Special
	}

	public GameMode gameMode;

	public Text curScoreText_Portrait;
	public Text highScoreText_Portrait;
	public Text TimeLeftText_Portrait;
	public Text modeText_Portrait;
	public Text curScoreText_Landscape;
	public Text highScoreText_Landscape;
	public Text TimeLeftText_Landscape;
	public Text modeText_Landscape;
	public GameObject GameOver_Portrait;
	public GameObject GameOver_Landscape;
	public GameObject PanelLeft_Portrait;
	public GameObject PanelRight_Portrait;
	public GameObject PanelLeft_Landscape;
	public GameObject PanelRight_Landscape;
	public GameObject ColorPlane_Portrait;
	public GameObject ColorPlane_Landscape;

	private void AdjustScreenOrientation() {
		int orientationInt = PlayerPrefs.GetInt("ScreenOrientation");
		//Debug.Log(orientationInt);
		switch(orientationInt) {
			case(0):
				Screen.orientation = ScreenOrientation.Portrait;
				mainCamera.orthographicSize = 10.7f;
				mainCamera.transform.rotation = Quaternion.Euler(0f,0f,90f);
				wireManager.correctColorPanel = ColorPlane_Portrait.GetComponent<Renderer>();
				scoreManager.colorPanel = ColorPlane_Portrait;
				scoreManager.leftPanel = PanelLeft_Portrait;
				scoreManager.rightPanel = PanelRight_Portrait;
				scoreManager.gameOverPanel = GameOver_Portrait;

				scoreManager.curScoreText = curScoreText_Portrait;
				scoreManager.highScoreText = highScoreText_Portrait;
				scoreManager.ModeText = modeText_Portrait;
				scoreManager.timeLeftText = TimeLeftText_Portrait;
				break;
			case(1):
				Screen.orientation = ScreenOrientation.Landscape;
				mainCamera.orthographicSize = 6.4f;
				mainCamera.transform.rotation = Quaternion.Euler(0f,0f,0f);
				wireManager.correctColorPanel = ColorPlane_Landscape.GetComponent<Renderer>();
				scoreManager.colorPanel = ColorPlane_Landscape;
				scoreManager.leftPanel = PanelLeft_Landscape;
				scoreManager.rightPanel = PanelRight_Landscape;
				scoreManager.gameOverPanel = GameOver_Landscape;

				scoreManager.curScoreText = curScoreText_Landscape;
				scoreManager.highScoreText = highScoreText_Landscape;
				scoreManager.ModeText = modeText_Landscape;
				scoreManager.timeLeftText = TimeLeftText_Landscape;
				break;

		}
	}

	private void Start() {
		//FRAME LOCK @ 60
		#if UNITY_EDITOR
		Application.targetFrameRate = 999;
		#else
		Application.targetFrameRate = 60;
		#endif

		AdjustScreenOrientation();

		if(scoreManager == null) {
			scoreManager = GetComponent<ScoreManager>();
		}
		if(wireManager == null) {
			wireManager = GetComponent<WireManager>();
		}
		int gameModeInt = PlayerPrefs.GetInt("GameMode");
		gameMode = (GameMode)gameModeInt;


		if(gameMode == GameMode.Hard) {
			wireManager.AllWires.Clear();
			foreach(Transform childWire in wireHolder_HardMode.transform) {
				childWire.gameObject.SetActive(true);
				childWire.transform.GetComponent<Animator>().speed = 1.0f;
				wireManager.AllWires.Add(childWire.gameObject);
			}
		}
		else if(gameMode == GameMode.Medium) {
			wireManager.AllWires.Clear();
			foreach(Transform childWire in wireHolder_HardMode.transform) {
				childWire.gameObject.SetActive(true);
				childWire.transform.GetComponent<Animator>().speed = 0.5f;
				wireManager.AllWires.Add(childWire.gameObject);
			}
		}
		else {
			wireManager.AllWires.Clear();
			foreach(Transform childWire in wireHolder_DefaultMode.transform) {
				childWire.gameObject.SetActive(true);
				wireManager.AllWires.Add(childWire.gameObject);
			}
		}

		scoreManager.InitialSetupUI();
	}

}
