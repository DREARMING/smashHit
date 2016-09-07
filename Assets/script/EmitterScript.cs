using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmitterScript : MonoBehaviour {

	public GameObject prefab;   //the prefab of crystal which prepared for cutting
	public PhysicMaterial PhysicM;  //the prefab's physical material
	public Material ChunkM;    // the chunk of material
	private FracturedObject comp;	//add the component obj to gameobj;
	private GameObject EmitterGameObject;

	public EmitterScript(Vector3 position){
		this.transform.position = position;
	}
	// Use this for initialization
	void Start () {
		Transform[]	childs = GetComponentsInChildren<Transform> ();
		EmitterGameObject = childs [1].gameObject;
		EmitterGameObject.AddComponent<FracturedObject> ();
		comp = EmitterGameObject.GetComponent<FracturedObject> ();
		comp.SourceObject = (GameObject)Instantiate (prefab);
		//set the chunks position
		comp.SourceObject.gameObject.transform.position = EmitterGameObject.transform.position;
		comp.StartStatic = true;
		comp.ChunkPhysicMaterial = PhysicM;
		comp.SplitMaterial = ChunkM;
		comp.GenerateNumChunks = 10;
		List<GameObject> list;
		comp.EventDetachedMinLifeTime = 2;
		comp.EventDetachedMaxLifeTime = 3;
		UltimateFracturing.Fracturer.FractureToChunks (comp, true, out list, null);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDestroy(){
		Destroy (comp.SourceObject);
		Destroy (comp);
		Destroy (EmitterGameObject);
		Destroy (this.gameObject);
	}
}
