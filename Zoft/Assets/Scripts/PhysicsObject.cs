using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour {

	#region Members
	private Vector3 force;
	private Vector3 acceleration;
	private Vector3 velocity;
	private Vector3 position;
	public float mass;
	#endregion

	#region Properties
	public float Mass { get { return mass; } }
	public Vector3 Position { get { return position; } }
	public Vector3 Velocity { get { return velocity; } }
	public Vector3 Acceleration { get { return acceleration; } }
	public float PositionX { get { return position.x; } }
	public float PositionY { get { return position.y; } }
	public float PositionZ { get { return position.z; } }
	#endregion

	#region Methods
	private void Start() {
		ResetForce();
	}

	private void FixedUpdate() {
		// TODO: Change integration
		// Semi-Implicit Euler Integration
		acceleration = force / mass;
		velocity += acceleration * Time.fixedDeltaTime;
		position += velocity * Time.fixedDeltaTime;
		gameObject.transform.position = position;

		ResetForce();
	}
	public void SetPosition(Vector3 position) {
		this.position = position;
	}

	public void AddForce(Vector3 force) {
		this.force += force;
	}

	public void ResetForce() {
		force = Vector3.zero;
	}
	#endregion
}
