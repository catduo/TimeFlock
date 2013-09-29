using UnityEngine;
using System.Collections;

public class Parallax : RewindableObject<bool> {
	
	public float movementSpeed;
	
	private Vector3 initPosition;
	
	// Use this for initialization
	override protected void Start () {
		initPosition = transform.position;
		base.Start();
	}
	
	// Update is called once per frame
	override protected void ForwardFixedUpdate () {
		float movement = movementSpeed * (1.0f + (GUIControls.distance / 500.0f));
		transform.Translate(new Vector3(-movement, 0, 0));
		AddRewindState(false);
	}
	
	override protected void ResetToBeginning () {
		transform.position = initPosition;
	}
}
