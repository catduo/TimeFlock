using UnityEngine;
using System.Collections;

public class Parallax : RewindableObject<bool> {
	
	public float movementSpeed;
	
	// Use this for initialization
	override protected void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	override protected void ForwardFixedUpdate () {
		if (!GUIControls.IsPaused) {
			float movement = movementSpeed * (1.0f + (GUIControls.distance / 500.0f));
			transform.Translate(new Vector3(-movement, 0, 0));
			AddRewindState(false);
		}
	}
}
