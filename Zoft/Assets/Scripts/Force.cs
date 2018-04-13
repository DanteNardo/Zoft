using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Force : MonoBehaviour {
    
    #region Members
    protected PhysicsObject physicsObject;
	protected Vector3 acceleration;
	#endregion

	#region Properties
	public Vector3 Acceleration { get { return acceleration; } }
	public float AccelerationX { get { return acceleration.x; } }
	public float AccelerationY { get { return acceleration.y; } }
	public float AccelerationZ { get { return acceleration.z; } }
	#endregion

	#region Methods
    protected void Update() {
        ApplyForce();
    }

	public abstract void ApplyForce();
	#endregion
}
