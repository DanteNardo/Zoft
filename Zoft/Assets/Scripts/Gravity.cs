using UnityEngine;

public class Gravity : Force {

	#region Methods
    private void Start() {
        physicsObject = GetComponent<PhysicsObject>();
        acceleration = new Vector3(0, -9.81f, 0);
    }

	public override void ApplyForce() {
        physicsObject.AddForce(physicsObject.mass * acceleration);
    }
	#endregion
}
