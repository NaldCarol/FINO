using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cu_MagicmoveBehave : MonoBehaviour {

	public Vector3 curHeroFwd_Cu;
	public GameObject plp;
	public GameObject faynaDoll;
	public Vector3 pushWay;

	//紀錄待改的地圖數值，用於找出實際記錄地圖告度數值的MapData座標空物件
	public Vector3 changeCurMapCuData;
	public Vector3 changeNextMapCuData;

	//抓取實際記錄地圖告度數值的MapData座標空物件
	public GameObject changeCurMapData;
	public GameObject changeNextMapData;

	public bool getBrocastfromFollowpp = false;



	// Use this for initialization
	void Start () {
		plp = GameObject.Find ("HeroPoint");
		faynaDoll = GameObject.Find ("Ch_Leader_FANYA2");
		curHeroFwd_Cu = plp.GetComponent<PP_PlayerPointBehave> ().curHeroFwd_PP;
	}

    void Update() {
        this.GetComponent<Cu_RockDataArray>().MysteryCubeDataTurner();
    }
	public void MagicMove () {
		curHeroFwd_Cu = plp.GetComponent<PP_PlayerPointBehave> ().curHeroFwd_PP;

		if (Input.anyKeyDown) {
			if (Input.GetAxisRaw ("Horizontal") != 0 && Input.GetAxisRaw ("Vertical") != 0) {
				return;
			} else if (Input.GetAxisRaw ("Horizontal") == 0 && Input.GetAxisRaw ("Vertical") == 0) {
				return;
			} else {
				if (Input.GetAxisRaw ("Horizontal") != 0 || Input.GetAxisRaw ("Vertical") != 0) {
					pushWay = new Vector3 (Input.GetAxisRaw ("Horizontal"), 1f, Input.GetAxisRaw ("Vertical"));
					this.GetComponent<Cu_RockBehave> ().DetectFloor ();
					changeCuAndMapData ();
					this.GetComponent<Cu_RockDataArray> ().MysteryCubeDataTurner ();
					}
			}
		} 

	}

	public void changeCuAndMapData () {
		Debug.Log ("enter changeCuAndMapData");
		changeCurMapCuData = this.transform.position;
		changeNextMapCuData = this.transform.position + pushWay;
		this.transform.position += pushWay;
		changeCurMapData = GameObject.Find ("Floor.Id(" + changeCurMapCuData.x.ToString () + "," + changeCurMapCuData.z.ToString () + ")");
		changeNextMapData = GameObject.Find ("Floor.Id(" + changeNextMapCuData.x.ToString () + "," + changeNextMapCuData.z.ToString () + ")");
		changeCurMapData.transform.position -= new Vector3 (0, 1.0f, 0);
		changeNextMapData.transform.position += new Vector3 (0, 1.0f, 0);
	}
		
}
