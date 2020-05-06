using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour {

    public static CollisionManager Instance;
    public List<PhysicsObject> pointMasses;
    public List<PhysicsBox> boxColliders;

    private Vector2 penetration;

    private void Awake() {

        // Create a singleton
        if (Instance != null) {
            Destroy(Instance.gameObject);
        }
        Instance = this;
    }

    private void FixedUpdate() {
        CollisionDetection();
    }

    public void Clear() {
        pointMasses.Clear();
    }

    public void Remove(Polygon polygon) {
        foreach (var physicsObject in polygon.GetComponents<PhysicsObject>()) {
            pointMasses.Remove(physicsObject);
        }
        foreach (var physicsObject in polygon.GetComponents<PhysicsObject>()) {
            pointMasses.Remove(physicsObject);
        }
    }

    // Performs all collision detection
    public void CollisionDetection() {

        // See if any point masses are inside of a box
        foreach (var pm in pointMasses) {
            foreach (var box in boxColliders) {
                if (Inside(pm, box)) {
                    CollisionResponse(pm, box);
                }
            }
        }
    }

    // Returns whether or not the point is inside the box
    // If it is, the penetration is saved for collision response
    private bool Inside(PhysicsObject pointMass, PhysicsBox box) {
        if (AABB(pointMass, box)) {
            FixPenetration(pointMass, box);
            return true;
        }
        else return false;
    }

    // Finds if a point is inside the box
    private bool AABB(PhysicsObject p, PhysicsBox b) {
        return  p.Position.x > b.Min.x &&
                p.Position.y > b.Min.y &&
                p.Position.x < b.Max.x &&
                p.Position.y < b.Max.y;
    }

    // Fixes the penetration of the objects
    private void FixPenetration(PhysicsObject p, PhysicsBox b) {

        // Move in opposite direction of movement
        Vector3 dir = -p.Velocity.normalized;
        float buffer = 0.01f;

        // MinX and MinY and MinX is the closer one
        //if (p.Position.x < b.Position.x &&
        //    p.Position.y < b.Position.y &&
        //    p.Position.x - b.Min.x < p.Position.y - b.Min.y) {
        //    float distMod = b.Min.x - p.Position.x;
        //    p.SetPosition(p.Position + dir * distMod);
        //}

        // MinX and MinY and MinY is the closer one
        if (p.Position.x <= b.Position.x &&
                 p.Position.y <= b.Position.y &&
                 p.Position.x - b.Min.x > p.Position.y - b.Min.y) {
            float distMod = b.Min.y - p.Position.y + buffer;
            p.SetPosition(p.Position + dir * distMod);
        }

        // MinX and MaxY and MinX is the closer one
        //else if (p.Position.x < b.Position.x &&
        //         p.Position.y >= b.Position.y &&
        //         p.Position.x - b.Min.x < b.Max.y - p.Position.y) {
        //    float distMod = b.Min.x - p.Position.x;
        //    p.SetPosition(p.Position + dir * distMod);
        //}

        // MinX and MaxY and MaxY is the closer one
        else if (p.Position.x <= b.Position.x &&
                 p.Position.y >= b.Position.y &&
                 p.Position.x - b.Min.x > b.Max.y - p.Position.y) {
            float distMod = b.Max.y - p.Position.y + buffer;
            p.SetPosition(p.Position + dir * distMod);
        }

        // MaxX and MaxY and MaxX is the closer one
        //else if (p.Position.x >= b.Position.x &&
        //         p.Position.y >= b.Position.y &&
        //         b.Max.y - p.Position.x < b.Max.y - p.Position.y) {
        //    float distMod = b.Max.x - p.Position.x;
        //    p.SetPosition(p.Position + dir * distMod);
        //}

        // MaxX and MaxY and MaxY is the closer one
        else if (p.Position.x >= b.Position.x &&
                 p.Position.y >= b.Position.y &&
                 b.Max.x - p.Position.x > b.Max.y - p.Position.y) {
            float distMod = b.Max.y - p.Position.y + buffer;
            p.SetPosition(p.Position + dir * distMod);
        }

        // MaxX and MinY and MaxX is the closer one
        //else if (p.Position.x >= b.Position.x &&
        //         p.Position.y < b.Position.y &&
        //         b.Max.x - p.Position.x < p.Position.y - b.Min.y) {
        //    float distMod = b.Max.x - p.Position.x;
        //    p.SetPosition(p.Position + dir * distMod);
        //}

        // MaxX and MinY and MinY is the closer one
        else if (p.Position.x >= b.Position.x &&
                 p.Position.y <= b.Position.y &&
                 b.Max.x - p.Position.x > p.Position.y - b.Min.y) {
            float distMod = b.Min.y - p.Position.y + buffer;
            p.SetPosition(p.Position + dir * distMod);
        }
    }

    // Applies an impulse based on a collision
    private void CollisionResponse(PhysicsObject pm, PhysicsBox b) {

        // Find the normalized relative velocity
        Vector3 relVel = pm.Velocity;
        Vector3 norm = pm.Position - b.Position;
        float nVel = Vector3.Dot(relVel, norm);

        // Set velocity to halt movement
        pm.Velocity = Vector3.zero;

        // Restitution (bouncyness)
        float e = 0.9f;

        // Calculate impulse
        float j = -(1 + e) * nVel;
        j /= (pm.mass + b.mass);
        Vector3 impulse = j * norm;
        pm.AddImpulse(impulse);
    }
}
