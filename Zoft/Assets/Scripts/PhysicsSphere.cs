using UnityEngine;
using System.Collections;

public class PhysicsSphere : PhysicsObject {

    #region Members
    private float radius;
    #endregion

    #region Methods
    private void Start() {
        ResetForce();
    }

    private void FixedUpdate() {

        // TODO: Change integration
        // Semi-Implicit Euler Integration
        Acceleration = force / mass;
        Velocity += Acceleration * Time.fixedDeltaTime;
        Position += Velocity * Time.fixedDeltaTime;
        gameObject.transform.position = Position;

        ResetForce();
    }
    #endregion
}
