using UnityEngine;
using System.Collections;

public class Ch_HeroFollowPP : MonoBehaviour {
	public Transform target;
	public float slowSmoothTime = 0.1F;
	public float fastSmoothTime = 0.01F;
	private Vector3 velocity = Vector3.zero;
	public bool slowSmoothSwitch = true;
	public Animator fanyaAniCtrl;
	public bool backToIdle = false;

	public bool getBrocastfromBePush = false;
	public GameObject curhitFromPP;


	//	public RaycastHit hit;
	//	public Ray detectFloor;

	public bool canJump;
	public bool isFollow;

	// Use this for initialization
	void Start () {
		fanyaAniCtrl = GetComponent<Animator> ();
		backToIdle = false;
	}

	// Update is called once per frame
	void Update () {
		slowSmoothSwitch = target.GetComponent<PP_PlayerPointBehave> ().isJump;
		Vector3 targetPosition = target.TransformPoint (new Vector3 (0, 0.5f, 0));

		if (slowSmoothSwitch == true) {
			if (getBrocastfromBePush) {
				#region 拉動物體時，先執行此動作，再重製地圖狀態
				curhitFromPP = target.GetComponent<PP_PlayerPointBehave> ().curHit;
				transform.forward = target.forward;
				Debug.Log (transform.forward + "/" + target.forward + "/" + (-target.forward));
				transform.position = Vector3.SmoothDamp (transform.position, targetPosition, 
					ref velocity, 0.001f);
				curhitFromPP.GetComponent<Cu_BePushedBehave> ().getBrocastfromFollowpp = true;
				getBrocastfromBePush = false;
				#endregion
			} else {
				transform.forward = target.forward;
				if (!Input.GetKey (KeyCode.Z)) {
					fanyaAniCtrl.SetBool ("isWalk", true);
				}
				transform.position = Vector3.SmoothDamp (transform.position, targetPosition, ref velocity, slowSmoothTime);
			}

			//原地靜止
			if (Mathf.RoundToInt (this.transform.position.x) == Mathf.RoundToInt (target.transform.position.x) && Mathf.RoundToInt (this.transform.position.z) == Mathf.RoundToInt (target.transform.position.z)) {
				fanyaAniCtrl.SetBool ("isWalk", false);
			}
		} else {
			if (canJump) {
				transform.position = Vector3.SmoothDamp (transform.position, transform.position + new Vector3 (0, 0.3f, 0), ref velocity, fastSmoothTime);
			} 
			//transform.position = Vector3.SmoothDamp (transform.position, targetPosition, ref velocity, fastSmoothTime);
		}
			
		if (!Input.GetKey (KeyCode.Z)) {
			fanyaAniCtrl.SetBool ("isPush", false);
			fanyaAniCtrl.SetBool ("isPull", false);
		} 
	
	}

	public void PushAni () {
		fanyaAniCtrl.SetBool ("isPush", true);
		fanyaAniCtrl.SetBool ("isWalk", false);
	}

	public void PullAni () {
		fanyaAniCtrl.SetBool ("isPull", true);
		fanyaAniCtrl.SetBool ("isWalk", false);
	}

	public void OnCollisionEnter (Collision collision) {
		if (collision.collider.name != "UIShowArea") {
			canJump = true;
		} else {
			canJump = false;
		}
	}

	public void OnCollisionExit (Collision collision) {
		canJump = false;
	}
}
