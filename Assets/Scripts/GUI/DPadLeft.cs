using UnityEngine;
using System.Collections;

public class DPadLeft : MonoBehaviour {
	
	public Transform bird;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Hold () {
		DPad.HAxis = -1;
	}
}
