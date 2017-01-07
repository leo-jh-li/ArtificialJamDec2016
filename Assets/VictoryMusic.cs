using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryMusic : MonoBehaviour {

	public AudioSource audio;
	public AudioClip victory;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		audio.clip = victory;
	}
	
	void OnTriggerEnter2D(Collider2D coll){
		print ("JUMP");
		audio.Play ();
	}
}
