using UnityEngine;
using System.Collections;

public class MuteButton : MonoBehaviour {
	
	private bool muted;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Tap () {
		if(muted){
			muted = false;
			AudioListener.volume = 1;
			renderer.material.mainTextureOffset = new Vector2(0.25F, 0.5F);
		}
		else{
			muted = true;
			AudioListener.volume = 0;
			renderer.material.mainTextureOffset = new Vector2(0.25F, 0F);
		}
	}
}
