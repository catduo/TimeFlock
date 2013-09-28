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
			if (name == "CurrentBird"){
				float obstacleState = other.GetComponent<Capacitor>().state;
				float forceX = transform.position.x - other.transform.position.x;
				float forceY = transform.position.y - other.transform.position.y;
				Vector3 forceVector = new Vector3(forceX, forceY, 0);
				
				if (!other.GetComponent<Capacitor>().stateBackwards){
					gameObject.rigidbody.AddForce((100f+4.5f*(100f-obstacleState))*forceVector.normalized);
				}
			}
		}
		else{
			if (name == "CurrentBird"){
				GUIControls.GameOver();
				gameObject.GetComponent<controls>().InitState(BirdState.Rewinding);
			} else if (name == "CurrentBird"){
				// Create ghost explosion
			}
		}
	}
}
