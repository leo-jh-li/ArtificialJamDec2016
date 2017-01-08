using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColour : MonoBehaviour {

	public GameObject player;
	public static SpriteRenderer[] children;
	void Start () {

		children =GetComponentsInChildren<SpriteRenderer>();	// Use this for initialization
	}

	public static void changeColor(Color playerColor){

		for (int i = 0; i < children.Length; i++) {
			print ("Changed children colour");
			children [i].color = playerColor;
		}
	}

}
