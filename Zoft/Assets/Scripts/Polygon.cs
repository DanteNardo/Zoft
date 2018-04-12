using System.Collections.Generic;
using UnityEngine;

public class Polygon : MonoBehaviour{
	public int count;
	public Vector2 size;
	public GameObject point;
	public List<GameObject> vertices;

	private void Start() {
		CreatePolygon();
	}

	private void CreatePolygon() {

		// Create gameObjects that represent vertices
		vertices = new List<GameObject>();

		// Init variables for polygon generation
		float angle = 0;
		float deltaAngle = 360 / count;

		for (int i = 0; i < count; i++) {

			// Determine vertice's position in space
			float x = Mathf.Cos(Mathf.Deg2Rad * angle) * size.x;
			float y = Mathf.Sin(Mathf.Deg2Rad * angle) * size.y;
			Vector3 position = new Vector3(x, y, 0);

			// Instantiate point that represents vertice
			vertices.Add(Instantiate(point, position, Quaternion.identity));

			// Iterate angle to create polygon
			angle += deltaAngle;
		}
	}
}