using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerVision : MonoBehaviour {

	//Camera variables
	private Camera cam;
	private float camHeight;
	private float camWidth;

	private HashSet<Vector2> visionPolygon; //DOES NOT INCLUDE THE 4 STATIC CORNER POINTS

	// Use this for initialization
	void Start () {
		//Init visionPolygon
		visionPolygon = new HashSet<Vector2>();

		//Get cam size
		cam = Camera.main;
		camHeight = (cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, 0)) - cam.ScreenToWorldPoint (new Vector3(0, 0, 0))).y;
		camWidth = (cam.ScreenToWorldPoint (new Vector3(cam.pixelWidth, 0, 0)) - cam.ScreenToWorldPoint (new Vector3(0, 0, 0))).x;

		//Create 
		BoxCollider2D bc2 = gameObject.AddComponent<BoxCollider2D>();
		bc2.size = new Vector2(camWidth, camHeight);
		bc2.isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D c) {
		GameObject wall = c.gameObject;
		if (wall.CompareTag("Wall")) {
			BoxCollider2D bc2 = (BoxCollider2D)c;
			Vector2 bc2size = bc2.size;
			Vector2 center = bc2.transform.position;

			//Add the 4 points to the visionPolygon
			visionPolygon.Add(new Vector2(center.x + bc2size.x, center.y + bc2size.y));
			visionPolygon.Add(new Vector2(center.x - bc2size.x, center.y + bc2size.y));
			visionPolygon.Add(new Vector2(center.x + bc2size.x, center.y - bc2size.y));
			visionPolygon.Add(new Vector2(center.x - bc2size.x, center.y - bc2size.y));
		}
	}

	void OnTriggerExit2D (Collider2D c){
		GameObject wall = c.gameObject;
		if (wall.CompareTag ("Wall")) {
			BoxCollider2D bc2 = (BoxCollider2D)c;
			Vector2 bc2size = bc2.size;
			Vector2 center = bc2.transform.position;

			//Remove the 4 points to the visionPolygon
			visionPolygon.Remove(new Vector2(center.x + bc2size.x, center.y + bc2size.y));
			visionPolygon.Remove(new Vector2(center.x - bc2size.x, center.y + bc2size.y));
			visionPolygon.Remove(new Vector2(center.x + bc2size.x, center.y - bc2size.y));
			visionPolygon.Remove(new Vector2(center.x - bc2size.x, center.y - bc2size.y));
		}
	}

	//Create an unsorted list of 
	List<Vector2> createUnsortedListVectorsAndBasePoints(HashSet<Vector2> polygon){
		List<Vector2> listVector = new List<Vector2>();

		//Add the screen's 4 points
		listVector.Add(new Vector2(transform.position.x + camWidth/2, transform.position.y + camHeight/2));
		listVector.Add(new Vector2(transform.position.x + camWidth/2, transform.position.y - camHeight/2));
		listVector.Add(new Vector2(transform.position.x - camWidth/2, transform.position.y + camHeight/2));
		listVector.Add(new Vector2(transform.position.x - camWidth/2, transform.position.y - camHeight/2));

		foreach(Vector2 v in polygon){
			listVector.Add (v);
		}

		return listVector;
	}

	List<RaycastHit2D> rayCastSortedVectors(List<Vector2> polygon){
		List<RaycastHit2D> raycastResults = new List<RaycastHit2D>();
		foreach (Vector2 v in polygon) {
			//raycastResults.Add(Physics2D.Raycast (transform.position, v*Quaternion.AngleAxis(Mathf.Rad2Deg*-0.00001f, Vector3.forward), Mathf.Infinity, LayerMask.NameToLayer("Wall"))); 
			raycastResults.Add(Physics2D.Raycast (transform.position, v, Mathf.Infinity, LayerMask.NameToLayer("Wall"))); 
			//raycastResults.Add(Physics2D.Raycast (transform.position, v*Quaternion.AngleAxis(Mathf.Rad2Deg*+0.00001f, Vector3.forward), Mathf.Infinity, LayerMask.NameToLayer("Wall"))); 
		}

		return raycastResults;
	}

	List<Vector3> getRaycastHitPoints(List<RaycastHit2D> raycastResults){
		List<Vector3> points = new List<Vector3>();
		foreach (RaycastHit2D rh in raycastResults) {
			points.Add (rh.point);
		}

		return points;
	}

	/*
	Mesh DrawAMeshMaybe(List<Vector3> points) {
		Mesh m = new Mesh ();
		//m.vertices = points;

	}*/

	public float calculateAngle(Vector2 vec1, Vector2 vec2){
		float dotProduct = Vector2.Dot (vec1, vec2);
		float determinant = vec1.x * vec2.y - vec1.y * vec2.x;

		float angle = Mathf.Atan2 (determinant, dotProduct) * 180/Mathf.PI;

		return angle;
	}
}
