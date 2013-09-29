using UnityEngine;
using System.Collections;

public class DPadDown : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		DPad.HAxis = 0;
		DPad.VAxis = 0;
	}
	
	void Hold () {
		DPad.VAxis = -1;
		DPad.HAxis = 0;
	}
	
	void Release () {
		DPad.HAxis = 0;
		DPad.VAxis = 0;
	}
}
