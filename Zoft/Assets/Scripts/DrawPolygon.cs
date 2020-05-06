using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrawPolygon : MonoBehaviour
{
    #region DrawPolygon Members
    public bool drawPolygon;
    public Color polygonColor;
    public Polygon polygon;
    public Material referenceMaterial;
    private Material polygonMaterial;
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;
    private Vector3[] vertices3d;
    private Vector2[] vertices2d;
    #endregion

    #region DrawPolygon Methods
    private void Start() {
        // Get components
        if (!polygon) polygon = GetComponent<Polygon>();
        if (!meshRenderer) meshRenderer = GetComponent<MeshRenderer>();
        if (!meshFilter) meshFilter = GetComponent<MeshFilter>();

        // Create new material
        polygonMaterial = new Material(referenceMaterial);

        // Create the mesh
        meshFilter.mesh = new Mesh();
        UpdateColor();
        UpdateMesh();
    }

    private void LateUpdate() {
        UpdateColor();
        UpdateMesh();
        UpdateDrawState();
    }

    private void UpdateMesh() {
        // Update vertex arrays from polygon
        vertices3d = GetVertices();
        vertices2d = ConvertVector3To2(meshFilter.mesh.vertices);

        // Update Mesh Vertices
        meshFilter.mesh.vertices = vertices3d;

        // Update Mesh Triangles
        meshFilter.mesh.triangles = new Triangulator(vertices2d).Triangulate();

        // Update Mesh Colors
        meshFilter.mesh.colors = CreateColorArrayFromVertices(vertices2d);

        // Recalculate Mesh normals and bounds
        meshFilter.mesh.RecalculateNormals();
        meshFilter.mesh.RecalculateBounds();

        // Recalculate Mesh UV texture coordinates
        Bounds bounds = meshFilter.mesh.bounds;
        meshFilter.mesh.uv = vertices2d.Select(v => new Vector2(v.x / bounds.size.x, v.y / bounds.size.y)).ToArray();
    }

    private void UpdateColor() { 
        polygonMaterial.color = polygonColor;
        meshRenderer.material = polygonMaterial;
    }

    private Vector3[] GetVertices() {
        return polygon.vertices.Select(v => new Vector3(
            v.transform.position.x, 
            v.transform.position.y))
            .ToArray();
    }

    private Vector2[] ConvertVector3To2(Vector3[] array) { 
        return System.Array.ConvertAll<Vector3, Vector2>(array, v => v);
    }
    
    private Color[] CreateColorArrayFromVertices(Vector2[] vertices) { 
        Color[] colors = new Color[vertices.Length];
        for (int i = 0; i < vertices.Length; i++) {
            colors[i] = polygonColor;
        }
        return colors;
    }

    private void UpdateDrawState() { 
        if (drawPolygon == false && meshRenderer.enabled == true) {
            meshRenderer.enabled = false;
        }
        else if (drawPolygon == true && meshRenderer.enabled == false) {
            meshRenderer.enabled = true;
        }
    }
    #endregion
};
