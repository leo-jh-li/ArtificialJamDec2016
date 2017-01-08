using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveAI : MonoBehaviour {

	public class movement{
		public GameObject destination;
		public Vector2 resultantRot;
		public bool clockwise;
		public float time;

		public movement(GameObject des, Vector2 rot, bool dir, float t){
			destination = des;
			resultantRot = rot;
			clockwise = dir;
			time = t;
		}
	}
	public GameObject[] toRotate;

	public GameObject[] destination;
	public Vector2[] resultantRot;
	public bool[] clockwise;
	public float[] time;

	public movement[] setOfMoves;
	private Rigidbody2D rb;
	private GameObject face;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		setOfMoves = new movement[destination.Length];

		for (int i = 0; i < setOfMoves.Length; i++) {
			setOfMoves [i] = new movement (destination[i], resultantRot[i], clockwise[i], time[i]);
		}
		StartCoroutine(moveEnemy());
	}

	IEnumerator moveEnemy(){
		int iterable = setOfMoves.Length;

		for (int i = 0; i < iterable; i = (i + 1) % iterable) {
			//Get Values
			Vector2 from = new Vector2 (0, -1);
			float timeLeft = setOfMoves [i].time;
			rb.velocity = (setOfMoves [i].destination.transform.position - transform.position) / setOfMoves [i].time;
			float angle = calculateAngle (from, setOfMoves[i].resultantRot);
			if (angle < 0 && setOfMoves [i].clockwise || angle > 0 && !setOfMoves [i].clockwise)
				angle = angle - 360;

			while (timeLeft > 0f) {
				timeLeft = timeLeft - Time.fixedDeltaTime;
				foreach (GameObject g in toRotate) {
					g.transform.rotation = Quaternion.Lerp (g.transform.rotation, Quaternion.Euler (0,0,angle), 1/timeLeft);
				}
				yield return new WaitForFixedUpdate();
			}
			//Set the actual ending position (to avoid weird offsets)
			transform.position = setOfMoves [i].destination.transform.position;
			foreach (GameObject g in toRotate) {
				g.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			}

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
