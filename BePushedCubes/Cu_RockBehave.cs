using UnityEngine;
using System.Collections;

public class Cu_RockBehave : MonoBehaviour {
	#region 宣告所需地圖位置變數

	public GameObject mapCurCube;
	public int needMapX;
	public int mapCurY;
	public int needMapZ;
	public Ch0_MapDataArray mapData;

	#endregion

	private Vector3 velocity = Vector3.zero;

	public RaycastHit hit;
	public Ray upHasCube;

	// Use this for initialization
	void Start () {
		DetectFloor ();
	}
	
	// Update is called once per frame
	void Update () {
		DetectFloor ();
	}

	void MapCubeReader () {
		#region 賦予PP所需要用來偵測的Cube參數數值，
		needMapX = Mathf.RoundToInt (this.transform.position.x);
		needMapZ = Mathf.RoundToInt (this.transform.position.z);
		mapCurCube = GameObject.Find ("Floor.Id(" + needMapX.ToString () + "," + needMapZ.ToString () + ")");
		mapCurY = Mathf.RoundToInt (mapCurCube.gameObject.transform.position.y);
		#endregion
	}

	public void DetectFloor () {
		MapCubeReader ();
		upHasCube = new Ray (transform.position + new Vector3 (0, 0, 0), transform.up);
		//Debug.DrawRay (transform.position + new Vector3 (0, 0, 0), transform.up, Color.blue, 1f);
		if (!Physics.Raycast (upHasCube, out hit, 1f)) {
			if (transform.position.y != mapCurY) {
				if (mapCurY < 0) {
					transform.position = Vector3.SmoothDamp (transform.position, new Vector3 (transform.position.x, mapCurY, transform.position.z), ref velocity, 0.01f);
				} else {			
						transform.position = Vector3.SmoothDamp (transform.position, new Vector3 (transform.position.x, mapCurY, transform.position.z), ref velocity, 0.1f);	
				}
			} 
		}
	}
}
