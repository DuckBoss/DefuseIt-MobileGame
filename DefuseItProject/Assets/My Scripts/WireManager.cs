using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using WireClassSpace;
using FDL.Library.Numeric;

/**
	Jason Jerome 2017
	
	This class handles the setting up of the 
	individual wires, assigning colors, and assigning the correct wire to cut.
 */


[RequireComponent(typeof(ScoreManager))]
public class WireManager : MonoBehaviour {

	[Header("Manager References")]
	public ScoreManager scoreManager;
	public ModeManager modeManager;

	[Header("Game Object References")]
	public List<GameObject> AllWires;
	public List<WireProperties> ReColoredWires;
	public Renderer correctColorPanel;

	private bool _debug;

	// Use this for initialization
	void Start () {
		// Standard null checks
		if(scoreManager == null) {
			scoreManager = GetComponent<ScoreManager>();
		}
		if(modeManager == null) {
			modeManager = GetComponent<ModeManager>();
		}
		if(_debug) {
			Debug.Log(AllWires.Count + " Wires Detected!");
		}
	}

	public void Setup() {
		//Initialize by clearing all preconfigured wires.
		ReColoredWires.Clear();

		//Generate new wire properties for each wire in the list.
		int wireIDCounter = 1;
		foreach(GameObject wire in AllWires) {
			WireProperties wireProp = new WireProperties();
			wireProp.wireName = wire.name;
			wireProp.wireOBJ = wire;
			wireProp.wireColor = new Color(CryptoRandom.Between(1,255)/255f, CryptoRandom.Between(1,255)/255f, CryptoRandom.Between(1,255)/255f, 1.0f);
			wireProp.wireID = wireIDCounter;
			wireProp.correctWire = false;
			if(_debug) {
				Debug.Log("WireName: " + wireProp.wireName + "\n" + "WireColor: " + wireProp.wireColor + "\n" + "WireID: " + wireProp.wireID);
			}
			ReColoredWires.Add(wireProp);
			 
			wireIDCounter++;
		}
		//Assign colors to the wires generated.
		AssignColors();
	}

	// This method assigns colors to the individual wires.
	private void AssignColors() {
		//Iterates through the wires and assigns the color from its properties.
		foreach(WireProperties wireProp in ReColoredWires) {
			wireProp.wireOBJ.GetComponent<Renderer>().material.color = wireProp.wireColor;
			//Adds an additional outline color to the shader if the player is in Practice Mode.
			if(modeManager.gameMode == ModeManager.GameMode.Practice) {
				wireProp.wireOBJ.GetComponent<Renderer>().material.SetFloat("_Outline", 0.0f);
			}
		}
		//Picks a wire to be the correct wire to cut.
		PickCorrectWire();
	}

	// This method picks a wire from the list to be the correct wire to cut.
	private void PickCorrectWire() {
		// Uses a random number generator to pick a wire by ID (integer).
		int chosenWireNum = CryptoRandom.Between(1, AllWires.Count);
		foreach(WireProperties wireProp in ReColoredWires) {
			if(wireProp.wireID == chosenWireNum) {
				wireProp.correctWire = true;

				if(modeManager.gameMode == ModeManager.GameMode.Practice) {
					wireProp.wireOBJ.GetComponent<Renderer>().material.SetFloat("_Outline", 0.1f);
				}
				correctColorPanel.material.color = wireProp.wireColor;
				break;
			}
			else {
				wireProp.correctWire = false;
			}
		}
	}

	//Resets all the wires by re-running the Setup method.
	public void ResetAll() {
		Setup();
	}

	// Handles the players wire choice in runtime and adjusts the players score.
	public void ChosenWire(GameObject wire) {
		for(int i = 0; i < ReColoredWires.Count; i++) {
			if(ReColoredWires[i].correctWire == true) {
				if(ReColoredWires[i].wireOBJ == wire) {
					if(modeManager.gameMode != ModeManager.GameMode.Special || modeManager.gameMode != ModeManager.GameMode.Practice) {
						scoreManager.AdjustCurPoints(1);
					}
					ResetAll();
				}
			}
			// If the player chooses the wrong wire then set the new high score and GAME OVER
			else {
				if(ReColoredWires[i].wireOBJ == wire) {
					if(scoreManager.curScore > scoreManager.highScore) {
						scoreManager.ChangeHighScore(scoreManager.curScore);
					}
					GAMEOVER();
				}
			}
		}
	}

	//Initializes the game over sequence.
	public void GAMEOVER() {
		scoreManager.GameOver();
	}

}
