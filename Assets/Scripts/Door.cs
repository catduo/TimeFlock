﻿using UnityEngine;
using System.Collections;

public class Door : RewindableObject<float> {
	public int distance = 15;
	public int speedX = 0;
	public int speedY = 5;
	
	// Use this for initialization
	override protected void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	override protected void ForwardFixedUpdate () {
		if (!GUIControls.IsPaused) {
			if (transform.position.x <= distance) {
				rigidbody.velocity = new Vector3(speedX, speedY, 0);
			}
			AddRewindState(transform.position.y, false);
		}
		else {
			rigidbody.velocity = Vector3.zero;
		}
	}
	
	override protected void ApplyCustom(float y) {
		transform.position = new Vector3(transform.position.x, y, transform.position.z);
	}
		
}
