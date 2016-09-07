using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Cast1 : MonoBehaviour
{
	public Text shotNum;
	private int numOfShot;
	GameObject hitObj;	//A object will be shoted! variable
	public iTweenPath path;   // tha path of shotPath
	public GameObject prefab;  //the shot prefab;
	private bool flag = true;
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		checkShotNum ();
		//cast the shot once you click the mouse button;
		if (flag && Input.GetMouseButtonDown (0)) {

			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit = new RaycastHit ();
			if (Physics.Raycast (ray.origin, ray.direction, out hit, 20.0f)) {

				path.nodes [0] = ray.origin;

				path.nodes [2] = hit.point;
				hitObj = (GameObject)Instantiate (prefab, new Vector3 (this.transform.position.x+0.2f , transform.position.y+0.2f, this.transform.position.z), Quaternion.identity);  
				path.nodes [1] = new Vector3 ((hit.point.x + hitObj.transform.position.x) / 2, (hit.point.y + hitObj.transform.position.y) / 2 + 0.2f, (hit.point.z + hitObj.transform.position.z) / 2);

				Vector3[] paths = new Vector3[3];
				paths [0] = path.nodes [0];
				paths [1] = path.nodes [1];
				paths [2] = path.nodes [2];
				iTween.MoveTo (hitObj, iTween.Hash ("path", paths, "speed", 20.0f, "easeType", iTween.EaseType.linear));
			
				numOfShot--;
				setShotTxt();
				//计算炮弹与目标点之间的距离
				//Vector3 direction = hit.transform.position - hitObj.transform.position;
				//发射炮弹

				//hitObj.GetComponent<Rigidbody>().AddForceAtPosition(direction.normalized,ray.origin,ForceMode.Impulse);
				//	hitObj.GetComponent<Rigidbody>().AddForce(direction*1000);
				//hitObj.GetComponent<Rigidbody>().AddForceAtPosition(direction, hit.transform.position,ForceMode.Impulse);
				//hitObj.GetComponent<Rigidbody>().AddForce(direction*100);
			}
		}

	}

	void checkShotNum(){
		numOfShot = int.Parse (shotNum.text.ToString ());
		if (numOfShot <= 0) {
			flag = false;
		}
	}

	void setShotTxt(){
		shotNum.text = numOfShot.ToString();
	}

	void OnCollisionEnter(Collision collision) {

	}
}