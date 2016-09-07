using UnityEngine;
using System.Collections;

public class FirePoint : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision){
		//if(collision.collider.name.Contains("Sphere(Clone)"))
		Debug.Log ("fire cut"+collision.collider.name);
	}
}
