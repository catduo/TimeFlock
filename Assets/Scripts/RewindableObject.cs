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

public abstract class Rewindable : MonoBehaviour {
	abstract public void StartRewind();
	abstract public void ResetRewind();
	abstract public void DoneRewinding();
	
	virtual protected void Start() {
	}
	
	virtual protected void FixedUpdate() {
	}
}

public class RewindableObject<T> : Rewindable where T: struct {
	
	public bool Rewinding;
	int currRewindFrame;
	List<ObjectRewindState<T>> states;
	
	public bool IsDrawn() {
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
	
	public void SetDrawn(bool r) {
		if (GetComponent<Renderer>() != null) {
			renderer.enabled = r;
		}
		
		foreach (Transform child in transform) {
			if (child.gameObject.GetComponent<Renderer>() != null) {
				child.gameObject.renderer.enabled = r;
			}
		}
	}
	
	public override void StartRewind() {
		Rewinding = true;
		if (states == null) return;
		currRewindFrame = states.Count-1;
		if (rigidbody != null) rigidbody.velocity = Vector3.zero;
	}
	
	public override void ResetRewind() {
		states.Clear();
		currRewindFrame = 0;
		Rewinding = false;
		ResetToBeginning();
	}

	override protected void Start () {
		base.Start();
		states = new List<ObjectRewindState<T>>();
		SetDrawn(true);
		ResetRewind();
	}
	
	override protected void FixedUpdate() {
		base.FixedUpdate();
		if (Rewinding) {
			var ors = states[currRewindFrame];
			transform.position = ors.Pos;
			if (rigidbody != null) rigidbody.velocity = Vector3.zero;
			SetDrawn(ors.Drawn);
			ApplyCustom(ors.Custom);
			
			currRewindFrame -= 1;
			if (currRewindFrame < 0) {
				// Done rewinding
				Rewinding = false;
				DoneRewinding();
			}
		}
		else {
			ForwardFixedUpdate();
			currRewindFrame += 1;
		}
	}
	
	virtual protected void ApplyCustom(T custom) {
	}
	
	virtual protected void ForwardFixedUpdate() {
	}
	
	override public void DoneRewinding() {
		ResetToBeginning();
	}
	
	virtual protected void ResetToBeginning() {
	}
	
	protected void AddRewindState(T custom) {
		var ors = new ObjectRewindState<T>();
		ors.Pos = transform.position;
		ors.Drawn = IsDrawn();
		ors.Custom = custom;
		states.Add (ors);
		currRewindFrame += 1;
	}
}
