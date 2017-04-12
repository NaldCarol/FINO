using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GM_UIManager : Singleton<GM_UIManager> {

	private string panelRoot = "Prefebs/UGUI/";
	public GameObject sceneCanvasRoot;
	public Dictionary<string,GameObject> panelList = new Dictionary<string, GameObject> ();

	private bool CheckCanvasRootIsNull () {
		if (sceneCanvasRoot == null) {
			Debug.LogError ("Canvas need RootHandler.cs");
			return true;
		} else {
			return false;
		}
	}

	private bool IsPanelLife (string name) {
		return panelList.ContainsKey (name);
	}

	public GameObject ShowPanel (string name) {
		if (CheckCanvasRootIsNull ()) {
			return null;
		}
		if (IsPanelLife (name)) {
			Debug.LogErrorFormat ("[{0}]is open", name);
			return null;
		}
		GameObject loadGameObj = Utility.AssetRelate.ResourcesLoadCheckNull<GameObject> (panelRoot + name);
		GameObject panelTemp = Utility.GameObjectRelate.InstantiateGameObject (sceneCanvasRoot, loadGameObj);
		panelTemp.name = name;
		panelList.Add (name, panelTemp);
		return panelTemp;
	}

	public void TogglePanel (string name, bool isOn) {
		if (IsPanelLife (name)) {
			if (panelList [name] != null) {
				panelList [name].SetActive (isOn);
			}
		} else {
			Debug.LogErrorFormat ("[{0}]not found", name);
		}
	}

	public void ClosePanel (string name) {
		if (IsPanelLife (name)) {
			if (panelList [name] != null) {
				Object.Destroy (panelList [name]);
			}
			panelList.Remove (name);
		} else {
			Debug.LogErrorFormat ("[{0}]not found", name);
		}
	}

	public void CloseAllPanel () {
		foreach (KeyValuePair<string, GameObject> item in panelList)
		{
			if (item.Value != null)
				Object.Destroy(item.Value);
		}

		panelList.Clear();
	}

	public Vector2 GetCanvasSize () {
		if (CheckCanvasRootIsNull())
			return Vector2.one * -1;

		RectTransform trans = sceneCanvasRoot.transform as RectTransform;

		return trans.sizeDelta;
	}
}
