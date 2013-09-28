using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {
	
	public float movementSpeed;
	public float distanceTravelled;
	private float rewindSpeed = 5;
	private int rewindFrames = 0;
	
	private Vector3 initPosition;
	
	// Use this for initialization
	void Start () {
		distanceTravelled = 0;
		initPosition = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float movement = movementSpeed * (1.0f + (GUIControls.distance / 500.0f));
		
		if (!GUIControls.IsRewinding) {
			transform.Translate(new Vector3(-movement, 0, 0));
			distanceTravelled += movementSpeed;
		}
		else {
			transform.Translate(new Vector3(movement, 0, 0));
		}
	}
	
	void Reset () {
		/*while((rewindFrames + 2) * rewindSpeed * movementSpeed < distanceTravelled){
			transform.Translate(new Vector3(movementSpeed * rewindSpeed, 0, 0));
			rewindFrames++;
		}
		transform.Translate(new Vector3(distanceTravelled - (rewindFrames + 2) * rewindSpeed * movementSpeed, 0, 0));*/
		transform.position = initPosition;
	}
}
