using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeSpawner : MonoBehaviour {
    #region Members
    public ColorManager colorManager;
    public GameObject shapePrefab;
    private List<GameObject> shapes;
    #endregion

    #region Methods
    private void Start() {
        shapes = new List<GameObject>();
    }

    public void Spawn(Text text) {
        Spawn(text.text);
    }

    public void Spawn(string shapeName) {
        switch (shapeName) {
            case "Triangle (3)": 
                Spawn(3);
                break;
            case "Square (4)":
                Spawn(4);
                break;
            case "Pentagon (5)":
                Spawn(5);
                break;
            case "Hexagon (6)":
                Spawn(6);
                break;
            case "Heptagon (7)":
                Spawn(7);
                break;
            case "Octagon (8)":
                Spawn(8);
                break;
            case "Dodecahedron (12)":
                Spawn(12, 1000.0f, 0.90f, 2.0f, 0.95f);
                break;
            case "Icosagon (20)":
                Spawn(20, 1500.0f, 0.55f, 3.0f, 0.55f);
                break;
            default:
                Debug.LogError("Incorrect string passed to Spawn");
                break;
        }
    }

    public void Clear() {
        CollisionManager.Instance.Clear();
        foreach (var shape in shapes) {
            Destroy(shape);
        }
    }

    public void Spawn(int verticeCount, float stiffness, float damping, float centerStiffness, float centerDamping) { 
        // Handle spawning instances
        //if (lastInstance != null) Destroy(lastInstance);
        var instance = Instantiate(shapePrefab, Vector3.zero, Quaternion.identity);

        // Get the polygon and update variables
        Polygon polygon = instance.GetComponent<Polygon>();
        polygon.count = verticeCount;
        polygon.stiffness = stiffness;
        polygon.damping = damping;
        polygon.centerStiffness = centerStiffness;
        polygon.centerDamping = centerDamping;

        // Get the component to draw and update the color
        DrawPolygon draw = instance.GetComponent<DrawPolygon>();
        draw.polygonColor = colorManager.polygonColor;

        // Add to list
        shapes.Add(instance);
    }

    public void Spawn(int verticeCount) {
        // Handle spawning instances
        //if (lastInstance != null) Destroy(lastInstance);
        var instance = Instantiate(shapePrefab, Vector3.zero, Quaternion.identity);

        // Get the polygon and update vertex count
        Polygon polygon = instance.GetComponent<Polygon>();
        polygon.count = verticeCount;

        // Get the component to draw and update the color
        DrawPolygon draw = instance.GetComponent<DrawPolygon>();
        draw.polygonColor = colorManager.polygonColor;

        // Add to list
        shapes.Add(instance);
    }
    #endregion
}
