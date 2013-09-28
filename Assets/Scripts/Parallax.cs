using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {
	
	public float movementSpeed;
	private float distanceTravelled;
	
	// Use this for initialization
	void Start () {
		distanceTravelled = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate(new Vector3(-movementSpeed, 0, 0));
		distanceTravelled += movementSpeed;
	}
}
