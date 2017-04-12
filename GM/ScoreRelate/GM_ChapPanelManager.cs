using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM_ChapPanelManager : MonoBehaviour {
	public GameObject chX_Panel;
	public Text npcNumTxt;
	public Text totalTimeTxt;
	public Text rankTxt;
	public int ch_ID;
	public int npcNum;
	public int totalTimeNum;
	public char rankChar;

	void Start () {
		string ch_name = this.name;
		switch (ch_name) {
		case "Ch0":
			ch_ID = 0;
			break;
		case "Ch1":
			ch_ID = 1;
			break;
		case "Ch2":
			ch_ID = 2;
			break;
		case "Ch3":
			ch_ID = 3;
			break;
		case "Ch4":
			ch_ID = 4;
			break;
		default:
			break;
		}
	}

	public void OpenPanel () {
		chX_Panel.SetActive (true);
		npcNum = GameObject.Find ("GameManager").GetComponent<GM_GameManager> ().ScoreReader (ch_ID);
		npcNumTxt.text = "營救NPC人數：" + npcNum.ToString () + "人";
		totalTimeNum = GameObject.Find ("GameManager").GetComponent<GM_GameManager> ().TimeReader (ch_ID);
		totalTimeTxt.text = "完成任務時間：" + totalTimeNum.ToString () + "分";
		rankChar = GameObject.Find ("GameManager").GetComponent<GM_GameManager> ().RankReader (ch_ID);
		rankTxt.text = "Rank："+rankChar.ToString ();
	}

	public void ShutDownPanel () {
		chX_Panel.SetActive (false);

	}
}
