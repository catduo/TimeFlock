﻿using UnityEngine;
using System.Collections.Generic;

public class GUIControls : MonoBehaviour {
	
	private GameObject menu;
	private GameObject mainCamera;
	private GameObject obstacles;
	private GameObject distanceText;
	private AudioSource audioSource;
	
	public Transform BirdPrefab;
	
	static public int NumBirdsUsed = 0;
	static public float distance = 0.0f;
	static private float bestDistance;
	
	static public bool IsRewinding = false;

	// Use this for initialization
	void Start () {
		bestDistance = PlayerPrefs.GetInt("bestDistance");
		mainCamera = GameObject.Find("MainCamera");
		menu = GameObject.Find("Menu");
		distanceText = GameObject.Find("Distance");
		audioSource = transform.GetComponent<AudioSource>();
		GameObject.Find ("BestDistance").GetComponent<TextMesh>().text = "Best Distance: " + bestDistance.ToString();
		GameObject.Find ("ThisDistance").GetComponent<TextMesh>().text = "This Round: " + distance.ToString();
		distanceText.GetComponent<TextMesh>().text = "Distance: " + distance.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		GetTouch();
		GetMouse();
		UpdateDistance();
	}
	
	void FixedUpdate() {
		if (!IsRewinding) {
			distance += 0.1f;
		}
		else {
			distance -= 0.1f;
		}
	}
	
	void GetTouch() {		//find everything that has a touch initiated on it and let it know
		if(Input.touchCount > 0){
			Touch touch = Input.GetTouch(0);
			if(touch.phase == TouchPhase.Began){
	            Ray ray = Camera.main.ScreenPointToRay(touch.position);
	            RaycastHit objectTouched ;
	            if (Physics.Raycast (ray, out objectTouched)) {
	                 objectTouched.transform.SendMessage("Tap", SendMessageOptions.DontRequireReceiver);
	            }
			}
		}		
		if(Input.touchCount > 0){
			Touch touch = Input.GetTouch(0);
			if(touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved){
	            Ray ray = Camera.main.ScreenPointToRay(touch.position);
	            RaycastHit objectTouched ;
	            if (Physics.Raycast (ray, out objectTouched)) {
	                 objectTouched.transform.SendMessage("Hold", SendMessageOptions.DontRequireReceiver);
	            }
			}
		}
	}
	
	void GetMouse() {		//find everything that has the mouse click on it and let it know
		if(Input.GetMouseButtonDown(0)){
			Vector3 simTouch = Input.mousePosition;
	        Ray simRay = Camera.main.ScreenPointToRay(simTouch);
	        RaycastHit objectTouchedSim ;
	        if (Physics.Raycast (simRay, out objectTouchedSim)) {
	             objectTouchedSim.transform.SendMessage("Tap", SendMessageOptions.DontRequireReceiver);
	        }
		}
		if(Input.GetMouseButton(0)){
			Vector3 simTouch = Input.mousePosition;
	        Ray simRay = Camera.main.ScreenPointToRay(simTouch);
	        RaycastHit objectTouchedSim ;
	        if (Physics.Raycast (simRay, out objectTouchedSim)) {
	             objectTouchedSim.transform.SendMessage("Hold", SendMessageOptions.DontRequireReceiver);
	        }
		}
	}
	
	void GetKeys () {
		
	}
	
	//update the distance text
	void UpdateDistance () {
		distanceText.GetComponent<TextMesh>().text = "Distance: " + distance.ToString();
	}
	
	//when the game ends put up a menu that lets you restart
	static public void GameOver(){
		if(bestDistance < 10){
			bestDistance = distance;
		}
		else if(distance > bestDistance){
			bestDistance = distance;
		}
		PlayerPrefs.SetFloat("bestDistance", bestDistance);
		GameObject.Find ("BestDistance").GetComponent<TextMesh>().text = "Best Distance: " + bestDistance.ToString();
		GameObject.Find ("ThisDistance").GetComponent<TextMesh>().text = "This Round: " + distance.ToString();
		
		IsRewinding = true;
		GameObject.Find ("Rewinder").GetComponent<Rewinder>().StartRewind();
	}
	
	public void ResetLevel () {
		Time.timeScale = 1.0f;
		IsRewinding = false;
		GameObject.Find ("Close").SendMessage("Reset");
		GameObject.Find ("Far").SendMessage("Reset");
		GameObject.Find ("Mid").SendMessage("Reset");
		GameObject.Find ("Obstacles").SendMessage("Reset");
		
		// Reset all birds
		foreach (var bird in FindAllBirds()) {
			bird.GetComponent<controls>().InitState(BirdState.Replaying);
		}
		var player = GameObject.Find ("CurrentBird");
		player.name = "GhostBird" + NumBirdsUsed;
		player.transform.parent = GameObject.Find ("OtherBirds").transform;
		NumBirdsUsed += 1;
		
		// Make a new player bird
		var newPlayer = (Transform)(Instantiate(BirdPrefab, Vector3.zero, Quaternion.identity));
		newPlayer.GetComponent<controls>().InitState(BirdState.PlayerControlled);
		newPlayer.name = "CurrentBird";
		newPlayer.transform.parent = GameObject.Find ("World").transform;
	}
	
	public void WaitForStart() {
		// TODO
		//menu.SendMessage("MenuOn");
	}
			
	public static IList<GameObject> FindAllBirds() {
		var ret = new List<GameObject>();
		ret.Add (GameObject.Find ("CurrentBird"));
		foreach (Transform t in GameObject.Find ("OtherBirds").transform) {
			ret.Add (t.gameObject);
		}
		return ret;
	}
}
