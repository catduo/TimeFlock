using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {
	
	public float movementSpeed;
	private float distanceTravelled;
	private float rewindSpeed = 20;
	private int rewindFrames = 0;
	
	// Use this for initialization
	void Start () {
		distanceTravelled = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate(new Vector3(-movementSpeed, 0, 0));
		distanceTravelled += movementSpeed;
	}
	
	void Reset () {
		while(){
			transform.Translate(new Vector3(movementSpeed * rewindSpeed, 0, 0));
			rewindFrames++;
		}
	}
}
