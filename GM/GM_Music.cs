using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_Music : MonoBehaviour {
	private AudioSource musicSource;
	//private float vol;

	[HideInInspector]
	public static float globalVol = -100f;
	[HideInInspector]

	// Use this for initialization
	void Start () {
		musicSource = GetComponent<AudioSource> ();	
		if (globalVol == -100f) {
			musicSource.volume = 1;
			globalVol = musicSource.volume;
		} else {
			musicSource.volume = globalVol;
		}
	}
	
	public void VolChange(float newVol_Temp){
		musicSource.volume = newVol_Temp;
		globalVol = newVol_Temp;
	}
}
