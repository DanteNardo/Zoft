using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour {
    #region Members
    public Color polygonColor;
	#endregion

	#region Methods
	public void SetColor(string hex) {
		bool passed = ColorUtility.TryParseHtmlString(hex, out polygonColor);
		if (passed == false) {
			Debug.LogError("Invalid HTML Hex String");
			polygonColor = Color.red;
		}
	}
	#endregion
}
