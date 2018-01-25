using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class WireHandler : MonoBehaviour {

	public WireManager wireManager;

	// Use this for initialization
	void Start () {
		if(wireManager == null) {
			wireManager = GameObject.Find("WireManager").GetComponent<WireManager>();
		}
	}
	
	public void MouseUp() {
		//if(!EventSystem.current.IsPointerOverGameObject()) {
			wireManager.ChosenWire(this.gameObject);
		//}
	}

}
