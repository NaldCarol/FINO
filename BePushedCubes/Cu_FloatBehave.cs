using UnityEngine;
using System.Collections;

public class Cu_FloatBehave : MonoBehaviour {
	#region 宣告所需地圖位置變數

	public GameObject mapCurCube;
	public int needMapX;
	public int mapCurY;
	public int needMapZ;
	public Ch0_MapDataArray mapData;
	#endregion

	private Vector3 velocity = Vector3.zero;

	public RaycastHit hit;
	public Ray underHasCube;
	public bool moveToTar;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		MapCubeReader ();
		if (transform.position.y != mapCurY) {
			Debug.Log ("transform.position.y != mapCurY");
			if (mapCurY < 0) {
				transform.position = Vector3.SmoothDamp (transform.position, new Vector3(transform.position.x,0f,
					transform.position.z), ref velocity, 0.001f);
				mapCurCube.transform.position=new Vector3(needMapX,0,needMapZ);
				mapCurCube.GetComponent<Ch0_MapDataArray> ().CommonCubeDataTurner ();
			} else {
				MoveToTarget ();
			}
		} 

	}

	void MapCubeReader ()	{
		#region 賦予PP所需要用來偵測的Cube參數數值，
		needMapX = Mathf.RoundToInt (this.transform.position.x);
		needMapZ = Mathf.RoundToInt (this.transform.position.z);
		mapCurCube = GameObject.Find ("Floor.Id(" + needMapX.ToString () + "," + needMapZ.ToString () + ")");
		mapCurY = Mathf.RoundToInt (mapCurCube.gameObject.transform.position.y);
		#endregion
	}

	public bool MoveToTarget (){
		moveToTar = false;
		transform.position = Vector3.SmoothDamp (transform.position, new Vector3 (transform.position.x, mapCurY, 	transform.position.z), ref velocity, 0.1f);	
		moveToTar = true;
		return moveToTar;
	}
}
