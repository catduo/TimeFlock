using UnityEngine;
using System.Collections;

public class DPadDown : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Hold () {
		DPad.VAxis = -1;
	}
	
	void Release () {
		DPad.VAxis = 0;
	}
}
