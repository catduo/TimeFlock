using UnityEngine;
using System.Collections;

public class Capacitor : MonoBehaviour {
	
	public float state = 0f;
	public bool stateBackwards = false;
	
	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		state += (stateBackwards? - 5 : 5);
		state = Mathf.Clamp(state, 0, 100);
			
		//Debug, we might do whatever animation instead of scale
		transform.localScale = new Vector3(5*state/100, 5*state/100, 5*state/100);
		if (state == 100) stateBackwards = true;
		if (state == 0) gameObject.renderer.enabled = false;
	}
}
