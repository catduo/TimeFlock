using UnityEngine;
using System.Collections;

public class SpriteAnimation : MonoBehaviour {
	
	private int animationSpeed = 1;
	private int animationPosition = 0;
	private int animationFrame = 0;
	private float xOffset = 0.25F;
	private float yOffset = 0.25F;
	private int oldOffset = 6;
	private bool isOld = false;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.parent.GetComponent<controls>().CurrState == BirdState.Replaying){
			isOld = true;
		}
		if(isOld){
			if(animationFrame<8){
				renderer.material.mainTextureOffset = new Vector2(xOffset * (animationFrame - 6) + 0.5F, yOffset * 2);
			}
			else{
				renderer.material.mainTextureOffset = new Vector2(xOffset * (animationFrame - 8), yOffset);
			}
			if(animationFrame < 11){
				animationFrame++;
			}
			else{
				animationFrame = 6;
			}
		}
		else{
			if(animationFrame<4){
				renderer.material.mainTextureOffset = new Vector2(xOffset * animationFrame, yOffset * 3);
			}
			else{
				renderer.material.mainTextureOffset = new Vector2(xOffset * (animationFrame - 4), yOffset * 2);
			}
			if(animationFrame < 5){
				animationFrame++;
			}
			else{
				animationFrame = 0;
			}
		}
	}
}
