using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_TogLog : MonoBehaviour {

	void OnTriggerEnter(Collider item){
		if (item.gameObject.tag == "Rock"|| item.gameObject.tag == "Float") {
			GM_UIManager.Instance.ShowPanel ("HeadPanel_Stone");
		} 
		if (item.gameObject.tag == "Mystery") {
			GM_UIManager.Instance.ShowPanel ("HeadPanel_Mys");
		} 
		if (Input.GetKey (KeyCode.Z)) {
			GM_UIManager.Instance.ClosePanel ("HeadPanel_Stone");
		}
		if ( Input.GetKey (KeyCode.X)) {
			GM_UIManager.Instance.ClosePanel ("HeadPanel_Mys");
		}
	}

	void OnTriggerExit(){
		GM_UIManager.Instance.CloseAllPanel ();
	}
}
