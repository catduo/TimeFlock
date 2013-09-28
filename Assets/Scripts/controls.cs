﻿using UnityEngine;
using System.Collections.Generic;

public struct BirdRewindState {
	public float PosX, PosY;
	public bool Alive;
}

public struct BirdInputState {
	public int HAxis, VAxis;
}

public enum BirdState {
	PlayerControlled,
	Replaying,
	Rewinding,
	Dead
};

public class controls : MonoBehaviour {
	
	public static Vector3 StartingPosition = new Vector3(5.0f, 5.0f, 0.0f);
	public Material NonPlayerMaterial;
	
	public BirdState CurrState;
	List<BirdInputState> inputs;
	List<BirdRewindState> rewind;
	int currFrame;
	
	public float force = 20;
	
	void SetRender(bool r) {
		foreach (Transform t in transform) {
			t.gameObject.renderer.enabled = r;
		}
	}
	
	public void InitState(BirdState s) {
		CurrState = s;
		if (s == BirdState.Dead) {
			SetRender(false);
		}
		else {
			if (s == BirdState.PlayerControlled) {
				inputs.Clear();
			}
			
			if (s == BirdState.PlayerControlled || s == BirdState.Replaying) {
				SetRender(true);
				
				currFrame = 0;
				rewind.Clear();
				transform.position = StartingPosition;
				rigidbody.velocity = Vector3.zero;
			}
			else if (s == BirdState.Rewinding) {
				currFrame = rewind.Count-1;
				var rs = rewind[currFrame];
				transform.position = new Vector3(rs.PosX, rs.PosY, 0.0f);
			}
		}
	}
	
	public void MakeNonPlayer() {
		transform.parent = GameObject.Find ("OtherBirds").transform;
		foreach (Transform t in transform) {
			t.gameObject.renderer.material = NonPlayerMaterial;
		}
		Destroy (collider);
	}
	
	// Bounds of the screen where the birds can fly
	private int minX = 2;
	private int maxX = 15;
	private int minY = 1;
	private int maxY = 19;
	// Distance from the border to start feeling pushed away
	private int tolerance = 3;
	
	// Use this for initialization
	void Start () {
		inputs = new List<BirdInputState>();
		rewind = new List<BirdRewindState>();
		InitState(BirdState.PlayerControlled);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (CurrState == BirdState.Dead) {
			// Add a "dead" rewind state
			var brs = new BirdRewindState();
			brs.PosX = transform.position.x;
			brs.PosY = transform.position.y;
			brs.Alive = false;
			rewind.Add (brs);
			return;
		}
		
		switch (CurrState) {
		case BirdState.PlayerControlled:
			var bis = GetKeys();
			inputs.Add (bis);
			ApplyInputs(bis);
			break;
		case BirdState.Replaying:
			DoReplay();
			break;
		case BirdState.Rewinding:
			DoRewind();
			break;
		}
	}
	
	BirdInputState GetKeys () {
		var bis = new BirdInputState();
		if (Input.GetKey (KeyCode.W)){
			bis.VAxis += 1;
		}
		if (Input.GetKey (KeyCode.S)){
			bis.VAxis -= 1;
		}
		if (Input.GetKey (KeyCode.A)){
			bis.HAxis -= 1;
		}
		if (Input.GetKey (KeyCode.D)){
			bis.HAxis += 1;
		}
		return bis;
	}
	
	public void ApplyInputs(BirdInputState bis) {
		rigidbody.AddForce(bis.HAxis * force, bis.VAxis * force, 0.0f);
		var brs = new BirdRewindState();
		brs.PosX = transform.position.x;
		brs.PosY = transform.position.y;
		brs.Alive = true;
		rewind.Add (brs);
		keepInBounds();
	}
	
	void DoReplay() {
		ApplyInputs(inputs[currFrame]);
		currFrame += 1;
		if (currFrame >= inputs.Count) {
			InitState(BirdState.Dead);
		}
	}
	
	void DoRewind() {
		rigidbody.velocity = Vector3.zero;
		if (currFrame < 0) {
			return;
		}
		
		SetRender (rewind[currFrame].Alive);
		if (rewind[currFrame].Alive) {
			transform.position = new Vector3(rewind[currFrame].PosX, rewind[currFrame].PosY, 0.0f);
		}
		currFrame -= 1;
	}
	
	// Supposed to slow down when approaching the borders; not quite working yet
	void fixVelocity(){
		/*if (transform.position.x >= maxX - tolerance && rigidbody.velocity.x > 0){
			print ("Fixing x velocity because x big =" + transform.position.x);
			rigidbody.velocity = new Vector3(rigidbody.velocity.x/2,rigidbody.velocity.y,0);
		}
		else if (transform.position.x <= minX + tolerance && rigidbody.velocity.x < 0){
			print ("Fixing x velocity because x small =" + transform.position.x);
			rigidbody.velocity = new Vector3(rigidbody.velocity.x/2,rigidbody.velocity.y,0);
		}
		else if (transform.position.y >= maxY - tolerance && rigidbody.velocity.y > 0){
			print ("Fixing y velocity because y big =" + transform.position.y);
			rigidbody.velocity = new Vector3(rigidbody.velocity.x,rigidbody.velocity.y/2,0);
		}
		else if (transform.position.y <= minY + tolerance && rigidbody.velocity.y < 0){
			print ("Fixing y velocity because y small =" + transform.position.y);
			rigidbody.velocity = new Vector3(rigidbody.velocity.x,rigidbody.velocity.y/2,0);
		}*/
	}
	
	void keepInBounds(){
		float newX = Mathf.Clamp(transform.position.x, minX, maxX);
		float newY = Mathf.Clamp(transform.position.y, minY, maxY);

		transform.position = new Vector3(newX, newY, 0);
		
		if ((newX == minX && rigidbody.velocity.x < 0)
			||(newX == maxX && rigidbody.velocity.x > 0)){
			rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
		}		
		if ((newY == minY && rigidbody.velocity.y < 0)
			||(newY == maxY && rigidbody.velocity.y > 0)){
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, 0);
		}
	}
}
