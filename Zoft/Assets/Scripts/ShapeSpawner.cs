using UnityEngine;
using UnityEngine.UI;

public class ShapeSpawner : MonoBehaviour {
    #region Members
    public ColorManager colorManager;
    public GameObject shapePrefab;
    private GameObject lastInstance;
    #endregion

    #region Methods
    public void Spawn(Text text) {
        Spawn(text.text);
    }

    public void Spawn(string shapeName) {
        switch (shapeName) {
            case "Triangle": 
                Spawn(3);
                break;
            case "Square":
                Spawn(4);
                break;
            case "Pentagon":
                Spawn(5);
                break;
            case "Hexagon":
                Spawn(6);
                break;
            case "Heptagon":
                Spawn(7);
                break;
            case "Octagon":
                Spawn(8);
                break;
            default:
                Debug.LogError("Incorrect string passed to Spawn");
                break;
        }
    }

    public void Spawn(int verticeCount) {
        // Handle spawning instances
        if (lastInstance != null) Destroy(lastInstance);
        lastInstance = Instantiate(shapePrefab, Vector3.zero, Quaternion.identity);

        // Get the polygon and update vertex count
        Polygon polygon = lastInstance.GetComponent<Polygon>();
        polygon.count = verticeCount;

        // Get the component to draw and update the color
        DrawPolygon draw = lastInstance.GetComponent<DrawPolygon>();
        draw.polygonColor = colorManager.polygonColor;
    }
    #endregion
}
