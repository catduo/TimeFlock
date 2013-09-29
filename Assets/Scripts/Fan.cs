using UnityEngine;
using System.Collections;

public class Fan : MonoBehaviour {
	
	public bool clockwise;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(clockwise){
			transform.Rotate(new Vector3(0,0,-1));
		}
		else{
			transform.Rotate(new Vector3(0,0,1));
		}
	}
}
