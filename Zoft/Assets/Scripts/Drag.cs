using UnityEngine;

public class Drag : Force {

	#region Members
	public float dragCoefficient;
	#endregion

	#region Methods
    private void Start() {
        physicsObject = GetComponent<PhysicsObject>();
    }

    public override void ApplyForce() {
        physicsObject.AddForce(-dragCoefficient * physicsObject.Velocity);
    }
	#endregion
}
