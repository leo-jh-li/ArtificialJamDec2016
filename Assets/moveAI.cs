using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveAI : MonoBehaviour {

	public class movement{
		public GameObject destination;
		public Vector2 resultantRot;
		public bool clockwise;
		public float time;
	}

	public movement[] setOfMoves;
	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		StartCoroutine(moveEnemy());
	}

	IEnumerator moveEnemy(){
		int iterable = setOfMoves.Length;

		for (int i = 0; i < iterable; i = (i + 1) % iterable) {
			//Get Values
			float timeLeft = setOfMoves [i].time;
			float angle = calculateAngle (setOfMoves [i].resultantRot, rb.rotation);
			if (angle < 0 && setOfMoves [i].clockwise || angle > 0 && !setOfMoves [i].clockwise)
				angle = angle - 360;

			rb.velocity = (setOfMoves [i].destination.transform.position - transform.position) / setOfMoves [i].time;
			rb.angularVelocity = angle * Mathf.Deg2Rad / setOfMoves [i].time;
			while (timeLeft > 0f) {
				timeLeft = timeLeft - Time.fixedDeltaTime;
				yield return new WaitForFixedUpdate();
			}
			//Set the actual ending position (to avoid weird offsets)
			transform.position = setOfMoves [i].destination.transform.position;
			transform.rotation = setOfMoves [i].resultantRot;
		}
		yield return null;
	}

	public float calculateAngle(Vector2 vec1, Vector2 vec2){
		float dotProduct = Vector2.Dot (vec1, vec2);
		float determinant = vec1.x * vec2.y - vec1.y * vec2.x;
		float angle = Mathf.Atan2 (determinant, dotProduct) * 180/Mathf.PI;

		return angle;
	}
}
