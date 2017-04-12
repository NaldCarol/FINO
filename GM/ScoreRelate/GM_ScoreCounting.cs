using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GM_ScoreCounting : MonoBehaviour {
	public Text npcNumTxt;
	public Text timeTxt;
	public Text rankTxt;


	public IEnumerator DisplayNPCNum (int npcNum,int timeNum) {
		npcNumTxt.text = "營救NPC人數："+npcNum.ToString ()+"人";
		timeTxt.text = "完成任務時間："+timeNum.ToString ()+"分";
		yield return null;
	}

	public IEnumerator DisplayRank (char rank) {
		rankTxt.text = "Rank："+rank.ToString ();
		yield return null;
	}
}
