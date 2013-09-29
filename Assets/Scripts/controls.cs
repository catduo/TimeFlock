using UnityEngine;
using System.Collections.Generic;

public struct BirdPlaybackState {
	public Vector3 Position;
	public bool DeathByCollision;
}

public struct BirdInputState {
	public int HAxis, VAxis;
	public bool SlowDownPressed;
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
	public Transform TrailRendererPrefab;
	
	public BirdState CurrState;
	List<BirdPlaybackState> replay;
	int currFrame;
	
	public float movementForce = 50.0f;
	
	public void InitState(BirdState s) {
		CurrState = s;
		if (s == BirdState.Dead) {
			SetDrawn(false);
			OnDeath ();
		}
		else {
			if (s == BirdState.PlayerControlled) {
				replay.Clear();
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
		if (s == BirdState.PlayerControlled) {
			GUIControls.initBirdControls(gameObject);
		}
	}
	
	public void MakeNonPlayer() {
		transform.parent = GameObject.Find ("OtherBirds").transform;
		foreach (Transform t in transform) {
			t.gameObject.renderer.material = NonPlayerMaterial;
		}
		Destroy (collider);
		var trailRenderer = (Transform)Instantiate(TrailRendererPrefab);
		trailRenderer.parent = this.transform;
	}
	
	public void OnDeath() {
		// Create explosion
		Transform newFluxT = (Transform) GameObject.Instantiate(FlockCapacitorPrefab, transform.position, Quaternion.identity);
		newFluxT.parent = GameObject.Find("Obstacles").transform;
		
		if (CurrState == BirdState.PlayerControlled) {
			var bps = new BirdPlaybackState();
			bps.Position = transform.position;
			bps.DeathByCollision = true;
			replay.Add(bps);
		}
	}
	
	// Use this for initialization
	override protected void Start () {
		base.Start ();
		replay = new List<BirdPlaybackState>();
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
			var bis = GetInputs();
			ApplyInputs(bis);
			AddReplay();
			break;
		case BirdState.Replaying:
			DoReplay();
			break;
		}
	}
	
	BirdInputState GetInputs() {
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
		
		if (Input.GetKey(KeyCode.Space)) {
			bis.SlowDownPressed = true;
		}
		
		return bis;
	}
	
	public void ApplyInputs (BirdInputState bis) {
		rigidbody.AddForce(bis.HAxis * movementForce, bis.VAxis * movementForce, 0.0f);
		rigidbody.velocity = rigidbody.velocity / 1.05f;
		keepInBounds();
		if (bis.SlowDownPressed) {
			Time.timeScale = 0.5f;
		}
		else {
			Time.timeScale = 1.0f;
		}
		
		AddRewindState(false);
	}
	
	public void AddReplay() {
		var bps = new BirdPlaybackState();
		bps.Position = transform.position;
		bps.DeathByCollision = false;
		replay.Add(bps);
	}
	
	void DoReplay() {
		rigidbody.velocity = Vector3.zero;
		transform.position = replay[currFrame].Position + new Vector3(0.0f, 0.0f, 0.1f);
		AddRewindState(false);
		if (replay[currFrame].DeathByCollision) {
			InitState(BirdState.Dead);
			return;
		}
		currFrame += 1;
		if (currFrame >= replay.Count) {
			CurrState = BirdState.Dead;
			SetDrawn(false);
		}
	}
	
	void keepInBounds(){
		float newX = Mathf.Clamp(transform.position.x, GUIControls.BoundsMinX, GUIControls.BoundsMaxX);
		float newY = Mathf.Clamp(transform.position.y, GUIControls.BoundsMinY, GUIControls.BoundsMaxY);
		
		var newZ = (CurrState == BirdState.PlayerControlled) ? -0.1f : 0.0f;
		transform.position = new Vector3(newX, newY, newZ);
		
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
