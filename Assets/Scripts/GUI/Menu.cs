using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	
	private TextMesh thisDistance;
	private TextMesh numLives;
	public bool menuIsOn = false;
	
	// Use this for initialization
	void Start () {
		MenuOff ();
		thisDistance = GameObject.Find ("ThisDistance").GetComponent<TextMesh>();
		numLives = GameObject.Find ("NumLives").GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void ToggleMenu() {
		if (menuIsOn) {
			MenuOff();
		} else {
			MenuOn();
		}
	}
	
	void MenuOn() {
		menuIsOn = true;
		Time.timeScale = 0;
		numLives.text = "Flock Size: " + (GUIControls.NumBirdsUsed + 1).ToString();
		thisDistance.text = "This Round: " + Mathf.RoundToInt(GUIControls.distance).ToString();
		if(transform.position.y != 10){
			transform.position = new Vector3(13, 10, -9);
		}
	}
	
	void MenuOff() {
		menuIsOn = false;
		Time.timeScale = 1;
		if(transform.position.y != 100){
			transform.position = new Vector3(13, 100, -9);
		}
	}
}
