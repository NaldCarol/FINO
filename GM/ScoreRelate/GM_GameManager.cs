using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM_GameManager : MonoBehaviour {
	public static int[] rescueNPCNum = { 0, 0, 0, 0, 0 };
	public static int[] everyChUseTime = { 0, 0, 0, 0, 0 };
	public static char[] rank = { 'N', 'N', 'N', 'N', 'N' };
	public float timeChecker;

	private int curSceneNum;
	public GameObject player;

	// Use this for initialization
	void Start () {
		curSceneNum = SceneManager.GetActiveScene ().buildIndex - 4;
		player = GameObject.Find ("myPlayer");
		if (curSceneNum >= 0) {
			SaveScore ();
		}
	}

	void Update () {
		timeChecker = Time.time;
	}

	public int SaveScore () {
		rescueNPCNum [curSceneNum] = player.GetComponent<onMyController> ().myEmployeeCounter;
		return rescueNPCNum [curSceneNum];
//		for (int i = 0; i <5; i++) {
//			Debug.Log ("SceneNum" + i +"NPC" + rescueNPCNum[i].ToString());
//		}
	}

	public int ScoreReader (int chNum) {
		return rescueNPCNum [chNum];
	}

	public int ReadTime () {
		everyChUseTime [curSceneNum] = Mathf.RoundToInt (timeChecker / 60);
		return everyChUseTime [curSceneNum];
	}

	public int TimeReader (int chNum) {
		return everyChUseTime [chNum];
	}

	public char RankCounter (int npcInt, int timeInt) {
		if (timeInt > 30) {
			if (npcInt >= 3) {
				rank [curSceneNum] = 'C';
			} else {
				rank [curSceneNum] = 'E';
			}
		} else if (timeInt <= 30 && timeInt > 20) {
			if (npcInt >= 3) {
				rank [curSceneNum] = 'B';
			} else if (npcInt < 3 && npcInt >= 2) {
				rank [curSceneNum] = 'C';
			} else {
				rank [curSceneNum] = 'E';
			}
		} else if (timeInt <= 20 && timeInt > 10) {
			if (npcInt >= 3) {
				rank [curSceneNum] = 'A';
			} else if (npcInt < 3 && npcInt >= 2) {
				rank [curSceneNum] = 'B';
			} else if (npcInt < 2 && npcInt >= 1) {
				rank [curSceneNum] = 'C';
			} else {
				rank [curSceneNum] = 'E';
			}
		} else if (timeInt <= 10) {
			if (npcInt >= 3) {
				rank [curSceneNum] = 'S';
			} else if (npcInt < 3 && npcInt >= 2) {
				rank [curSceneNum] = 'A';
			} else if (npcInt < 2 && npcInt >= 1) {
				rank [curSceneNum] = 'B';
			} else {
				rank [curSceneNum] = 'C';
			}
		} else {
			rank [curSceneNum] = 'N';
		}
		return rank [curSceneNum];
	}

	public char RankReader(int chNum){
		return rank [chNum];
	}
}