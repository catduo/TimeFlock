using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {
	
	public float movementSpeed;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate(new Vector3(-movementSpeed, 0, 0));
	}
	
	void Tap () {
		Debug.Log ("background tapped");
	}
}
