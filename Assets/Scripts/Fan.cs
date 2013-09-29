using UnityEngine;
using System.Collections;

public class Fan : RewindableObject<Quaternion> {
	
	Quaternion initialRot;
	public bool clockwise;
	
	// Use this for initialization
	override protected void Start () {
		initialRot = transform.rotation;
		base.Start();
	}
	
	override protected void ResetToBeginning() {
		transform.rotation = initialRot;
	}
	
	// Update is called once per frame
	override protected void ForwardFixedUpdate () {
		if(clockwise){
			transform.Rotate(new Vector3(0,0,-1));
		}
		else{
			transform.Rotate(new Vector3(0,0,1));
		}
		AddRewindState(transform.rotation, false);
	}
	
	override protected void ApplyCustom(Quaternion q) {
		transform.rotation = q;
	}
}
