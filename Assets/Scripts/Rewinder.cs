using UnityEngine;
using System.Collections;

public class Rewinder : MonoBehaviour {
	
	bool rewinding;
	float rewindStartTime;

	// Use this for initialization
	void Start () {
		rewinding = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (rewinding) {
			var timeSinceRewind = (Time.realtimeSinceStartup - rewindStartTime);
			var easeScale = timeSinceRewind;
			Time.timeScale = easeScale * 10.0f;
			if (Time.timeScale > 40.0f) {
				Time.timeScale = 40.0f;
			}
			
			if (GUIControls.distance <= 0) {
				// Done rewinding
				rewinding = false;
				GameObject.Find ("GUI").GetComponent<GUIControls>().ResetLevel();
				GameObject.Find ("GUI").GetComponent<GUIControls>().WaitForStart();
			}
		}
	}
	
	public void StartRewind() {
		rewindStartTime = Time.realtimeSinceStartup;
		rewinding = true;
		Time.timeScale = 0.0f;
		
		// Rewind all birds
		foreach (var bird in GUIControls.FindAllBirds()) {
			bird.GetComponent<controls>().StartRewind();
		}
	}
}
