using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveAI : MonoBehaviour {

	public class movement{
		public Vector2 destination;
		public Vector2 resultantRot;
		public bool clockwise;
		public float time;
	}

	public movement[] setOfMoves;

	// Use this for initialization
	void Start () {
		StartCoroutine(moveEnemy());
	}

	IEnumerator moveEnemy(){
		int iterable = setOfMoves.Length;

		for (int i = 0; i < iterable; i = (i + 1) % iterable) {
			
		}

		yield return null;
	}
}
