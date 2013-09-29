using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour {

	private GameObject menu;
	
	// Use this for initialization
	void Start () {
		menu = GameObject.Find ("Menu");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Tap () {
		menu.SendMessage("ToggleMenu");
	}
}
