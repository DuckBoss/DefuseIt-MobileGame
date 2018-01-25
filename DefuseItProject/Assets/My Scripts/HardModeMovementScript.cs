using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HardModeMovementScript : MonoBehaviour {

	public List<GameObject> hardWires;
	public ModeManager modeManager;

	public List<Animator> animators;

	void Start() {
		if(modeManager.gameMode == ModeManager.GameMode.Hard) {
			MoveWires();
		}
		else {
			return;
		}
	}

	public void MoveWires() {
		foreach(GameObject wire in hardWires) {
			Animator tempAnim = wire.GetComponent<Animator>();
			animators.Add(tempAnim);
		}
	}

}
