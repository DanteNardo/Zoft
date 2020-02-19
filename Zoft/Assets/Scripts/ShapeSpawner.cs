using UnityEngine;

public class ShapeSpawner : MonoBehaviour {
    #region Members
    public GameObject shapePrefab;
    private GameObject lastInstance;
    #endregion

    #region Methods
    public void Spawn(int verticeCount) {
        if (lastInstance != null) Destroy(lastInstance);
        lastInstance = Instantiate(shapePrefab, Vector3.zero, Quaternion.identity);
        Polygon poly = lastInstance.GetComponent<Polygon>();
        poly.count = verticeCount;
    }
    #endregion
}
