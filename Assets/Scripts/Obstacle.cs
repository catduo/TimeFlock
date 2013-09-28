using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		if(other.name == "FlockZone(Clone)"){
			
		}
		else {
			if (GetComponent<controls>().CurrState == BirdState.PlayerControlled) {
				// Only end if the collider is player controlled
				GUIControls.GameOver();
			}
			else {
				GetComponent<controls>().InitState(BirdState.Dead);
			}
		}
	}
}
