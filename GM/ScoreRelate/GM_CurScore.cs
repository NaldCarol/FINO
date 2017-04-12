using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GM_CurScore : MonoBehaviour {
	public Text npcNumTxt;
	public Text timeTxt;
	public int npcNumInt;
	public int totalTimeInt;
	private GameObject GM;

	public void ShowInfo () {
		GM = GameObject.Find ("GameManager");
		npcNumInt = GM.GetComponent<GM_GameManager> ().SaveScore ();
		totalTimeInt =GM.GetComponent<GM_GameManager> ().ReadTime ();
		npcNumTxt.text = "營救NPC人數：" + npcNumInt.ToString () + "人";
		timeTxt.text = "完成任務時間：" + totalTimeInt.ToString () + "分";
	}
}
