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
				
				GUIControls.PlayerEnergy += 2f;
				if (GUIControls.PlayerEnergy > GUIControls.StartingPlayerEnergy) {
					GUIControls.PlayerEnergy = GUIControls.StartingPlayerEnergy;
				}
				
				if (!other.GetComponent<Capacitor>().stateBackwards){
					gameObject.rigidbody.AddForce(150f*forceVector.normalized);
				}
			}
		}
		else if (other.gameObject == GameObject.Find ("EndLevel")){
			if(Application.loadedLevel < 0){
				Application.LoadLevel(Application.loadedLevel + 1);
			}
			else{
				GameObject.Find ("GUI").GetComponent<GUIControls>().RestartGame();
				GameObject.Find ("Menu").SendMessage("MenuOn");
				Time.timeScale = 0;
			}
		}
		else {
			if (GetComponent<controls>().Rewinding) return;
			
			if (currState == BirdState.PlayerControlled) {
				// Player controlled bird hit an obstacle
				GetComponent<controls>().OnDeath();
				GUIControls.GameOver();
			}
		}
	}
}
