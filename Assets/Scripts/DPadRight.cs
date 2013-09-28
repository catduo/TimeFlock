using UnityEngine;
using System.Collections;

public class DPadRight : MonoBehaviour {
	
	public Transform bird;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Hold () {
		bird.SendMessage("controlRight");
	}
}
