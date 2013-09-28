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
			if (GetComponent<controls>().CurrState == BirdState.PlayerControlled){
				float forceX = 200*(transform.position.x - other.transform.position.x);
				float forceY = 200*(transform.position.y - other.transform.position.y);
				print ("applying force x="+forceX+" y="+forceY);
				gameObject.rigidbody.AddForce(new Vector3(forceX, forceY, 0));
			}
		}
		else {
			// TODO Make explosion?
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
