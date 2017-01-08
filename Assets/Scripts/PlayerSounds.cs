using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {

	public AudioSource audio;
	public AudioClip bump;
	public AudioClip keyPickup;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		audio.clip = bump;
	}
			
	void OnCollisionEnter2D(Collision2D other){

		if (other.gameObject.CompareTag ("Wall")) {
			audio.clip = bump;
			print ("beep");
			audio.Play ();
		}
	}

	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.CompareTag ("Key")) {
			audio.clip = keyPickup;
			audio.Play ();
		}
	}
}
