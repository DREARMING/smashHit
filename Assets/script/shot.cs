using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class shot : MonoBehaviour {

	private Text shotNumTxt;
	private bool hited = true;
	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, 2);
	}
	
	// Update is called once per frame
	void Update () {
		shotNumTxt = (Text) FindObjectOfType (typeof(Text));
	}

	void AddShotTxt(){
		int numOfShot = int.Parse (shotNumTxt.text.ToString());
		numOfShot += 3;
		shotNumTxt.text = numOfShot.ToString();
	}

	void OnCollisionExit(Collision collision){
		if (this.hited) {
			if (collision.collider.name.Contains ("crystal4(Clone)")&& this.hited) {
				this.hited = false;
				AddShotTxt();
				return;
			}
			if(collision.collider.name.Contains("Emitter(Clone)")&& this.hited){
				this.hited = false;
				GameObject ray = GameObject.FindGameObjectWithTag("Ray");
				if(ray!=null)
				Destroy(ray);
				return;
			}
			if(collision.collider.name.Contains("Emitter_Animation(Clone)")&& this.hited){
				this.hited = false;
				GameObject ray = GameObject.FindGameObjectWithTag("Ray_Animation");
				if(ray!=null)
				Destroy(ray);
				return;
			}
			if(collision.collider.name.Contains("glassnomove(Clone)")&& this.hited){
				this.hited = false;
				GameObject ray = GameObject.FindGameObjectWithTag("ClassNoMove_Tag");
				if(ray!=null)
					Destroy(ray);
				return;
			}
		}
	}
}
