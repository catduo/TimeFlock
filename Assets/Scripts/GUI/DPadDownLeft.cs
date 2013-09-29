using UnityEngine;
using System.Collections;

public class DPadDownLeft : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Hold () {
		DPad.HAxis = -1;
		DPad.VAxis = -1;
	}
	
	void Release () {
		DPad.HAxis = 0;
		DPad.VAxis = -0;
	}
}
