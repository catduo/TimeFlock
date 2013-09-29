using UnityEngine;
using System.Collections;

public class Capacitor : MonoBehaviour {
	
	public float state = 0f;
	private float maxState = 100f;
	public bool stateBackwards = false;
	
	// Use this for initialization
	void Start () {
		state = 0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		state += (stateBackwards? - 5 : 5);
		state = Mathf.Clamp(state, 0, 100);
			
		//Debug, we might do whatever animation instead of scale
		transform.localScale = new Vector3(3*state/100, 3*state/100, 3*state/100);
		if (state == 100) stateBackwards = true;
		if (state == 0) gameObject.renderer.enabled = false;
	}
	
	/*void SetRender(bool r) {
		foreach (Transform t in transform) {
			t.gameObject.renderer.enabled = r;
		}
	}*/
}
