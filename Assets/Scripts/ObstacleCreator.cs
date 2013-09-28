using UnityEngine;
using System.Collections;

public class ObstacleCreator : MonoBehaviour {
	
	public GameObject obstacle1;
	private float courseDistance;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(GUIControls.distance > courseDistance){
			float randomAhead = Random.value * 5 + 15;
			float randomY = Random.value * 15;
			float randomHeight = Random.value * 10 + 5;
			float randomWidth = Random.value * 5 + 1;
			GameObject newObstacle = (GameObject) GameObject.Instantiate(obstacle1, new Vector3(courseDistance + randomAhead, randomY, 0), Quaternion.identity);
			newObstacle.transform.localScale = new Vector3(randomWidth, randomHeight, 1);
			newObstacle.transform.parent = transform;
			courseDistance += Random.value * 10;
		}
	}
}
