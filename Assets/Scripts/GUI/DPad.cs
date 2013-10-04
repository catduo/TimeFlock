using UnityEngine;
using System.Collections;

public class DPad : MonoBehaviour {

	private bool held = false;
	private bool heldPrevious = false;
	static public float vertical = 0;
	static public float horizontal = 0;
	public bool binaryInputs;
	public float diagonalSensitivity = 0.5F;
	public GameObject bird;
	BirdInputState bis;

	// Use this for initialization
	void Start () {
		bird = GameObject.Find("CurrentBird");
	}

	void FixedUpdate () {
		Debug.Log(horizontal.ToString() + "," + vertical.ToString());
		if(heldPrevious){
			vertical = (GUIControls.InputXYs[0].y - transform.position.y) / transform.lossyScale.z * 0.5F;
			horizontal = (GUIControls.InputXYs[0].x - transform.position.x) / transform.lossyScale.x * 0.5F;
			if(binaryInputs){
				if(vertical > diagonalSensitivity * Mathf.Abs(horizontal)){
					vertical = 1;
				}
				else if(vertical < - diagonalSensitivity * Mathf.Abs(horizontal)){
					vertical = -1;
				}
				else{
					vertical = 0;
				}
				if(horizontal > diagonalSensitivity * Mathf.Abs(vertical)){
					horizontal = 1;
				}
				else if(horizontal < - diagonalSensitivity * Mathf.Abs(vertical)){
					horizontal = -1;
				}
				else{
					horizontal = 0;
				}
			}
		}
		else{
			vertical = 0;
			horizontal = 0;
		}
		if(!held){
			heldPrevious = false;
		}
		held = false;
	}

	void Hold () {
		held = true;
		heldPrevious = true;
	}

	void Tap () {
		Hold();
	}
}