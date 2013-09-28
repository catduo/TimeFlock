using UnityEngine;
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
	public Transform FlockCapacitorPrefab;
	
	public BirdState CurrState;
	List<BirdInputState> inputs;
	List<BirdRewindState> rewind;
	int currFrame;
	
	public float movementForce = 50.0f;
	
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
	
	public void OnDeath() {
		// Create explosion
		Transform newFluxT = (Transform) GameObject.Instantiate(FlockCapacitorPrefab, transform.position, Quaternion.identity);
		newFluxT.parent = GameObject.Find("Obstacles").transform;
	}
	
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
		
		var brs = new BirdRewindState();
		brs.PosX = transform.position.x;
		brs.PosY = transform.position.y;
		brs.Alive = true;
		rewind.Add (brs);
	}
	
	void DoReplay() {
		ApplyInputs(inputs[currFrame]);
		currFrame += 1;
		if (currFrame >= inputs.Count) {
			OnDeath();
			InitState(BirdState.Dead);
		}
	}
	
	void DoRewind() {
		rigidbody.velocity = Vector3.zero;
		if (currFrame < 0) {
			return;
		}
		
		SetRender (rewind[currFrame].Alive);
		transform.position = new Vector3(rewind[currFrame].PosX, rewind[currFrame].PosY, 0.0f);
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
