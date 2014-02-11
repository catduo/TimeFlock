using UnityEngine;
using System.Collections.Generic;

public class GUIControls : MonoBehaviour {
	
	// Bounds of the screen where the birds can fly
	public const int BoundsMinX = -1;
	public const int BoundsMaxX = 24;
	public const int BoundsMinY = 1;
	public const int BoundsMaxY = 19;
	
	static public int NumBirdsUsed = 0;
	static public float distance = 0.0f;
	static private float bestDistance;
	public const float StartingPlayerEnergy = 2.5f;
	static public float PlayerEnergy = StartingPlayerEnergy;
	
	static public bool IsRewinding = false;
	static public bool IsSlowing = false;
	static public bool IsPaused = false;
	
	private bool is_ftue = true;
	private bool is_logo = true;
	public Texture2D logo;
	
	private GameObject menu;
	private GameObject mainCamera;
	private GameObject obstacles;
	private GameObject distanceText;
	private AudioSource audioSource;
	
	public Transform BirdPrefab;
	
	static public Vector2[] InputXYs = new Vector2[1];

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 60;
		bestDistance = PlayerPrefs.GetInt("bestDistance");
		mainCamera = GameObject.Find("MainCamera");
		menu = GameObject.Find("Menu");
		distanceText = GameObject.Find("Distance");
		audioSource = transform.GetComponent<AudioSource>();
		GameObject.Find ("BestDistance").GetComponent<TextMesh>().text = "Best Distance: " + bestDistance.ToString();
		GameObject.Find ("ThisDistance").GetComponent<TextMesh>().text = "This Round: " + distance.ToString();
		distanceText.GetComponent<TextMesh>().text = "Distance: " + distance.ToString();
		
		GameObject.Find ("CurrentBird").GetComponent<controls>().InitState(BirdState.PlayerControlled);
		
		PlayerEnergy = StartingPlayerEnergy; // 30s
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			GameObject.Find ("Menu").SendMessage("ToggleMenu");
		}
		
		GetTouch();
		GetMouse();
		
		if (!IsRewinding) {
			if (IsSlowing && PlayerEnergy > 0) {
				Time.timeScale = 0.35f;
				PlayerEnergy -= Time.deltaTime;
			}
			else if (Time.timeScale != 0) {
				Time.timeScale = 1.0f;
			}
		}
		
		UpdateDistance();
	}
	
	void FixedUpdate() {
		if (!IsPaused) {
			if (!IsRewinding) {
				distance += 0.1f;
			}
			else {
				distance -= 0.1f;
			}
		}
	}
	
	void GetTouch() {		//find everything that has a touch initiated on it and let it know
		if(Input.touchCount > 0){
			Touch touch = Input.GetTouch(0);
			InputXYs[touch.fingerId] = Camera.main.ScreenToWorldPoint(touch.position);
			if(touch.phase == TouchPhase.Began){
	            Ray ray = Camera.main.ScreenPointToRay(touch.position);
	            RaycastHit objectTouched ;
	            if (Physics.Raycast (ray, out objectTouched)) {
	                 objectTouched.transform.SendMessage("Tap", SendMessageOptions.DontRequireReceiver);
	            }
			}
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
			InputXYs = new Vector2[1];
			InputXYs[0] = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3 simTouch = Input.mousePosition;
	        Ray simRay = Camera.main.ScreenPointToRay(simTouch);
	        RaycastHit objectTouchedSim ;
	        if (Physics.Raycast (simRay, out objectTouchedSim)) {
	             objectTouchedSim.transform.SendMessage("Tap", SendMessageOptions.DontRequireReceiver);
	        }
		}
		if(Input.GetMouseButton(0)){
			InputXYs = new Vector2[1];
			InputXYs[0] = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3 simTouch = Input.mousePosition;
	        Ray simRay = Camera.main.ScreenPointToRay(simTouch);
	        RaycastHit objectTouchedSim ;
	        if (Physics.Raycast (simRay, out objectTouchedSim)) {
	             objectTouchedSim.transform.SendMessage("Hold", SendMessageOptions.DontRequireReceiver);
	        }
		}
		if(Input.GetMouseButtonUp(0)){
			InputXYs = new Vector2[1];
			InputXYs[0] = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3 simTouch = Input.mousePosition;
	        Ray simRay = Camera.main.ScreenPointToRay(simTouch);
	        RaycastHit objectTouchedSim ;
	        if (Physics.Raycast (simRay, out objectTouchedSim)) {
	             objectTouchedSim.transform.SendMessage("Release", SendMessageOptions.DontRequireReceiver);
	        }
		}
	}
	
	void GetKeys () {
		
	}
	
	//update the distance text
	void UpdateDistance () {
		distanceText.GetComponent<TextMesh>().text = "Distance: " + Mathf.RoundToInt (distance).ToString();
	}
	
	//when the game ends put up a menu that lets you restart
	static public void GameOver(){
		print ("Remaining energy " + PlayerEnergy);
		
		if(bestDistance < 10){
			bestDistance = distance;
		}
		else if(distance > bestDistance){
			bestDistance = distance;
		}
		PlayerPrefs.SetFloat("bestDistance", bestDistance);
		GameObject.Find ("BestDistance").GetComponent<TextMesh>().text = "Best Distance: " + Mathf.RoundToInt (bestDistance).ToString();
		GameObject.Find ("ThisDistance").GetComponent<TextMesh>().text = "This Round: " + Mathf.RoundToInt (distance).ToString();
		
		IsRewinding = true;
		GameObject.Find ("Rewinder").GetComponent<Rewinder>().StartRewind();
	}
	
	public void ResetLevel () {
		PlayerEnergy = StartingPlayerEnergy;
		Time.timeScale = 1.0f;
		IsRewinding = false;
		distance = 0;
		
		// Reset stuff
		/*GameObject.Find ("Close").SendMessage("Reset");
		GameObject.Find ("Far").SendMessage("Reset");
		GameObject.Find ("Mid").SendMessage("Reset");
		GameObject.Find ("Obstacles").SendMessage("Reset");*/
		
		foreach (var o in GameObject.FindObjectsOfType(typeof(Rewindable))) {
			((Rewindable)o).DoneRewinding();
			((Rewindable)o).ResetRewind();
		}
		
		// Reset all birds
		foreach (var bird in FindAllBirds()) {
			bird.GetComponent<controls>().InitState(BirdState.Replaying);
		}
		var player = GameObject.Find ("CurrentBird");
		player.name = "GhostBird" + NumBirdsUsed;
		player.GetComponent<controls>().MakeNonPlayer();
		NumBirdsUsed += 1;
		
		// Make a new player bird
		var newPlayer = (Transform)(Instantiate(BirdPrefab, Vector3.zero, Quaternion.identity));
		newPlayer.SendMessage("Start");
		newPlayer.GetComponent<controls>().InitState(BirdState.PlayerControlled);
		newPlayer.name = "CurrentBird";
		newPlayer.transform.parent = GameObject.Find ("World").transform;
	}
	
	public void RestartGame () {
		ResetLevel ();
		IsPaused = true;
		Time.timeScale = 0;
		GameObject otherBirds = GameObject.Find ("OtherBirds");
		for(int childBirds = 0; childBirds < otherBirds.transform.childCount; childBirds++){
			Destroy(otherBirds.transform.GetChild(childBirds).gameObject);
		}
		NumBirdsUsed = 0;
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

	int ftuelocation = 0;
	void OnGUI(){
		GUI.skin.box.fontSize = 50;
		GUI.skin.box.wordWrap = true;
		if(is_logo){
			IsPaused = true;
			Time.timeScale = 0;
			GUI.DrawTexture(new Rect(Screen.width/2 - 320, 20, 640, 380), logo);
			if(GUI.Button(new Rect(Screen.width/2 - 320, 420, 640, 30), "Continue")){
				is_logo = false;
			}
		}
		else if(is_ftue){
			switch(ftuelocation){
			case 0:
				GUI.Box(new Rect(Screen.width/2 - 320, 20, 640, 380), "Welcome to TimeFlock");
				if(GUI.Button(new Rect(Screen.width/2 - 320, 420, 640, 30), "Next")){
					ftuelocation++;
				}
				break;
				
			case 1:
				GUI.Box(new Rect(Screen.width/2 - 320, 20, 640, 380), "You are playing as a robin that swallowed a time control device and is trying to escape an airforce base");
				if(GUI.Button(new Rect(Screen.width/2 - 320, 420, 640, 30), "Next")){
					ftuelocation++;
				}
				break;
				
			case 2:
				GUI.Box(new Rect(Screen.width/2 - 320, 20, 640, 380), "When you touch anything you will trigger the time control device, causing you to go back in time to the point when you ate it");
				if(GUI.Button(new Rect(Screen.width/2 - 320, 420, 640, 30), "Next")){
					ftuelocation++;
				}
				break;
				
			case 3:
				GUI.Box(new Rect(Screen.width/2 - 320, 20, 640, 380), "To control the bird use the left side of the screen as a directional pad; touch to engage and move your finger to move the bird");
				if(GUI.Button(new Rect(Screen.width/2 - 320, 420, 640, 30), "Next")){
					ftuelocation++;
				}
				break;
				
			case 4:
				GUI.Box(new Rect(Screen.width/2 - 320, 20, 640, 380), "Touching the right side of the screen will cause the bird to use the device to slow time");
				if(GUI.Button(new Rect(Screen.width/2 - 320, 420, 640, 30), "Next")){
					ftuelocation++;
				}
				break;
				
			case 5:
				GUI.Box(new Rect(Screen.width/2 - 320, 20, 640, 380), "If you are close to one of your previous lives' explosions you can pick up some of the time power and replenish the meter");
				if(GUI.Button(new Rect(Screen.width/2 - 320, 420, 640, 30), "Next")){
					ftuelocation++;
				}
				break;
				
			default:
				GUI.Box(new Rect(Screen.width/2 - 320, 20, 640, 380), "Attempt to make it out of the airforce base, it should take a little over a minute on a perfect run");
				if(GUI.Button(new Rect(Screen.width/2 - 320, 420, 640, 30), "Start!")){
					is_ftue = false;
					IsPaused = false;
					Time.timeScale = 1;
				}
				break;
			}
		}
		else{
			if(GUI.Button(new Rect(0, 0, 150, 50), "Menu")){
				GameObject.Find ("Menu").SendMessage("ToggleMenu");
			}
		}
	}
}
