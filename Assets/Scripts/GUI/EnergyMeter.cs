using UnityEngine;
using System.Collections;

public class EnergyMeter : MonoBehaviour {
	
	private float width;
	private float xpos;
	
	// Use this for initialization
	void Start () {
		xpos = 0;
		width = 0.9F;
	}
	
	// Update is called once per frame
	void Update () {
		width = 0.9F * (100 - GUIControls.distance);
		xpos = width - 0.9F;
	}
	
	void Hold(){
		//slow time
	}
}
