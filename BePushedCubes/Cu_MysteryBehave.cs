using UnityEngine;
using System.Collections;

public class Cu_MysteryBehave : MonoBehaviour {
	#region 宣告所需地圖位置變數


	public Vector3[] endPos=new Vector3[4]{new Vector3(17f,3f,16f),new Vector3(15f,3f,14f),new Vector3(15f,3f,14f),new Vector3(15f,3f,14f)};
	public int[] endPosHasItem=new int[4];

	#endregion

	public GameObject gameManager;

	public void OpenEnd(){
		if (this.transform.position == endPos [0]) {
			endPosHasItem [0] = 1;
			Debug.Log ("1st holl has mys");
		} else {
			endPosHasItem [0] = 0;
		}
		if (this.transform.position == endPos [1]) {
			endPosHasItem [1] = 1;
			Debug.Log ("2nd holl has mys");
		} else {
			endPosHasItem [1] = 0;
		}
		if (this.transform.position == endPos [2]) {
			endPosHasItem [2] = 1;
			Debug.Log ("3rd holl has mys");
		} else {
			endPosHasItem [2] = 0;
		}
		if (this.transform.position == endPos [3]) {
			endPosHasItem [3] = 1;
			Debug.Log ("4th holl has mys");
		} else {
			endPosHasItem [3] = 0;
		}

	}
}

