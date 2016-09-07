using UnityEngine;
using System.Collections;

public class RayScript : MonoBehaviour {

	private Transform[] gameobjs;
	// Use this for initialization
	void Start () {
		gameobjs = this.gameObject.GetComponentsInChildren<Transform> ();
		//Destroy (gameobjs [1].gameObject);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
