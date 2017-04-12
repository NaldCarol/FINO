using UnityEngine;
using System.Collections;

public class Ch_NPCFollowNPoint : MonoBehaviour {
	public Transform targetNPC;
	public Transform targetPlayer;
	public float slowSmoothTime = 0.5F;
	public float fastSmoothTime = 0.01F;
	private Vector3 velocity = Vector3.zero;
	public bool slowSmoothSwitch;
	Quaternion wantedRotation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		slowSmoothSwitch = targetNPC.GetComponent<Ch_NPCFollowHero> ().slowSmooth;
		Vector3 targetPosition = targetNPC.TransformPoint(new Vector3(0, 0.5f, 0));

		if (slowSmoothSwitch == true) {
			wantedRotation = Quaternion.LookRotation (targetPlayer.transform.position - this.transform.position, targetPlayer.up);
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, wantedRotation, Time.deltaTime * 1f);
			this.transform.position = Vector3.Lerp (this.transform.position,targetPosition,Time.deltaTime*1f);
			//transform.position = Vector3.SmoothDamp (transform.position, targetPosition, ref velocity, slowSmoothTime);
		} else {
			transform.position = Vector3.SmoothDamp (transform.position, targetPosition, ref velocity, fastSmoothTime);
		}
	}
}
