using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Crystal : MonoBehaviour{


	public GameObject prefab;   //the prefab of crystal which prepared for cutting
    public PhysicMaterial PhysicM;  //the prefab's physical material
    public Material ChunkM;    // the chunk of material
	private FracturedObject comp;	//add the component obj to gameobj;

	// Use this for initialization
	void Start () {
		this.gameObject.AddComponent<FracturedObject> ();
		this.name = "prefab";
		comp = this.gameObject.GetComponent<FracturedObject> ();
		comp.SourceObject = (GameObject)Instantiate (prefab);
		//set the chunks position
		comp.SourceObject.gameObject.transform.position = this.gameObject.transform.position;
		
		comp.StartStatic = true;
		comp.ChunkPhysicMaterial = PhysicM;
		comp.SplitMaterial = ChunkM;
		comp.GenerateNumChunks = 20;
		List<GameObject> list;
		comp.EventDetachedMinLifeTime = 2;
		comp.EventDetachedMaxLifeTime = 3;
		UltimateFracturing.Fracturer.FractureToChunks (comp, true, out list, null);
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnDestroy(){
		Destroy (comp.SourceObject.gameObject);
		Destroy (this.gameObject.GetComponent<FracturedObject> ());
		Destroy (this.gameObject);
	}
}
