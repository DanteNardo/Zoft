using UnityEngine;

public class Spring : Force {

	#region Members
	public float stiffness;
    public float damping;
    private float restLength;
    private float currLength;
    private Vector3 direction;
    public PhysicsObject objectA;
    public PhysicsObject objectB;
	#endregion

	#region Methods
    private void Start() {
        restLength = Vector3.Distance(objectA.Position, objectB.Position);
        currLength = restLength;
    }

    public override void ApplyForce() {
        
        // Get the direction between the two objects
        direction = objectA.Position - objectB.Position;

        if (direction != Vector3.zero) {

            // Get the length and direction to calculate spring force
            currLength = direction.magnitude;
            direction.Normalize();

            // Apply spring force and damping
            // Hooke's Law: -k * x where k is a constant and x is the length
            // Damped oscillator: this means the damping is proportional to the
            // velocity in a harmonic oscillator (spring).
            Vector3 v = objectA.Velocity - objectB.Velocity;
            Vector3 force = -stiffness * ((currLength - restLength) * direction);
            force += -damping * Vector3.Dot(v, direction) * direction;

            // Apply the equal force to both objects, but opposite directions
            objectA.AddForce(force);
            objectB.AddForce(-force);
        }
    }
	#endregion
}
