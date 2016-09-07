using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class XLine : MonoBehaviour {
	public GameObject Line;
	public GameObject FXef;//激光击中物体的粒子效果
	private Text shotNumTxt;
	private bool flag = true;
	private float shake_decay = 0.01f;
	private float shake_intensity = 0.3f;
	private Vector3 originPosition;
	private Quaternion originRotation;
	private bool animationFlag = false;
	private Transform myCamera;
	// Use this for initialization
	void Start(){
		myCamera = GameObject.Find ("Camera").transform;
		shotNumTxt = (Text) FindObjectOfType (typeof(Text));
	}
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Vector3 Sc = new Vector3 ();// 变换大小
		Sc.x=0.5f;
		Sc.z=0.5f;
		//发射射线，通过获取射线碰撞后返回的距离来变换激光模型的y轴上的值
        if (Physics.Raycast(transform.position, this.transform.forward, out hit)){
			if(flag && hit.collider.name == "Camera"){
				flag = false;
				Shake();
				lowShotTxt();
			}
			Debug.DrawLine(this.transform.position,hit.point);
			Sc.y=hit.distance;
			FXef.transform.position=hit.point;//让激光击中物体的粒子效果的空间位置与射线碰撞的点的空间位置保持一致；
			FXef.SetActive(true);
		}
		//当激光没有碰撞到物体时，让射线的长度保持为500m，并设置击中效果为不显示
		else{
			Sc.y=500;
		    FXef.SetActive(false);
		}
			
		Line.transform.localScale=Sc;

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
			shake_decay = 0.01f;
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
		if(numOfShot >= 3){
			numOfShot -= 3;
		}else{
			numOfShot = 0;
		}
		shotNumTxt.text = numOfShot.ToString();
	}

	void OnCollisionEnter(Collision collision){
		if (collision.collider.name == "Sphere(Clone)")
			Destroy (this.gameObject);

		if (collision.collider.name == "Camera") {

		}
	}
}
