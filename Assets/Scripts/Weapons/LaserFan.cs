using UnityEngine;
using System.Collections;

public class LaserFan : RewindableObject<float> {
	
	public Transform LaserShotPrefab;
	
	const int ShotRate = 15;
	
	bool startRotating = false;
	int shotCountdown = 0;
	float rotation;
	
	override protected void ResetToBeginning() {
		//base.ResetToBeginning();
		startRotating = false;
		rotation = -80.0f;
		shotCountdown = ShotRate;
		SetDrawn(true);
	}

	// Use this for initialization
	override protected void Start () {
		base.Start();
		ResetToBeginning();
	}
	
	override protected void ApplyCustom(float r) {
		rotation = r;
	}
	
	// Update is called once per frame
	override protected void ForwardFixedUpdate () {
		if (transform.position.x <= 26 && !startRotating) {
			startRotating = true;
			shotCountdown = ShotRate;
		}
		
		if (startRotating) {
			if (transform.position.x < -1) {
				startRotating = false;
				return;
			}
			
			rotation += 1.2f;
			shotCountdown -= 1;
			if (shotCountdown <= 0) {
				shotCountdown = ShotRate;
				// TODO: fire a shot
				var shot = (Transform)Instantiate(LaserShotPrefab, transform.position + new Vector3(0.0f, 0.0f, -0.1f),
					LaserShotPrefab.transform.rotation);
				shot.transform.parent = transform;
				shot.RotateAround(transform.position, new Vector3(0.0f, 0.0f, -1.0f), rotation);
			}
		}
		
		AddRewindState(rotation, false);
	}
}
