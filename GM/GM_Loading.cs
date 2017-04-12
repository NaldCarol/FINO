using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GM_Loading : MonoBehaviour {

	public Text loadingRate;

	public IEnumerator DisplayLoadingScreen (string sceneName) {
		AsyncOperation loadLV = SceneManager.LoadSceneAsync (sceneName);
		while (!loadLV.isDone) {
			loadingRate.text = (loadLV.progress * 100).ToString ("0.0") + "%";
			yield return null;
		}
	}
}
