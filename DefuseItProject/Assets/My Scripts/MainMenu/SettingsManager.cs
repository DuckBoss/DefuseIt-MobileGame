using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {

	public Dropdown filterDropDown;

	// Use this for initialization
	void Start () {
		filterDropDown.onValueChanged.AddListener(delegate {
			filterDropDownValueChangedHandler(filterDropDown);
		});
		SetDropDownIndex();
	}

	private void filterDropDownValueChangedHandler(Dropdown target) {
		Debug.Log("Selected: " + target.value);
		PlayerPrefs.SetInt("FilterInt", target.value);
	}

	public void Destroy() {
		filterDropDown.onValueChanged.RemoveAllListeners();
	}

	public void SetDropDownIndex() {
		filterDropDown.value = PlayerPrefs.GetInt("FilterInt");            
	}
	

}
