using UnityEngine;
using System.Collections;

public class DPadUpLeft : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		DPad.HAxis = 0;
		DPad.VAxis = 0;
	}
	
	void Hold () {
		DPad.HAxis = -1;
		DPad.VAxis = 1;
	}
	
	void Release () {
		DPad.HAxis = 0;
		DPad.VAxis = 0;
	}
}
