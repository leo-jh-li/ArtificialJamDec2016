using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorDoorSounds : MonoBehaviour {

	public AudioSource audio;
	public AudioClip clip;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		audio.clip = clip; 
	}

	void OnCollisionEnter2D(Collision2D other){

		if (other.gameObject.CompareTag ("ColourDoor")) {
			audio.clip = clip;
			print ("bounce");
			audio.Play ();
		} 
	}
}
