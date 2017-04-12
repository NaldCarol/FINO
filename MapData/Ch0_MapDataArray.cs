using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Ch0_MapDataArray : MonoBehaviour {

	//宣告場景中NormaFloor要給2D陣列使用的所有數值
	public GameObject currentCube;
	public int currentX;
	public int currentY;
	public int currentZ;
	//地圖數據存放用陣列(2D)，結果為y軸值，邊界各+2為透明牆
	public int[,] mapCh0FloorY=new int[22,35];
	public int[,] mapCh1FloorY=new int[19,55];
	public int[,] mapCh2FloorY=new int[23,50];
	public int[,] mapCh3FloorY=new int[23,42];
	public int[,] mapCh4FloorY=new int[19,35];
	public string sceneSwitcher;
	public Scene curScene;

	public bool resetMapData=false;

	void Awake(){
		curScene= SceneManager.GetActiveScene();
		sceneSwitcher = curScene.name.ToString ();
		//再載入場景的同時，將分屬不同地圖的陣列載入currentY，而不會造成無參照狀態
		switch (sceneSwitcher) {
		case "Ch0_Mission":
			mapCh0FloorY [currentX, currentZ] = currentY;
			break;
		case "Ch1_Mission":
			mapCh1FloorY[currentX, currentZ] = currentY;
			break;
		case "Ch2_Mission":
			mapCh2FloorY[currentX, currentZ] = currentY;
			break;
		case "Ch3_Mission":
			mapCh3FloorY[currentX, currentZ] = currentY;
			break;
		case "Ch4_Mission":
			mapCh4FloorY[currentX, currentZ] = currentY;
			break;
		default:
			break;
		}
		CommonCubeDataTurner ();
	}
		
	//先將所有current相關變數指向this.gameObject，再將直回傳給不同場景分屬的陣列
	public void CommonCubeDataTurner(){
		currentCube = this.gameObject;
		currentX = Mathf.RoundToInt(this.gameObject.transform.position.x);
		currentY = Mathf.RoundToInt(this.gameObject.transform.position.y);
		currentZ = Mathf.RoundToInt(this.gameObject.transform.position.z);
		currentCube.name="Floor.Id("+currentX.ToString()+","+currentZ.ToString()+")";

	}


}
