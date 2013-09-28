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
			Time.timeScale = (Time.realtimeSinceStartup - rewindStartTime) * 2.0f;
			if (Time.timeScale > 4.0f) {
				Time.timeScale = 4.0f;
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
	}
}
