using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	public int distance = 15;
	public int speedX = 0;
	public int speedY = 5;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x <= distance) {
			rigidbody.velocity = new Vector3(speedX, speedY, 0);
		}
	}
		
}
