using UnityEngine;
using System.Collections.Generic;

public struct BirdInputState {
	public int HAxis, VAxis;
}

public enum BirdState {
	PlayerControlled,
	Replaying,
	Dead
};

public class controls : RewindableObject<bool> {
	
	public static Vector3 StartingPositionPC = new Vector3(5.0f, 5.0f, -0.1f);
	public static Vector3 StartingPositionReplay = new Vector3(5.0f, 5.0f, 0.0f);
	public Material NonPlayerMaterial;
	public Transform FlockCapacitorPrefab;
	
	public BirdState CurrState;
	List<BirdInputState> inputs;
	int currFrame;
	
	public float movementForce = 50.0f;
	
	public void InitState(BirdState s) {
		CurrState = s;
		if (s == BirdState.Dead) {
			SetDrawn(false);
		}
		else {
			if (s == BirdState.PlayerControlled) {
				inputs.Clear();
				transform.position = StartingPositionPC;
			}
			else if (s == BirdState.Replaying) {
				transform.position = StartingPositionReplay;
			}
			
			if (s == BirdState.PlayerControlled || s == BirdState.Replaying) {
				SetDrawn(true);
				
				currFrame = 0;
				ResetRewind();
				rigidbody.velocity = Vector3.zero;
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
	
	public void OnDeath() {
		// Create explosion
		Transform newFluxT = (Transform) GameObject.Instantiate(FlockCapacitorPrefab, transform.position, Quaternion.identity);
		newFluxT.parent = GameObject.Find("Obstacles").transform;
	}
	
	// Use this for initialization
	override protected void Start () {
		base.Start ();
		inputs = new List<BirdInputState>();
		InitState(BirdState.PlayerControlled);
	}
	
	override protected void FixedUpdate() {
		base.FixedUpdate();
	}
	
	// Update is called once per frame
	override protected void ForwardFixedUpdate () {
		if (CurrState == BirdState.Dead) {
			AddRewindState(false);
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
		}
	}
	
	BirdInputState GetKeys () {
		var bis = new BirdInputState();
		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)){
			bis.VAxis += 1;
		}
		if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)){
			bis.VAxis -= 1;
		}
		if (Input.GetKey (KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
			bis.HAxis -= 1;
		}
		if (Input.GetKey (KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
			bis.HAxis += 1;
		}
		return bis;
	}
	
	public void ApplyInputs(BirdInputState bis) {
		rigidbody.AddForce(bis.HAxis * movementForce, bis.VAxis * movementForce, 0.0f);
		rigidbody.velocity = rigidbody.velocity / 1.05f;
		keepInBounds();
		
		AddRewindState(false);
	}
	
	void DoReplay() {
		ApplyInputs(inputs[currFrame]);
		currFrame += 1;
		if (currFrame >= inputs.Count) {
			OnDeath();
			InitState(BirdState.Dead);
		}
	}
	
	override protected void ApplyCustom(bool custom) {
	}
	
	override protected void DoneRewinding() {
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
		float newX = Mathf.Clamp(transform.position.x, GUIControls.BoundsMinX, GUIControls.BoundsMaxX);
		float newY = Mathf.Clamp(transform.position.y, GUIControls.BoundsMinY, GUIControls.BoundsMaxY);

		transform.position = new Vector3(newX, newY, 0);
		
		if ((newX == GUIControls.BoundsMinX && rigidbody.velocity.x < 0)
			||(newX == GUIControls.BoundsMaxX && rigidbody.velocity.x > 0)){
			rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
		}		
		if ((newY == GUIControls.BoundsMinY && rigidbody.velocity.y < 0)
			||(newY == GUIControls.BoundsMaxY && rigidbody.velocity.y > 0)){
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, 0);
		}
	}
}
