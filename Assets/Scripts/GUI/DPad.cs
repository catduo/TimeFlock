using UnityEngine;
using System.Collections;

public class DPad : MonoBehaviour {
	
	static public int HAxis = 0;
	static public int VAxis = 0;
	private GameObject bird;
	BirdInputState bis;
	
	// Use this for initialization
	void Start () {
		bird = GameObject.Find("CurrentBird");
	}
	
	// Update is called once per frame
	void Update () {
		bis.VAxis = VAxis;
		bis.HAxis = HAxis;
		if(HAxis != 0 || VAxis != 0){
			bird.GetComponent<controls>().ApplyInputs(bis);
		}
	}
}
