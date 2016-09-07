using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GlassHit : MonoBehaviour {
	private bool isTrigger = true;
	private Text shotNumTxt;
	private bool flag = true;
	private float shake_decay = 0.005f;
	private float shake_intensity = 0.3f;
	private Vector3 originPosition;
	private Quaternion originRotation;
	private bool animationFlag = false;
	private Transform myCamera;

	// Use this for initialization
	void Start () {
		myCamera = GameObject.Find ("Camera").transform;
		shotNumTxt = (Text) FindObjectOfType (typeof(Text));
	}
	
	// Update is called once per frame
	void Update () {
		if (animationFlag && shake_intensity > 0) {
			myCamera.position = originPosition + Random.insideUnitSphere * shake_intensity;
			myCamera.rotation = new Quaternion (
				originRotation.x + Random.Range (-shake_intensity, shake_intensity) * .2f,
				originRotation.y + Random.Range (-shake_intensity, shake_intensity) * .2f,
				originRotation.z + Random.Range (-shake_intensity, shake_intensity) * .2f,
				originRotation.w + Random.Range (-shake_intensity, shake_intensity) * .2f);
			shake_intensity -= shake_decay;
		} else if (animationFlag && shake_intensity <= 0) {
			myCamera.rotation = Quaternion.LookRotation(Vector3.left);
			animationFlag = false;
			shake_decay = 0.005f;
			shake_intensity = 0.3f;
		}
	}

	void Shake(){
		originPosition = myCamera.position;
		originRotation = myCamera.rotation;
		animationFlag = true;
	}

	void lowShotTxt(){
		int numOfShot = int.Parse (shotNumTxt.text.ToString());
		if(numOfShot > 5){
			numOfShot -= 5;
		}else{
			numOfShot = 0;
		}
		shotNumTxt.text = numOfShot.ToString();
	}

	void OnTriggerExit(Collider collider){
		if (!isTrigger) {
			return;
		}
		isTrigger = false;
		print ("Class Hit" + collider.name);
		if (collider.name == "Camera") {
			Shake();
			lowShotTxt ();
		}
	}
}
