using UnityEngine;
using System.Collections;

public class DPadDown : MonoBehaviour {
	
	public Transform bird;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Hold () {
		DPad.VAxis = -1;
	}
}
