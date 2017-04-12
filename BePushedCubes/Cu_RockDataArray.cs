using UnityEngine;
using System.Collections;

public class Cu_RockDataArray : MonoBehaviour{

	//宣告場景中NormaFloor要給2D陣列使用的所有數值
	public GameObject currentCube;
	public int currentX;
	public int currentY;
	public int currentZ;
	//地圖數據存放用陣列(2D)，結果為y軸值
	public GameObject[] Rock;
	public GameObject[] FloatRock;
	public GameObject[] Mystery;

	void Awake (){
		Rock = GameObject.FindGameObjectsWithTag ("Rock");
		FloatRock = GameObject.FindGameObjectsWithTag ("Float");
		Mystery = GameObject.FindGameObjectsWithTag ("Mystery");
		if (this.tag == "Rock" || this.tag == "Float") {
			CommonCubeDataTurner ();
			}
		if (this.tag == "Mystery") {
			MysteryCubeDataTurner ();
		}
	}
    void Update()
    {
       // CommonCubeDataTurner();
      //  DetectFloor();
    }

    //將所有current相關變數指向this.gameObject
    public void CommonCubeDataTurner ()	{
		currentCube = this.gameObject;
        /*currentX = Mathf.RoundToInt (this.gameObject.transform.position.x);
		currentY = Mathf.RoundToInt (this.gameObject.transform.position.y);
		currentZ = Mathf.RoundToInt (this.gameObject.transform.position.z);*/
        currentX = (int)gameObject.transform.position.x;
        currentY = (int)gameObject.transform.position.y;
        currentZ = (int)gameObject.transform.position.z;
        currentCube.name="("+currentX.ToString()+","+currentY.ToString()+","+currentZ.ToString()+")";
	}
	public void MysteryCubeDataTurner ()	{
		currentCube = this.gameObject;
		currentX = Mathf.RoundToInt (this.gameObject.transform.position.x);
		currentZ = Mathf.RoundToInt (this.gameObject.transform.position.z);
		currentCube.name="Mystery("+currentX.ToString()+","+currentZ.ToString()+")";
	}
}

	

