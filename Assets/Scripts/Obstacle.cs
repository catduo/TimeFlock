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
		var currState = GetComponent<controls>().CurrState;
		if(other.name == "FlockCapacitor(Clone)"){
			if (currState == BirdState.PlayerControlled){
				float obstacleState = other.GetComponent<Capacitor>().state;
				float forceX = transform.position.x - other.transform.position.x;
				float forceY = transform.position.y - other.transform.position.y;
				Vector3 forceVector = new Vector3(forceX, forceY, 0);
				
				if (!other.GetComponent<Capacitor>().stateBackwards){
					gameObject.rigidbody.AddForce(150f*forceVector.normalized);
				}
			}
		}
		else {
			if (GetComponent<controls>().Rewinding) return;
			if (currState == BirdState.PlayerControlled) {
				// Player controlled bird hit an obstacle
				GetComponent<controls>().OnDeath();
				GUIControls.GameOver();
			}
			else if (currState == BirdState.Replaying) {
				GetComponent<controls>().InitState(BirdState.Dead);
			}
		}
	}
}
