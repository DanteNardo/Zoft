using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedVector3 {
	#region Members
	private Vector3 vector3;
	private float limit;
	#endregion

	#region Properties
	public float x { 
		get { return vector3.x; } 
		private set {
			if (Mathf.Abs(x) < limit) {
				vector3 = new Vector3(0, y, z);
			}
			else { 
				vector3 = new Vector3(x, y, z);
			}
		}
	}

	public float y {
		get { return vector3.y; } 
		private set {
			if (Mathf.Abs(y) < limit) {
				vector3 = new Vector3(x, 0, z);
			}
			else { 
				vector3 = new Vector3(x, y, z);
			}
		}
	}

	public float z { 
		get { return vector3.z; } 
		private set {
			if (Mathf.Abs(z) < limit) {
				vector3 = new Vector3(x, y, 0);
			}
			else { 
				vector3 = new Vector3(x, y, z);
			}
		}
	}
	#endregion

	#region Methods
	public LimitedVector3(float limit) {
		vector3 = Vector3.zero;
		this.limit = limit;
		Debug.Log("Vector3:" + vector3.ToString() + "____Limit:" + limit);
	}

	public Vector3 GetVector3() {
		return vector3;
	}

	public void NewVector3(Vector3 vector3) {
		NewVector3(vector3.x, vector3.y, vector3.z);
	}

	public void AddVector3(Vector3 vector3) {
		AddVector3(vector3.x, vector3.y, vector3.z);
	}

	public void NewVector3(float x, float y, float z) {
		this.x = x;
		this.y = y;
		this.z = z;
		vector3 = new Vector3(this.x, this.y, this.z);
	}

	public void AddVector3(float x, float y, float z) {
		this.x += x;
		this.y += y;
		this.z += z;
	}
	#endregion
}
