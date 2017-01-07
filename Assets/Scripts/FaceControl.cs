using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceControl : MonoBehaviour {

	Animator anim;
	private float nextBlink;
	private float lastBlink;

	void Start () {
		anim = GetComponent<Animator> ();
		generateBlink ();
		lastBlink = 0f;
	}

	void Update () {
		if (Time.time > lastBlink + nextBlink) {
			lastBlink = Time.time;
			generateBlink ();
			anim.SetTrigger ("Blink");
		}
	}

	void generateBlink() {
		nextBlink = Random.Range (0f, 4f) + Random.Range (0f, 4f) + Random.Range (0f, 4f);
	}
}
