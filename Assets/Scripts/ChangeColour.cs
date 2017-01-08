using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColour : MonoBehaviour {

	public static SpriteRenderer[] children;

	void Start () {
		children = GetComponentsInChildren<SpriteRenderer>();	// Use this for initialization
	}

	public static void changeColor(Color playerColor){
		
		for (int i = 0; i < children.Length; i++) {
			if (children [i].gameObject.name.Equals("Body")) {
				print ("Changed body colour");
				children [i].color = playerColor;
			}
		}
	}

}
