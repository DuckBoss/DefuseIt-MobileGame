using UnityEngine;
using System.Collections;

public class FilterHandler : MonoBehaviour {

	public int chosenFilter;
	public ColorBlindFilter cbfilter;


	// Use this for initialization
	void Start () {
		chosenFilter = PlayerPrefs.GetInt("FilterInt");
		cbfilter.mode = (ColorBlindMode)chosenFilter;
	}
		
}
