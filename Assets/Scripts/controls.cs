using UnityEngine;
using System.Collections.Generic;

public struct BirdRewindState {
	public float PosX, PosY;
	public int Alive;
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
	
	BirdState currState;
	List<BirdInputState> inputs;
	List<BirdRewindState> rewind;
	int currFrame;
	
	public float force = 20;
	
	public void InitState(BirdState s) {
		currState = s;
		if (s == BirdState.Dead) {
			this.enabled = false;
			this.gameObject.SetActive(false);
		}
		else {
			this.enabled = true;
			this.gameObject.SetActive(true);
			
			if (s == BirdState.PlayerControlled) {
				inputs.Clear();
			}
			
			if (s == BirdState.PlayerControlled || s == BirdState.Replaying) {
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
	
	// Use this for initialization
	void Start () {
		inputs = new List<BirdInputState>();
		rewind = new List<BirdRewindState>();
		InitState(BirdState.PlayerControlled);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (currState == BirdState.Dead) {
			return;
		}
		
		switch (currState) {
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
		rewind.Add (brs);
	}
	
	void DoReplay() {
		ApplyInputs(inputs[currFrame]);
		currFrame += 1;
		if (currFrame >= inputs.Count) {
			InitState(BirdState.Dead);
		}
	}
	
	void DoRewind() {
		if (currFrame < 0) {
			return;
		}
		transform.position = new Vector3(rewind[currFrame].PosX, rewind[currFrame].PosY, 0.0f);
		currFrame -= 1;
	}
}
