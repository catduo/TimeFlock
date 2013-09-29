using UnityEngine;
using System.Collections;

public class ObstacleCreator : MonoBehaviour {
	
	public GameObject obstacle1;
	public GameObject obstacle2;
	public GameObject obstacle3;
	public GameObject obstacle4;
	private float courseDistance = 20;
	public GameObject[] obstacles;
	
	// Use this for initialization
	void Start () {
		obstacles = new GameObject[] {obstacle1, obstacle2, obstacle3, obstacle4};
	}
	
	// Update is called once per frame
	void Update () {
		if(GUIControls.distance > courseDistance){
			float randomY = Random.value * 15;
			float randomHeight = Random.value * 10 + 5;
			float randomAhead = randomHeight - 3;
			float randomWidth = Random.value * 5 + 1;
			int randomObstacle = Mathf.FloorToInt(Random.value * 4);
			GameObject thisObstacle = obstacles[randomObstacle];
			GameObject newObstacle = (GameObject) GameObject.Instantiate(thisObstacle, new Vector3(courseDistance + randomAhead, randomY, 0), Quaternion.identity);
			newObstacle.transform.localScale = new Vector3(randomWidth, randomHeight, 1);
			newObstacle.transform.parent = transform;
			courseDistance += Random.value * 10;
		}
	}
}
