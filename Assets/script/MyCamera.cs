using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class MyCamera : MonoBehaviour {
	
	//private string filePath_crystal = "/Resources/positions.txt";
	//private string filePath_Ray_four = "/Resources/positions_ray_four.txt";
	//private string filePath_rayAnimation = "/Resources/positions_rayAnimation.txt";
	//private string filePath_GlassNoMove = "/Resources/positions_GlassNoMove.txt";
	public GameObject prefab_crystal;
	public GameObject prefab_ray_four;
	public GameObject prefab_rayAnimation;
	public GameObject prefab_glassNoMove;
	private float speed = 2.0f;
	private Vector3 originPosition;
	private Quaternion originRotation;
	public float shake_decay;
	public float shake_intensity;
	public bool animationFlag = false;
	private List<Vector3> list_crystal = new List<Vector3> ();
	private List<GameObject> crystal_list = new List<GameObject>();
	//存放四条ray的list
	private List<Vector3> list_ray_four = new List<Vector3>();
	private List<GameObject> ray_four_list = new List<GameObject> ();

	//存在两条ray的list
	private List<Vector3> list_rayAnimation = new List<Vector3> ();
	private List<GameObject> rayAnimation_list = new List<GameObject>();

	//存在玻璃的list
	private List<Vector3> list_glassNoMove = new List<Vector3> ();
	private List<GameObject> glassNoMove_list = new List<GameObject>();

	private int currentIndex_crystal = 0;
	private int clearIndex_crystal = 0;

	private int currentIndex_ray_four = 0;
	private int clearIndex_ray_four = 0;

	private int currentIndex_rayAnimation = 0;
	private int clearIndex_rayAnimation = 0;

	private int currentIndex_glassNoMove = 0;
	private int clearIndex_glassNoMove = 0;
	
	private const int distance = 10;
	private const float notSeenDistance = 1.0f;
	private long i =0;

	public TextAsset asset_Crystal;
	public TextAsset asset_ray_four;
	public TextAsset asset_rayAnimation;
	public TextAsset asset_glassNoMove;
	// Use this for initialization
	void Start () {
		getVector3Position (list_crystal,asset_Crystal);
		getVector3Position (list_ray_four,asset_ray_four);
		getVector3Position (list_rayAnimation,asset_rayAnimation);
		getVector3Position (list_glassNoMove,asset_glassNoMove);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate (Vector3.forward*Time.deltaTime*speed);
		if (animationFlag &&  shake_intensity > 0){
			transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
			transform.rotation = new Quaternion(
				originRotation.x + Random.Range (-shake_intensity,shake_intensity) * .2f,
				originRotation.y + Random.Range (-shake_intensity,shake_intensity) * .2f,
				originRotation.z + Random.Range (-shake_intensity,shake_intensity) * .2f,
				originRotation.w + Random.Range (-shake_intensity,shake_intensity) * .2f);
			shake_intensity -= shake_decay;
		}else if (animationFlag && shake_intensity <= 0) {
			this.transform.rotation = Quaternion.LookRotation(Vector3.left);
			animationFlag = false;
			shake_decay = 0.005f;
			shake_intensity = 0.3f;
		}

		check_crystalPosition ();
		check_ray_four_Position ();
		check_rayAnimation_Position ();
		check_glassNoMove_Position ();
		i++;

		if (i % 50 == 0) {
			clearCrystals ();
			clearRayfour();
			clearRayAnimation();
			clearGlassNoMove();
		}
	}

	void clearCrystals(){
		if (currentIndex_crystal > clearIndex_crystal) {
			float cameraX = this.transform.position.x;
			for(int i=clearIndex_crystal;i<currentIndex_crystal;i++){
				Vector3 temp = list_crystal[i];
				if(temp.x - notSeenDistance > cameraX){
					Destroy(crystal_list[i].gameObject);
					clearIndex_crystal++;
				}
			}
		}
	}

	void check_crystalPosition(){
		if (list_crystal.Count > currentIndex_crystal) {
			float cameraX = this.transform.position.x;
			Vector3 temp = list_crystal[currentIndex_crystal];
			if (temp.x + distance >= cameraX) {
				crystal_list.Add ((GameObject)Instantiate(prefab_crystal,list_crystal[currentIndex_crystal],Quaternion.identity));
				currentIndex_crystal++;
			}
		}
	}

	void clearRayfour(){
		if (currentIndex_ray_four > clearIndex_ray_four) {
			float cameraX = this.transform.position.x;
			for(int i=clearIndex_ray_four;i<currentIndex_ray_four;i++){
				Vector3 temp = list_ray_four[i];
				if(temp.x - notSeenDistance > cameraX){
					if(ray_four_list[i].gameObject!=null){
						Destroy(ray_four_list[i].gameObject);
						clearIndex_ray_four++;
					}
				}
			}
		}
	}
	
	void check_ray_four_Position(){
		if (list_ray_four.Count > currentIndex_ray_four) {
			float cameraX = this.transform.position.x;
			Vector3 temp = list_ray_four[currentIndex_ray_four];
			if (temp.x + 20 >= cameraX) {
				ray_four_list.Add ((GameObject)Instantiate(prefab_ray_four,list_ray_four[currentIndex_ray_four],this.transform.rotation));
				currentIndex_ray_four++;
			}
		}
	}

	void clearRayAnimation(){
		if (currentIndex_rayAnimation > clearIndex_rayAnimation) {
			float cameraX = this.transform.position.x;
			for(int i=clearIndex_rayAnimation;i<currentIndex_rayAnimation;i++){
				Vector3 temp = list_rayAnimation[i];
				if(temp.x - notSeenDistance > cameraX){
					if(rayAnimation_list[i].gameObject!=null){
						Destroy(rayAnimation_list[i].gameObject);
						clearIndex_rayAnimation++;
					}
				}
			}
		}
	}
	
	void check_rayAnimation_Position(){
		if (list_rayAnimation.Count > currentIndex_rayAnimation) {
			float cameraX = this.transform.position.x;
			Vector3 temp = list_rayAnimation[currentIndex_rayAnimation];
			if (temp.x + 20 >= cameraX) {
				rayAnimation_list.Add ((GameObject)Instantiate(prefab_rayAnimation,list_rayAnimation[currentIndex_rayAnimation],this.transform.rotation));
				currentIndex_rayAnimation++;
			}
		}
	}

	
	void clearGlassNoMove(){
		if (currentIndex_glassNoMove > clearIndex_glassNoMove) {
			float cameraX = this.transform.position.x;
			for(int i=clearIndex_glassNoMove;i<currentIndex_glassNoMove;i++){
				Vector3 temp = list_glassNoMove[i];
				if(temp.x - notSeenDistance > cameraX){
					if(glassNoMove_list[i].gameObject!=null){
						Destroy(glassNoMove_list[i].gameObject);
						clearIndex_glassNoMove++;
					}
				}
			}
		}
	}
	
	void check_glassNoMove_Position(){
		if (list_glassNoMove.Count > currentIndex_glassNoMove) {
			float cameraX = this.transform.position.x;
			Vector3 temp = list_glassNoMove[currentIndex_glassNoMove];
			if (temp.x + 20 >= cameraX) {
				glassNoMove_list.Add ((GameObject)Instantiate(prefab_glassNoMove,list_glassNoMove[currentIndex_glassNoMove],this.transform.rotation));
				currentIndex_glassNoMove++;
			}
		}
	}
	
	void getVector3Position (List<Vector3>list_temp,TextAsset asset){
		string[] strs =asset.text.Split (new char[]{'\n'});
		for (int i=0; i<strs.Length; i++) {
			string[] temp = strs[i].Split(',');
			float x = float.Parse(temp[0]);
			float y = float.Parse(temp[1]);
			float z = float.Parse(temp[2]);
			list_temp.Add(new Vector3(x,y,z));
		}
			
	}

	void Shake(){
		originPosition = transform.position;
		originRotation = transform.rotation;
		animationFlag = true;
	}

}