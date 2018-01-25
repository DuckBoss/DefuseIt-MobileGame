using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/**
	Jason Jerome 2017

	This class handles the Menu user interface.
 */

public class MenuManager : MonoBehaviour {	

	// Use this for initialization
	void Start () {
		//Reset timescale.
		Time.timeScale = 1.0f;

		//Locked Framerate @ 60fps ON ANDROID
		//Unlocked Framerate in EDITOR
		#if UNITY_EDITOR
		Application.targetFrameRate = 999;
		#else
		Application.targetFrameRate = 60;
		#endif

		//Reset screen orientation if previously modified.
		Screen.orientation = ScreenOrientation.Portrait;

		//Checks if it is players first time playing.
		int firstTime = 0;
		firstTime = PlayerPrefs.GetInt("FirstTimePlaying");
		if(firstTime == 0) {
			PortraitToggle.isOn = false;
			LandscapeToggle.isOn = true;
			PlayerPrefs.SetInt("FirstTimePlaying", 2);
		}
		else {
			if(firstTime == 1) {
				PortraitToggle.isOn = true;
				LandscapeToggle.isOn = false;
			}
			else if(firstTime == 2) {
				LandscapeToggle.isOn = true;
				PortraitToggle.isOn = false;
			}
		}

		ShowMenu();
	}

	/** This was a temporary fix.
	private void Update() {
		Screen.orientation = ScreenOrientation.Portrait;
	}
	*/

	[Header("UI Object References")]
	public GameObject MenuPanel;
	public GameObject ModePanel;
	public GameObject SettingsPanel;
	public GameObject InstructionsPanel;
	public GameObject CreditsPanel;

	public Toggle PortraitToggle;
	public Toggle LandscapeToggle;

	public Animator settingsAnimator;
	public Animator menuAnimator;
	public Animator modeAnimator;
	public Animator instructionsAnimator;
	public Animator creditsAnimator;

	//Shows Settings UI
	public void ShowSettings() {
		menuAnimator.SetBool("MenuActive", false);
		settingsAnimator.SetBool("SettingsActive", true);
		modeAnimator.SetBool("ModeActive", false);
		creditsAnimator.SetBool("CreditsActive", false);
	}
	//Shows Mode UI
	public void ShowMode() {
		menuAnimator.SetBool("MenuActive", false);
		settingsAnimator.SetBool("SettingsActive", false);
		modeAnimator.SetBool("ModeActive", true);
		creditsAnimator.SetBool("CreditsActive", false);
		instructionsAnimator.SetBool("InstructionActive", false);
	}
	//Shows Menu UI
	public void ShowMenu() {
		menuAnimator.SetBool("MenuActive", true);
		settingsAnimator.SetBool("SettingsActive", false);
		modeAnimator.SetBool("ModeActive", false);
		creditsAnimator.SetBool("CreditsActive", false);
	}
	//Shows Credits UI
	public void ShowCredits() {
		creditsAnimator.SetBool("CreditsActive", true);
		settingsAnimator.SetBool("SettingsActive", false);
		modeAnimator.SetBool("ModeActive", false);
		menuAnimator.SetBool("MenuActive", false);
		instructionsAnimator.SetBool("InstructionActive", false);
	}
	//Shows Instructions UI
	public void ShowInstructions() {
		instructionsAnimator.SetBool("InstructionActive", true);
	}
	//Closes Instructions UI
	public void CloseInstructions() {
		instructionsAnimator.SetBool("InstructionActive", false);
	}
	//Sets the players chosen settings and loads the main game scene.
	public void StartGame() {
		if(LandscapeToggle.isOn) {
			PlayerPrefs.SetInt("FirstTimePlaying", 2);
			PlayerPrefs.SetInt("ScreenOrientation", 1);
		}
		else if(PortraitToggle.isOn) {
			PlayerPrefs.SetInt("FirstTimePlaying", 1);
			PlayerPrefs.SetInt("ScreenOrientation", 0);
		}
		SceneManager.LoadScene("MainScene");
	}
	//Quits the game.
	public void QuitGame() {
		Application.Quit();
	}
	//Sets game mode to practice
	public void SelectMode_Practice() {
		PlayerPrefs.SetInt("GameMode", 0);
		StartGame();
	}
	//Sets the game mode to normal
	public void SelectMode_Normal() {
		PlayerPrefs.SetInt("GameMode", 1);
		StartGame();
	}
	//Sets the game mode to medium
	public void SelectMode_Medium() {
		PlayerPrefs.SetInt("GameMode", 2);
		StartGame();
	}
	//Sets the game mode to hard
	public void SelectMode_Hard() {
		PlayerPrefs.SetInt("GameMode", 3);
		StartGame();
	}
	//Sets the game mode to special (unimplemented)
	public void SelectMode_Special() {
		PlayerPrefs.SetInt("GameMode", 4);
		StartGame();
	}
}
