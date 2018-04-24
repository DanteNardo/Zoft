using System.Collections.Generic;
using UnityEngine;

public class Polygon : MonoBehaviour {

	#region Members
	public int count;                           // The amount of vertices
	public Vector2 size;                        // The size of the overall 2D polygon
	public GameObject point;                    // The prefab for point masses
	public List<GameObject> vertices;           // The list of point masses
	private List<PhysicsObject> pointMasses;    // The list of point mass physics components
	private List<Spring> springs;               // The list of all springs that make up the soft body
	#endregion

	#region Methods
	private void Start() {
		CreatePolygon();
	}

    // Creates a poygon with "count" amount of vertices and creates springs inbetween
	private void CreatePolygon() {

		// Create vertices, point masses, and springs
		vertices = new List<GameObject>();
		pointMasses = new List<PhysicsObject>();
		springs = new List<Spring>();

		// Init variables for polygon generation
		float angle = 0;
		float deltaAngle = 360 / count;

		for (int i = 0; i < count; i++) {

			// Determine vertice's position in space
			float x = Mathf.Cos(Mathf.Deg2Rad * angle) * size.x;
			float y = Mathf.Sin(Mathf.Deg2Rad * angle) * size.y;
			Vector3 position = new Vector3(x, y, 0);

			// Instantiate point that represents vertice and point mass
			vertices.Add(Instantiate(point, position, Quaternion.identity));
			pointMasses.Add(vertices[i].GetComponent<PhysicsObject>());
			pointMasses[i].SetPosition(position);
            CollisionManager.Instance.pointMasses.Add(pointMasses[i]);

			// Add spring if this isn't the first iteration
			if (i != 0) {
				AddSpring(pointMasses[i-1], pointMasses[i]);
			}

			// Iterate angle to create polygon
			angle += deltaAngle;
		}
		
		// Add the last spring from the last vertice to the first
		AddSpring(pointMasses[pointMasses.Count-1], pointMasses[0]);
	}

    // Adds a spring between two physics objects
	private void AddSpring(PhysicsObject a, PhysicsObject b) {
		
		// Create a new spring component
		Spring spring = gameObject.AddComponent<Spring>() as Spring;

		// Set the initial values for the spring
		spring.stiffness = 8.0f;
		spring.damping = 0.1f;
		spring.objectA = a;
		spring.objectB = b;

		// Add the new spring
		springs.Add(spring);
	}
	#endregion
}