using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPolygon : MonoBehaviour {

	#region DrawPolygon Members
	public Material lineMat;
	public Polygon poly;
    #endregion

    #region DrawPolygon Methods

    // Get the polygon if it wasn't dragged in the inspector
    private void Start() {
        if (poly == null) {
            poly = GetComponent<Polygon>();
        }
    }

	// Will be called after all regular rendering is done
    public void OnRenderObject()
    {
        // Apply the line material
        lineMat.SetPass(0);

		// Safely put away matrix so that we can draw to GL temporarily
        GL.PushMatrix();

        // Set transformation matrix for drawing to match our transform
        GL.MultMatrix(transform.localToWorldMatrix);

        // Draw lines
        GL.Begin(GL.LINES);
        GL.Color(Color.white);

        // To save memory/computation and be readable
        Vector3 pos;
        Vector3 nexPos;
        Vector3 center = poly.center.transform.position;

        // Draw every spring on the polygon
        for (int i = 0; i < poly.GetSprings().Count; i++) { 
            // Loops from the end of the array to the start
            int next = i+1 == poly.GetSprings().Count ? 0 : i+1;

            // Save current and next position
            pos = poly.GetSprings()[i].objectA.transform.position;
            nexPos = poly.GetSprings()[i].objectB.transform.position;

            // Create vertex points for edges
            GL.Vertex3(pos.x, pos.y, pos.z);
            GL.Vertex3(nexPos.x, nexPos.y, nexPos.z);
        }

        // Final point that wraps around the polygon from finish->start
        // GL.Vertex3(poly.points[i].x, poly.points[i].y, poly.points[i].z);

        // Finish drawing polygon and put matrix back on
        GL.End();
        GL.PopMatrix();
    }
    #endregion
};
