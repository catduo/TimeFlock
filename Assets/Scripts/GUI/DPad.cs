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
	public float initialX = 0;
	public float initialY = 0;
	public float offset = 0;
	public float directionalSize = 0;
	public float outsideBounds = 0;

	// Use this for initialization
	void Start () {
		bird = GameObject.Find("CurrentBird");
		offset = (Camera.main.WorldToScreenPoint(new Vector2(1,1)).x-Camera.main.WorldToScreenPoint(new Vector2(0,0)).x);
		directionalSize = Screen.dpi * Mathf.Min(1.25F, (Screen.width/Screen.dpi)/4);
		outsideBounds = Screen.dpi * Mathf.Min(1.125F, (Screen.width/Screen.dpi - 0.5F)/4);
	}

	void FixedUpdate () {
		Debug.Log(horizontal.ToString() + "," + vertical.ToString());
		foreach(Touch touch in Input.touches){
			if(touch.position.y < Screen.width/2){
				if(touch.phase == TouchPhase.Began){
					initialX = touch.position.x;
					initialY = touch.position.y;
				}
				else if (touch.phase == TouchPhase.Ended){
					initialX = touch.position.x;
					initialY = touch.position.y;
				}
				SendJoystick(touch);
			}
		}
	}
	
	Vector2 SendJoystick(Touch touch){
		float touchHorizontal = 0;
		float touchVertical = 0;
		if(touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled){
			Vector2 initial = new Vector2(initialX, initialY);
			Vector2 final = touch.position - initial;
			if(final.magnitude > outsideBounds){
				final = final.normalized * outsideBounds / Screen.dpi;
			}
			else{
				final /= Screen.dpi;
			}
			touchVertical = final.y;
			touchHorizontal = final.x;
		}
		return new Vector2(horizontal, vertical);
	}
}