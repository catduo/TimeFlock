using UnityEngine;
using System.Collections;

public class controls : MonoBehaviour {
	
	public int force = 20;
	
	// Bounds of the screen where the birds can fly
	private int minX = 2;
	private int maxX = 15;
	private int minY = 1;
	private int maxY = 19;
	// Distance from the border to start feeling pushed away
	private int tolerance = 3;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetKeys ();
		keepInBounds();
		fixVelocity();
	}
	
	void GetKeys () {
		if (Input.GetKey (KeyCode.W)){
			controlUp();
		}
		if (Input.GetKey (KeyCode.S)){
			controlDown();
		}
		if (Input.GetKey (KeyCode.A)){
			controlLeft();
		}
		if (Input.GetKey (KeyCode.D)){
			controlRight();
		}
	}
	
	//press up, keys or touch controls
	void controlUp(){
		rigidbody.AddForce(Vector3.up * force);
	}
	
	//press down, keys or touch controls
	void controlDown(){
		rigidbody.AddForce(Vector3.down * force);
	}
	
	//press left, keys or touch controls
	void controlLeft(){
		rigidbody.AddForce(Vector3.left * force);
	}
	
	//press right, keys or touch controls
	void controlRight(){
		rigidbody.AddForce(Vector3.right * force);
	}
	
	// Supposed to slow down when approaching the borders; not quite working yet
	void fixVelocity(){
		/*if (transform.position.x >= maxX - tolerance && rigidbody.velocity.x > 0){
			print ("Fixing x velocity because x big =" + transform.position.x);
			rigidbody.velocity = new Vector3(rigidbody.velocity.x/2,rigidbody.velocity.y,0);
		}
		else if (transform.position.x <= minX + tolerance && rigidbody.velocity.x < 0){
			print ("Fixing x velocity because x small =" + transform.position.x);
			rigidbody.velocity = new Vector3(rigidbody.velocity.x/2,rigidbody.velocity.y,0);
		}
		else if (transform.position.y >= maxY - tolerance && rigidbody.velocity.y > 0){
			print ("Fixing y velocity because y big =" + transform.position.y);
			rigidbody.velocity = new Vector3(rigidbody.velocity.x,rigidbody.velocity.y/2,0);
		}
		else if (transform.position.y <= minY + tolerance && rigidbody.velocity.y < 0){
			print ("Fixing y velocity because y small =" + transform.position.y);
			rigidbody.velocity = new Vector3(rigidbody.velocity.x,rigidbody.velocity.y/2,0);
		}*/
	}
	
	void keepInBounds(){
		float newX = Mathf.Clamp(transform.position.x, minX, maxX);
		float newY = Mathf.Clamp(transform.position.y, minY, maxY);

		transform.position = new Vector3(newX, newY, 0);
		
		if ((newX == minX && rigidbody.velocity.x < 0)
			||(newX == maxX && rigidbody.velocity.x > 0)){
			rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
		}		
		if ((newY == minY && rigidbody.velocity.y < 0)
			||(newY == maxY && rigidbody.velocity.y > 0)){
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, 0);
		}
	}
}
