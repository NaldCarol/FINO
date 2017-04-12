using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_CanvasManager : MonoBehaviour {

	private GameObject curScorePanel;


	public void SettingDis () {
		GM_UIManager.Instance.ShowPanel ("SettingPanel");
	}

	public void CurScDis () {
		GM_UIManager.Instance.ShowPanel ("CurScorePanel");
		curScorePanel = GameObject.Find ("CurScorePanel");
		curScorePanel.GetComponent<GM_CurScore> ().ShowInfo ();
	}

	public void MisCom () {
		GM_UIManager.Instance.ShowPanel ("ScorePanel");
	}

	public void CloseSettingDis () {
		GM_UIManager.Instance.ClosePanel ("SettingPanel");
		}

	public void CloseCurScDis () {
		GM_UIManager.Instance.ClosePanel ("CurScorePanel");
	}

	public void CloseMisCom () {
		GM_UIManager.Instance.ClosePanel ("ScorePanel");
	}

	public void CloseManDia(){
		GM_UIManager.Instance.ClosePanel ("ManDiaPanel");
	}

	public void CloseBoyDia(){
		GM_UIManager.Instance.ClosePanel ("BoyDiaPanel");
	}

	public void CloseGirlDia(){
		GM_UIManager.Instance.ClosePanel ("GirlDiaPanel");
	}

	public void CloseOldLadyDia(){
		GM_UIManager.Instance.ClosePanel ("OldLadyDiaPanel");
	}
}
