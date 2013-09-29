using UnityEngine;
using System.Collections;

public class LaserShot : RewindableObject<bool> {

	// Use this for initialization
	override protected void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	override protected void ForwardFixedUpdate () {
		transform.Translate(transform.up * 0.8f);
		AddRewindState(false);
	}
	
	override public void DoneRewinding() {
		Destroy (this);
		GameObject.Destroy(this.gameObject);
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.name == "FlockCapacitor(Clone)") {
			GameObject.Destroy(this.gameObject);
		}
	}
}
