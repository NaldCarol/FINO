using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class GM_SceneManager : MonoBehaviour {
	public GM_ChapterData chapterData;
	public Scene lastScene;
	public string sceneName;
	public GameObject loadingPanel;

	public static List<string> lastLoadScene = new List<string> ();

	void Awake () {
		if (SceneManager.GetActiveScene ().name == "Main") {
			GetActiveScene ();
		}
	}

	void Update(){
		if (Input.GetKey (KeyCode.Escape)) {
			MainTitle ();
		}
	}
	//----------Index Page----------//
	public void NewGame () {
		//sceneName = SceneManager.GetActiveScene ().name;
		LoadPanel ();
		StartCoroutine (loadingPanel.GetComponent<GM_Loading> ().DisplayLoadingScreen ("Ch0_Mission_redo"));
		chapterData.chapterNum [0] = 0;
	}

	public void LoadGame () {
		LoadPanel ();
		StartCoroutine (loadingPanel.GetComponent<GM_Loading> ().DisplayLoadingScreen ("Load"));
	}

	public void Option () {
		LoadPanel ();
		StartCoroutine (loadingPanel.GetComponent<GM_Loading> ().DisplayLoadingScreen ("Option"));
	}

	public void About () {
		LoadPanel ();
		StartCoroutine (loadingPanel.GetComponent<GM_Loading> ().DisplayLoadingScreen ("About"));
	}

	public void Exit () {
		Debug.Log ("Exit");
		Application.Quit ();
	}

	public void MainTitle () {
		LoadPanel ();
		StartCoroutine (loadingPanel.GetComponent<GM_Loading> ().DisplayLoadingScreen ("Main"));
	}

	//----------LoadGame Page----------//
	public void CH0 () {
		//sceneName = SceneManager.GetActiveScene ().name;
		LoadPanel ();
		StartCoroutine (loadingPanel.GetComponent<GM_Loading> ().DisplayLoadingScreen ("Ch0_Mission_redo"));
		chapterData.chapterNum [0] = 0;
	}

	public void CH1 () {
		//sceneName = SceneManager.GetActiveScene ().name;
		LoadPanel ();
		StartCoroutine (loadingPanel.GetComponent<GM_Loading> ().DisplayLoadingScreen ("Ch1_Mission"));
		chapterData.chapterNum [1] = 1;
	}

	public void CH2 () {
		LoadPanel ();
		StartCoroutine (loadingPanel.GetComponent<GM_Loading> ().DisplayLoadingScreen ("Ch2_Mission_redo"));
		chapterData.chapterNum [2] = 2;
	}

	public void CH3 () {
		//sceneName = SceneManager.GetActiveScene ().name;
		LoadPanel ();
		StartCoroutine (loadingPanel.GetComponent<GM_Loading> ().DisplayLoadingScreen ("Ch3_Mission_redo_NPC"));
		chapterData.chapterNum [3] = 3;
	}

	public void CH4 () {
		//sceneName = SceneManager.GetActiveScene ().name;
		LoadPanel ();
		StartCoroutine (loadingPanel.GetComponent<GM_Loading> ().DisplayLoadingScreen ("Ch4_Mission_redo"));
		chapterData.chapterNum [4] = 4;
	}

	public void Reload () {
		GM_UIManager.Instance.CloseAllPanel ();
		Scene curScene = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (curScene.name);
	}

	public void LoadPanel () {
		GetActiveScene ();
		GM_UIManager.Instance.CloseAllPanel ();
		GM_UIManager.Instance.ShowPanel ("LoadPanel");
		loadingPanel = GameObject.Find ("LoadPanel");
	}

//	public void Update () {
//		if (Input.GetKey (KeyCode.F1)) {
//			if ((lastLoadScene.Count - 2) >= 0) {
//				Debug.Log ((lastLoadScene.Count - 2) + "/" + (lastLoadScene [lastLoadScene.Count - 2].ToString ()));
//			} else {
//				Debug.Log ("This is the first page");
//			}
//		}
//	}

	public void GetActiveScene(){
		lastLoadScene.Add (SceneManager.GetActiveScene ().name);
		//Debug.Log ((lastLoadScene.Count - 1) + "/" + (lastLoadScene [lastLoadScene.Count - 1].ToString ()));
	}

	public void Back(){
		string lastSceneName = lastLoadScene [lastLoadScene.Count - 2].ToString ();
		LoadPanel ();
		StartCoroutine (loadingPanel.GetComponent<GM_Loading> ().DisplayLoadingScreen (lastSceneName));

	}
}
