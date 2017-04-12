using UnityEngine;
using System.Collections;

public class Ch_NPCFollowHero : MonoBehaviour {

	public Vector3 npcStandPos;
	public GameObject player;
	public GameObject emptyPlayer;
	public bool canFollowHero;

	#region 主角平滑跟隨
	public bool slowSmooth = false;
	#endregion

	// Use this for initialization
	void Start () {
		canFollowHero = false;
	}

	// Update is called once per frame
	void Update () {
		if (canFollowHero == true) {
			CanFollowHero ();
		}
	}

	void OnTriggerEnter(){
		if (player.CompareTag ("Player")) {
			canFollowHero = true;
		}
	}
	void CanFollowHero(){
		slowSmooth = true;
		npcStandPos = emptyPlayer.GetComponent<PP_PlayerPointBehave> ().heroPre;
		this.transform.position = npcStandPos+new Vector3(0,0,0);
	}
}
