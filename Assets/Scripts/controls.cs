using UnityEngine;
using System.Collections;

public class controls : MonoBehaviour {
	
	public int force = 20;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetKeys ();
	}
	
	void GetKeys () {
		if (Input.GetKey (KeyCode.W)){
			controlUp();
		}
		if (Input.GetKey (KeyCode.S)){
			controlDown();
		}
		if (Input.GetKey (KeyCode.A)){
			controlLeft();
		}
		if (Input.GetKey (KeyCode.D)){
			controlRight();
		}
	}
	
	//press up, keys or touch controls
	void controlUp(){
		rigidbody.AddForce(Vector3.up * force);
	}
	
	//press down, keys or touch controls
	void controlDown(){
		rigidbody.AddForce(Vector3.down * force);
	}
	
	//press left, keys or touch controls
	void controlLeft(){
		rigidbody.AddForce(Vector3.left * force);
	}
	
	//press right, keys or touch controls
	void controlRight(){
		rigidbody.AddForce(Vector3.right * force);
	}
}
