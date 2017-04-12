using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cu_BePushedBehave : MonoBehaviour {

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

	#region 格點移動測試

	private float moveSpeed = 1f;
	private float gridSize = 1f;

	private Vector2 input;
	private bool isMoving = false;
	private Vector3 startPosition;
	private Vector3 endPosition;
	private float t;
	private float factor = 1f;

	#endregion


	// Use this for initialization
	void Start () {
		plp = GameObject.Find ("HeroPoint");
		faynaDoll = GameObject.Find ("FANYA4");
		curHeroFwd_Cu = plp.GetComponent<PP_PlayerPointBehave> ().curHeroFwd_PP;
	}


	public void BePushed () {
		curHeroFwd_Cu = plp.GetComponent<PP_PlayerPointBehave> ().curHeroFwd_PP;

		if (!isMoving) {
			input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
			if (Mathf.Abs (input.x) > Mathf.Abs (input.y)) {
				input.y = 0;
			} else {
				input.x = 0;
			}
			if (input != Vector2.zero) {
				pushWay = new Vector3 (input.x, 0, input.y);
				CheckNearBy ();
				//Debug.Log (CheckNearBy ());
				if (CheckNearBy () == 0) {
					if (pushWay == curHeroFwd_Cu) {
						faynaDoll.GetComponent<Ch_HeroFollowPP> ().PushAni ();
						changeCuAndMapData ();
					} else if (pushWay == -curHeroFwd_Cu) {
						faynaDoll.GetComponent<Ch_HeroFollowPP> ().PullAni ();
						if (CheckBehindHero () == 0) {
							plp.GetComponent<PP_PlayerPointBehave> ().DetectBack (pushWay);
							if (!plp.GetComponent<PP_PlayerPointBehave> ().DetectBack (pushWay)) {
								faynaDoll.GetComponent<Ch_HeroFollowPP> ().getBrocastfromBePush = true;

								if (getBrocastfromFollowpp) {
									changeCuAndMapData ();
									getBrocastfromFollowpp = false;
								}
							}
						}
					}
				}
				if (this.tag == "Mystery") {
					//Debug.Log (this.tag.ToString());
					this.GetComponent<Cu_RockDataArray> ().MysteryCubeDataTurner ();
				}
			} 
		}
	}

	public void changeCuAndMapData () {
		Debug.Log ("enter changeCuAndMapData");
		changeCurMapCuData = this.transform.position;
		changeNextMapCuData = this.transform.position + pushWay;
		StartCoroutine (move (transform));
		changeCurMapData = GameObject.Find ("Floor.Id(" + changeCurMapCuData.x.ToString () + "," + changeCurMapCuData.z.ToString () + ")");
		changeNextMapData = GameObject.Find ("Floor.Id(" + changeNextMapCuData.x.ToString () + "," + changeNextMapCuData.z.ToString () + ")");
		changeCurMapData.transform.position -= new Vector3 (0, 1.0f, 0);
		changeNextMapData.transform.position += new Vector3 (0, 1.0f, 0);
	}

	public IEnumerator move (Transform transform) {
		isMoving = true;
		startPosition = transform.position;
		t = 0;
		endPosition = new Vector3 (startPosition.x + System.Math.Sign (input.x) * gridSize,	
			startPosition.y, startPosition.z + System.Math.Sign (input.y) * gridSize);

		while (t < 1f) {			
			t += Time.deltaTime * (moveSpeed / gridSize) * factor;
			transform.position = Vector3.Lerp (startPosition, endPosition, t);
			yield return null;
		}

		isMoving = false;
		yield return 0;
	}

	public int CheckNearBy () {
		RaycastHit hit;
		Ray detectUpCube = new Ray (transform.position + new Vector3 (0, 0, 0), transform.up);
		Ray detectFrontCube = new Ray (transform.position + new Vector3 (0, 0, 0), pushWay);
		int upHasCube = 0;
		int frontHasItem = 0;
		if ((Physics.Raycast (detectUpCube, out hit, 1f))) {
			Debug.Log (hit.transform.name);
			upHasCube = 1;
		} else {
			upHasCube = 0;
		}
		if (Physics.Raycast (detectFrontCube, out hit, 1f)) {
			if (hit.transform.tag == "Player" || hit.transform.name == "boy_NPCpoint") {
				frontHasItem = 0;
			} else {
				Debug.Log (hit.transform.name);
				frontHasItem = 1;
			}
		} else {
			frontHasItem = 0;
		}

		return upHasCube + frontHasItem;
	}

	public int CheckBehindHero () {
		RaycastHit hit;
		Ray detectBehindHero = new Ray (plp.transform.position + new Vector3 (0, 1f, 0), pushWay);
		int behindHeroHasItem = 0;
		if (Physics.Raycast (detectBehindHero, out hit, 1f)) {
			if (hit.transform.name == "boy_NPCpoint") {
				behindHeroHasItem = 0;
			} else {
				Debug.Log (hit.transform.name);
				behindHeroHasItem = 1;
			}
		} else {
			behindHeroHasItem = 0;
		}
		return behindHeroHasItem;

	}
}
