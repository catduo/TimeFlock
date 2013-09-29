using UnityEngine;
using System.Collections;

public class SlowPad : MonoBehaviour {
	public GameObject bird;
	BirdInputState bis;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Hold () {
		bis.SlowDownPressed = true;
		bird.GetComponent<controls>().ApplyInputs(bis);
	}
	
	void Release () {
		bis.SlowDownPressed = false;
		bird.GetComponent<controls>().ApplyInputs(bis);
	}
}
