﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {

	public AudioSource audio;
	public AudioClip bump;
	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		audio.clip = bump;

	}
			
	void OnCollisionEnter2D(Collision2D other){

		if (other.gameObject.CompareTag ("Wall")) {
			print ("beep");
			audio.Play ();
		}
	}
}
