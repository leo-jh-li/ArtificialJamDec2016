using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableVisibility : MonoBehaviour {
	// Use this for initialization
	void Start () {
		//Disable Map Render at the beginning
		disableMapRender();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//ASSUME THE HIERARCHY MAP -> ROOM -> FLOOR TILE (WE WANT THE FLOOR TILE)
	void disableMapRender(){
		for (int i = 0; i < transform.childCount; i++) {
			Transform room = transform.GetChild (i);
			for (int j = 0; j < room.childCount; j++) {
				Transform tile = room.GetChild (j);
				if(tile.gameObject.layer == LayerMask.NameToLayer("Floor"))
					tile.GetComponent<SpriteRenderer> ().color = new Color(1f, 1f, 1f, 0);	//Transparency
			}
		}
	}
}
