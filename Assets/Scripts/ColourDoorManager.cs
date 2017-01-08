using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourDoorManager : MonoBehaviour {

	public static void TryOpen (Color playerColor, Color doorColor, Collision2D coll ) {

		// TODO: test player colour against door's colour
		if (playerColor.a == doorColor.a && playerColor.b == doorColor.b && playerColor.g == doorColor.g) {
			print ("Pass the colour door");
			coll.collider.enabled = false;
		}
	}
}
