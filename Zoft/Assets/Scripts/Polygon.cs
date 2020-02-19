using System.Collections.Generic;
using UnityEngine;

public class Polygon : MonoBehaviour {

	#region Members
	public int count;                           // The amount of vertices
	public float stiffness = 1000.0f;           // Stiffness
	public float damping = 0.9f;                // Damping for springs (slows down spring movement)
	public float centerStiffness = 2.0f;        // Stiffness is multiplied by this for center springs to make the polygon structurally sound
	public float centerDamping = 1.05f;         // Damping is multiplied by this for center springs to make the polygon structurally sound
	public Vector2 size;                        // The size of the overall 2D polygon
	public GameObject pointPrefab;                    // The prefab for point masses
	public List<GameObject> vertices;           // The list of point masses
	private List<PhysicsObject> pointMasses;    // The list of point mass physics components
	private List<Spring> springs;               // The list of all springs that make up the soft body
    public GameObject center;                   // The center point of the shape
    private PhysicsObject centerPhysics;        // The physics component of the center shape
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
			var point = Instantiate(pointPrefab, position, Quaternion.identity);
			point.transform.parent = transform;
			vertices.Add(point);
			pointMasses.Add(vertices[i].GetComponent<PhysicsObject>());
			pointMasses[i].SetPosition(position);
            CollisionManager.Instance.pointMasses.Add(pointMasses[i]);

			// Add spring if this isn't the first iteration
			if (i != 0) {
				AddSpring(pointMasses[i - 1], pointMasses[i], stiffness, damping);
			}

			// Iterate angle to create polygon
			angle += deltaAngle;
        }

        // Add the last spring from the last vertice to the first
        AddSpring(pointMasses[pointMasses.Count - 1], pointMasses[0], 1000.0f, 0.9f);

		/// <summary>
		/// The following code is a partial attempt at connecting polygon points horizontally and vertically for larger polygons. 
		/// However I scrapped the implementation once I realized it would be a systematic change that requires far more fine tuning.
		/// Essentially, you need different values from outer springs, inner springs, and springs that connect to the center.
		/// For this project I did not want to spend a lot of time fine tuning the result unless I expanded the project itself.
		/// </summary>
		// If there are enough vertices, then connect horizontally and vertically
		//if (count >= 6) {
		//	int index = 0;
		//	for (int i = 0; i < count / 2; i++) {
		//		// Calculate which vertice to connect to
		//		int pairedVertice = index + (count / 2) - 1;
		//		if (pairedVertice >= count) pairedVertice %= count;
		//		Debug.Log("Index:" + index + " PairedVertice:" + pairedVertice);

		//		// Create vertical/horizontal spring
		//		AddSpring(pointMasses[index], pointMasses[pairedVertice], stiffness, damping);

		//		// Update primary index
		//		index += (count / 4);
		//		if (index >= count) index %= count;
		//	}
		//}

		// Lastly, add a center point if the shape has at least three vertices
		if (count >= 3) {
			center = Instantiate(pointPrefab, Vector3.zero, Quaternion.identity);
			center.transform.parent = transform;
            centerPhysics = center.GetComponent<PhysicsObject>();
            centerPhysics.SetPosition(Vector3.zero);
            CollisionManager.Instance.pointMasses.Add(centerPhysics);

            // Have a spring from the center point to each vertice
            foreach (var point in pointMasses) {
                AddSpring(point, centerPhysics, stiffness * centerStiffness, damping * centerDamping);
            }
        }
	}

    // Adds a spring between two physics objects
	private void AddSpring(PhysicsObject a, PhysicsObject b, float stiffness, float damping) {
		
		// Create a new spring component
		Spring spring = gameObject.AddComponent<Spring>() as Spring;

		// Set the initial values for the spring
		spring.stiffness = stiffness;
		spring.damping = damping;
		spring.objectA = a;
		spring.objectB = b;

		// Add the new spring
		springs.Add(spring);
	}

	public List<Spring> GetSprings() {
		return springs;
	}
	#endregion
}