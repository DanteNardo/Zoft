using UnityEngine;

public class PhysicsBox : PhysicsObject {

    #region Properties
    public float Width { get; private set; }
    public float Height { get; private set; }
    public Vector2 Min {
        get {
            return new Vector2(
                transform.position.x - Width / 2,
                transform.position.y - Height / 2);
        }
    }
    public Vector2 Max {
        get {
            return new Vector2(
                transform.position.x + Width / 2,
                transform.position.y + Height / 2);
        }
    }
    #endregion

    #region Methods
    private void Start() {
        SetPosition(transform.position);
        ResetForce();
        Width = transform.localScale.x;
        Height = transform.localScale.y;
    }
    #endregion
}
