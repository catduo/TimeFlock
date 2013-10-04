using UnityEngine;
using System.Collections;

public class FTUE : MonoBehaviour {

	private string[] ftueText;
	private string[] ftueTitle;
	public TextMesh ftueTextMesh;
	public TextMesh ftueTitleTextMesh;
	public TextMesh ftueCTA;
	public Transform ftueBackground;
	public Transform ftueCollider;
	public Transform mainCamera;
	public Transform ftueImages;
	static public int ftueLocation = 0;

	// Use this for initialization
	void Start () {
		string ftue1 = "I'm the Forest Spirit and \n I'll be teaching you \n how to play.";
		string ftue2 = "To grow your forest, jump to \n the clouds and busrt them,\n each time you burst a cloud \n you will get a boost.";
		string ftue3 = "The darker clouds will take \n extra hits to burst.";
		string ftue4 = "Yellow lightning clouds will \n set the forest ablaze.";
		string ftue5 = "Purple acid clouds will \n destroy living trees.";
		string ftue6 = "Blue ice clouds will \n freeze you.";
		string ftue7 = "Jump from cloud to cloud to \n get a combo multiplier.  \n Colorful clouds will break \n your combo.";
		string ftue1t = "Welcome to CloudBurst!";
		string ftue2t = "Burst the Clouds!";
		string ftue3t = "Brighten Dark Clouds!";
		string ftue4t = "Avoid Colorful Clouds!";
		string ftue5t = "Avoid Colorful Clouds!";
		string ftue6t = "Avoid Colorful Clouds!";
		string ftue7t = "Combo your Bursts!";
		ftueText = new string[] {ftue1, ftue2, ftue3, ftue4, ftue5, ftue6, ftue7};
		ftueTitle = new string[] {ftue1t, ftue2t, ftue3t, ftue4t, ftue5t, ftue6t, ftue7t};
		HideFTUE();
	}

	// Update is called once per frame
	void Update () {

	}

	public void ShowFTUE() {
		if(ftueLocation > 0){
			for(int i = 0; i < ftueImages.FindChild("FtueImages" + (ftueLocation).ToString()).childCount; i++){
				ftueImages.FindChild("FtueImages" + (ftueLocation).ToString()).GetChild(i).renderer.enabled = false;
			}
		}
		if(ftueLocation < ftueText.Length){
			for(int i = 0; i < ftueImages.FindChild("FtueImages" + (ftueLocation + 1).ToString()).childCount; i++){
				Debug.Log ("run1");
				ftueImages.FindChild("FtueImages" + (ftueLocation + 1).ToString()).GetChild(i).renderer.enabled = true;
			}
			transform.position = new Vector3(0,0,mainCamera.position.z + 3F);
			ftueBackground.renderer.enabled = true;
			ftueTextMesh.renderer.enabled = true;
			ftueCTA.renderer.enabled = true;
			ftueTitleTextMesh.renderer.enabled = true;
			ftueTextMesh.text = ftueText[ftueLocation];
			ftueTitleTextMesh.text = ftueTitle[ftueLocation];
			ftueLocation ++;
		}
		else{
			HideFTUE ();
		}
	}
	public void HideFTUE() {
		if(ftueLocation > 0){
			for(int i = 0; i < ftueImages.FindChild("FtueImages" + (ftueLocation).ToString()).childCount; i++){
			Debug.Log ("run1");
				ftueImages.FindChild("FtueImages" + (ftueLocation).ToString()).GetChild(i).renderer.enabled = false;
			}
		}
		transform.position = new Vector3(1000,1000,mainCamera.position.z - 5F);
		ftueBackground.renderer.enabled = false;
		ftueTextMesh.renderer.enabled = false;
		ftueCTA.renderer.enabled = false;
		ftueTitleTextMesh.renderer.enabled = false;
		ftueCollider.collider.enabled = false;
	}
}