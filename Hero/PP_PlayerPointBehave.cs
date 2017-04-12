using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class PP_PlayerPointBehave : MonoBehaviour {

	public Vector3 targetDirection = Vector3.zero;
	private Vector3 targetMove = Vector3.zero;

	#region 格點移動測試

	private float moveSpeed = 3f;
	private float gridSize = 1f;

	private Vector2 input;
	private bool isMoving = false;
	private Vector3 startPosition;
	private Vector3 endPosition;
	private float t;
	private float factor = 1f;

	#endregion

	#region 宣告所需地圖位置變數

	public GameObject mapCurCube;
	public int needMapX;
	public int mapCurY;
	public int needMapZ;
	public Ch0_MapDataArray mapData;
	public bool pp_ResetMapData = false;
	public int needArrayLength;

	#endregion

	#region 宣告所需地圖位置變數

	public GameObject nowCube;
	public int needNowX;
	public int nowY;
	public int needNowZ;

	#endregion

	#region 宣告所需Rock位置變數

	public Cu_RockDataArray rockData;
	public RaycastHit hit;
	public Ray detectOtherCube;
	public Cu_BePushedBehave functionPush;
	public Vector3 curHeroFwd_PP;

	#endregion

	//宣告主角上一步位置變數
	public Vector3 heroPre;
	public Vector3 heroPreDir;

	//主角平滑跟隨
	public bool isJump = true;

	public Animator fanyaAniCtrl;
	public GameObject fanya;

	public GameObject curHit;

	public GameObject EndStone;

	public GameObject magicCube;

	// Use this for initialization
	void Start () {
		heroPre = this.transform.position;
		fanyaAniCtrl = fanya.GetComponent<Animator> ();

		//先賦予PP所需要用來偵測的Cube參數初值，避免一開始的跳躍不正確
		MapCubeReader ();

		functionPush = GetComponent<Cu_BePushedBehave> ();
		EndStone = GameObject.Find ("End_StoneTablet");
	}
	
	// Update is called once per frame
	void Update () {
		
		#region 跳躍，強制加上Vector3做移動控制，重力另外控制
		if (Input.GetKeyDown (KeyCode.Space)) {
			fanyaAniCtrl.SetBool ("isJump", true);
			isJump = false;
			this.transform.position += new Vector3 (0, 1f, 0);
			//Debug.Log ("transform.position.y:" + transform.position.y + "CurY:" + NowGroundReader ());
		} else {
			isJump = true;
			fanyaAniCtrl.SetBool ("isJump", false);
		}
		#endregion

		#region 推拉
		if (Input.GetKey (KeyCode.Z)) {
			detectOtherCube = new Ray (transform.position + new Vector3 (0, 1f, 0), transform.forward);
			Debug.DrawRay (transform.position + new Vector3 (0, 1f, 0), transform.forward, Color.red, 1f);
			if (Physics.Raycast (detectOtherCube, out hit, 1f)) {
				fanyaAniCtrl.SetBool ("isWalk", false);
				//Debug.Log ("射中物體");
				if (hit.collider.tag == "Rock" || hit.collider.tag == "Mystery") {					
					curHit = GameObject.Find (hit.transform.name);
					curHeroFwd_PP = this.transform.forward;
					curHit.GetComponent<Cu_BePushedBehave> ().BePushed ();
				}
				if (hit.collider.tag == "Float") {					
					if (hit.collider.GetComponent<Cu_FloatBehave> ().MoveToTarget ()) {
						//Debug.Log(hit.collider.GetComponent<Cu_FloatBehave> ().MoveToTarget ().ToString());
						curHit = GameObject.Find (hit.transform.name);
						curHeroFwd_PP = this.transform.forward;
						curHit.GetComponent<Cu_BePushedBehave> ().BePushed ();
					}
				}
				if (hit.collider.name == "End_StoneTablet") {
					GM_UIManager.Instance.ShowPanel ("ScorePanel");
				}
			}
		} 
			
		if (Input.GetKey (KeyCode.X)) {
			needMapX = Mathf.RoundToInt (this.transform.position.x + targetDirection.x);
			needMapZ = Mathf.RoundToInt (this.transform.position.z + targetDirection.z);
			magicCube = GameObject.Find ("Mystery(" + needMapX.ToString () + "," + needMapZ.ToString () + ")");
			Debug.Log (magicCube.name);
			if (magicCube != null) {
				curHeroFwd_PP = this.transform.forward;
				magicCube.GetComponent<Cu_MagicmoveBehave> ().MagicMove ();
			}
		} 
		#endregion
		if (!Input.GetKey (KeyCode.Z)) {
			if (!Input.GetKey (KeyCode.X)) {
				moveSpeed = 3f;
			}
		} else {
			moveSpeed = 1f;
		}

		#region 專攻移動
		if (!isMoving) {
			input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
			if (Mathf.Abs (input.x) > Mathf.Abs (input.y)) {
				input.y = 0;
			} else {
				input.x = 0;
			}
			if (input != Vector2.zero) {
				heroPreDir = this.transform.forward;
				if (!Input.GetKey (KeyCode.Z)) {					
					targetDirection = new Vector3 (input.x, 0, input.y);
					this.transform.forward = targetDirection;
				}
				MapCubeReader ();
				int switcher;
				switcher = mapCurY - Mathf.RoundToInt (this.transform.position.y);
				switch (switcher) {
				case 0:
				case -1:
					StartCoroutine (canMove ());
					break;
				case 1:
				default:
					StopAllCoroutines ();
					break;
				}
			} else {
				transform.forward = heroPreDir;
			}
		}
		#endregion		

		#region 自製重力
		if (transform.position.y > NowGroundReader ()) {
			if ((transform.position.y - NowGroundReader ()) <= 1 && (transform.position.y - NowGroundReader ()) >= -1) {
				transform.position = new Vector3 (needNowX, Mathf.Lerp (transform.position.y, NowGroundReader (), 1f), needNowZ);
			}
		} else {
			return;
		}
		#endregion


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

	public IEnumerator canMove () {
		detectOtherCube = new Ray (transform.position + new Vector3 (0, 1f, 0), transform.forward);
		if (Input.GetKey (KeyCode.Z) || Input.GetKey (KeyCode.X)) {
			if (hit.collider == null) {
				yield return null;
			} else if (hit.collider.tag == "Rock" || hit.collider.tag == "Float" || hit.collider.tag == "Mystery") {
				yield return StartCoroutine (move (transform));
			} else {
				yield return null;
			}
		} else {
			yield return StartCoroutine (move (transform));
		}
	}

	void MapCubeReader () {
		#region 賦予PP所需要用來偵測的Cube參數數值
		needMapX = Mathf.RoundToInt (this.transform.position.x + targetDirection.x);
		needMapZ = Mathf.RoundToInt (this.transform.position.z + targetDirection.z);
		mapCurCube = GameObject.Find ("Floor.Id(" + needMapX.ToString () + "," + needMapZ.ToString () + ")");
		mapCurY = Mathf.RoundToInt (mapCurCube.gameObject.transform.position.y);
		#endregion
	}

	int NowGroundReader () {
		#region 賦予PP所需要用來偵測的Cube參數數值
		needNowX = Mathf.RoundToInt (this.transform.position.x);
		needNowZ = Mathf.RoundToInt (this.transform.position.z);
		nowCube = GameObject.Find ("Floor.Id(" + needNowX.ToString () + "," + needNowZ.ToString () + ")");
		nowY = Mathf.RoundToInt (nowCube.gameObject.transform.position.y);
		return nowY;
		#endregion
	}

	public bool DetectBack (Vector3 back) {
		int needBackMapX = Mathf.RoundToInt (this.transform.position.x + back.x);
		int needBackMapZ = Mathf.RoundToInt (this.transform.position.z + back.z);
		GameObject	backCurCube = GameObject.Find ("Floor.Id(" + needBackMapX.ToString () + "," + needBackMapZ.ToString () + ")");
		int backCurY = Mathf.RoundToInt (backCurCube.gameObject.transform.position.y);
		if (backCurY - Mathf.RoundToInt (this.transform.position.y) >= 0 && backCurY - Mathf.RoundToInt (this.transform.position.y) <= 1) {
			return false;
		} else {
			return true;
		}
	}

}
