using UnityEngine;
using System.Collections;

public class EnergyMeter : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = new Vector3(0.9F * GUIControls.PlayerEnergy/GUIControls.StartingPlayerEnergy, 1, 0.9F);
		transform.localPosition = new Vector3((0.9F - transform.localScale.x) * 5, 0, 0);
	}
	
	void Hold(){
		//slow time
	}
}
