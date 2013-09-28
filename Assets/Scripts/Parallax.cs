using UnityEngine;
using System.Collections;

public class Parallax : RewindableObject<bool> {
	
	public float movementSpeed;
	
	private Vector3 initPosition;
	
	// Use this for initialization
	override protected void Start () {
		base.Start();
		initPosition = transform.position;
	}
	
	// Update is called once per frame
	override protected void ForwardFixedUpdate () {
		float movement = movementSpeed * (1.0f + (GUIControls.distance / 500.0f));
		if (Input.GetKey(KeyCode.Space)) {
			movement /= 3.0f;
		}
		transform.Translate(new Vector3(-movement, 0, 0));
		AddRewindState(false);
	}
	
	void Reset () {
		ResetRewind();
		transform.position = initPosition;
	}
}
