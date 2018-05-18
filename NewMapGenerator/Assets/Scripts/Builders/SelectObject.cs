using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class SelectObject : MonoBehaviour {

	public TerrainObject terrainscript;

	void OnMouseOver() {
		
	if (terrainscript.inSight) {
		
			GetComponent<MeshRenderer> ().enabled = true;

			if (Input.GetMouseButtonDown (1)) {
				Debug.Log ("mouse right click");
			}
		}
	}

	void OnMouseExit() {

		GetComponent<MeshRenderer> ().enabled = false;
	}
}
