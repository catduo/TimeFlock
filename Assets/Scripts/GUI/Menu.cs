using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		MenuOff ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void MenuOn() {
		Time.timeScale = 0;
		GameObject.Find ("ThisDistance").GetComponent<TextMesh>().text = "This Round: " + GUIControls.distance.ToString();
		if(transform.position.y != 10){
			transform.position = new Vector3(13, 10, -9);
		}
	}
	
	void MenuOff() {
		Time.timeScale = 1;
		if(transform.position.y != 100){
			transform.position = new Vector3(13, 100, -9);
		}
	}
}
