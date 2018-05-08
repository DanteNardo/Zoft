using UnityEngine;

public class PhysicsObject : MonoBehaviour {

    #region Members
    public Gravity gravity;
	protected Vector3 force;
    public float mass;
    #endregion

    #region Properties
    public float InvertMass { get; private set; }
    public Vector3 Position { get; protected set; }
    public Vector3 Velocity { get; set; }
    public Vector3 Acceleration { get; protected set; }
	#endregion

	#region Methods
	private void Start() {
        gravity = GetComponent<Gravity>();
        SetPosition(transform.position);
		ResetForce();
        InvertMass = 1 / mass;
	}

	private void FixedUpdate() {
        // TODO: Change integration
        // Semi-Implicit Euler Integration
        Acceleration = force * InvertMass;
		Velocity += Acceleration * Time.fixedDeltaTime;
		Position += Velocity * Time.fixedDeltaTime;
		gameObject.transform.position = Position;

        ResetForce();
	}

	public void SetPosition(Vector3 position) {
		Position = position;
        gameObject.transform.position = Position;

    }

	public void AddForce(Vector3 force) {
		this.force += force;
	}

    public void AddImpulse(Vector3 impulse) {
        Velocity += impulse * InvertMass;
    }

	public void ResetForce() {
		force = Vector3.zero;
	}
	#endregion
}
