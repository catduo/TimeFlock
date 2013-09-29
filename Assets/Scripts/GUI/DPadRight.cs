using UnityEngine;
using System.Collections;

public class DPadRight : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Hold () {
		DPad.HAxis = 1;
	}
	
	void Release () {
		DPad.HAxis = 0;
	}
}
