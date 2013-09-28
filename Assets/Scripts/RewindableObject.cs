using UnityEngine;
using System.Collections.Generic;

public struct CustomRewindState {
	int I1, I2, I3;
	float F1, F2, F3;
	bool B1, B2, B3;
}

public struct ObjectRewindState<T> where T : struct {
	public Vector3 Pos;
	public bool Drawn;
	
	public T Custom;
}

public class RewindableObject<T> : MonoBehaviour {
	
	public bool Rewinding;
	int currFrame;
	List<ObjectRewindState<T>> states;
	
	bool IsDrawn() {
		if (GetComponent<Renderer>() != null) {
			return renderer.enabled;
		}
		
		foreach (Transform child in transform) {
			if (child.gameObject.GetComponent<Renderer>() != null) {
				return child.gameObject.renderer.enabled;
			}
		}
		
		return true;
	}
	
	void SetDrawn(bool r) {
		if (GetComponent<Renderer>() != null) {
			renderer.enabled = true;
		}
		
		foreach (Transform child in transform) {
			if (child.gameObject.GetComponent<Renderer>() != null) {
				child.gameObject.renderer.enabled = true;
			}
		}
	}
	
	public void StartRewind() {
		Rewinding = true;
		currFrame = 0;
		if (rigidbody != null) rigidbody.velocity = Vector3.zero;
	}
	
	public void ResetRewind() {
		states.Clear();
		currFrame = 0;
		Rewinding = false;
	}

	virtual void Start () {
		states = new List<ObjectRewindState>();
		ResetRewind();
	}
	
	void FixedUpdate() {
		if (Rewinding) {
			var ors = states[currFrame];
			transform.position = ors.Pos;
			if (rigidbody != null) rigidbody.velocity = Vector3.zero;
			SetDrawn(ors.Drawn);
			ApplyCustom(ors.Custom);
			
			currFrame -= 1;
			if (currFrame < 0) {
				// Done rewinding
				Rewinding = false;
				DoneRewinding();
			}
		}
		else {
			ForwardFixedUpdate();
		}
	}
	
	virtual void ApplyCustom(T custom) {
	}
	
	virtual void ForwardFixedUpdate() {
	}
	
	virtual void DoneRewinding() {
	}
	
	void AddRewindState(T custom) {
		var ors = new ObjectRewindState();
		ors.Pos = transform.position;
		ors.Drawn = IsDrawn();
		ors.Custom = custom;
		states.Add (ors);
		currFrame += 1;
	}
}
