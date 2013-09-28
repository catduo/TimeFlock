using UnityEngine;
using System.Collections;

public class GUIControls : MonoBehaviour {
	
	private GameObject menu;
	private GameObject mainCamera;
	private GameObject obstacles;
	private GameObject distanceText;
	private AudioSource audioSource;
	static public int distance;
	private int bestDistance;
	static public bool gameOver = false;

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
		if(gameOver){
			GameOver();
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
	void GameOver(){
		if(bestDistance < 10){
			bestDistance = distance;
		}
		else if(distance > bestDistance){
			bestDistance = distance;
		}
		PlayerPrefs.SetInt("bestDistance", bestDistance);
		GameObject.Find ("BestDistance").GetComponent<TextMesh>().text = "Best Distance: " + bestDistance.ToString();
		GameObject.Find ("ThisDistance").GetComponent<TextMesh>().text = "This Round: " + distance.ToString();
		menu.SendMessage("MenuOn");
	}
	
	void Reset () {
		gameOver = false;
	}
}
