using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrowerObject : MonoBehaviour {

	public bool cover;
	public bool grassObject;
	public bool treeObject;

//	private float posGrowthRate = 0;
//	private float targetPos = 0;

	private float scaleGrowthRate = 0;
	private float targetScale = 0;


	public int Growth = -1;
	public int startGrowth = 0;

	List<Transform> blades = new List<Transform>();


	public void SetProperties () {

//		posGrowthRate = 0.01f;
//		targetPos = 0f;
		scaleGrowthRate = 0.01f;
		targetScale = 1.0f;

		if (cover) {
			transform.localScale = new Vector3 (0, 0, 0);
		}
			
		if (grassObject) {

			transform.localPosition = new Vector3 (0, -0.05f, 0);
			transform.localScale = new Vector3 (0.8f, 1, 0.8f);

			// stupid unity bug fix, to not include parent node
			Transform[] bladesPre = transform.GetChild (0).GetComponentsInChildren<Transform> ();
			for (int i = 1; i < bladesPre.Length; i++) {
				if (bladesPre [i] != null) {
					blades.Add (bladesPre [i]);
				}
			}

			foreach (Transform blade in blades) {
//				float transScaleX = blade.localScale.x;
				float transScaleY = blade.localScale.y;
				float transScaleZ = blade.localScale.z;

				blade.localScale = new Vector3 (0, transScaleY, transScaleZ);
			}
		}
	}

	public void CalculateGrowth (int growthCount)
	{
		Growth += growthCount;

		for (int i = 0; i < growthCount; i++) {

			if (cover) {
				if (Growth >= 5) {
					Vector3 newScale = new Vector3 (1, 1, 1);
					transform.localScale = newScale;
				} else if (Growth < 5) {
					Vector3 newScale = new Vector3 (0, 0, 0);
					transform.localScale = newScale;
				}
			}
				
//
//			// Position ///
//			if (transPosY != targetPos) {
//				if (transPosY < targetPos) {
//					transPosY += posGrowthRate;
//				} else if (transPosY > targetPos) {
//					transPosY -= posGrowthRate;
//				}
//			}
//			if (transPosX != targetPos) {
//				if (transPosX < targetPos) {
//					transPosX += posGrowthRate;
//				} else if (transPosX > targetPos) {
//					transPosX -= posGrowthRate;
//				}
//			}
//			if (transPosZ != targetPos) {
//				if (transPosZ < targetPos) {
//					transPosZ += posGrowthRate;
//				} else if (transPosZ > targetPos) {
//					transPosZ -= posGrowthRate;
//				}
//			}
//			Vector3 newPos = new Vector3 (transPosX, transPosY, transPosZ);
//			transform.localPosition = newPos;
			/////

			// For the Grass Blades
			if (grassObject) {
			
				foreach (Transform blade in blades) {
				
					float transScaleX = blade.localScale.x;
					float transScaleY = blade.localScale.y;
					float transScaleZ = blade.localScale.z;

					if (transScaleX != transScaleY) {
						if (transScaleX < transScaleY) {
							transScaleX += scaleGrowthRate;
						} else if (transScaleX > transScaleY) {
							transScaleX -= scaleGrowthRate;
						}

						blade.localScale = new Vector3 (transScaleX, transScaleY, transScaleZ);
					}
				}
			}

			if (treeObject) {

				// Grower Object Container
				float transScaleX = transform.localScale.x;
				float transScaleY = transform.localScale.y;
				float transScaleZ = transform.localScale.z;
		
//				float transPosX = transform.localPosition.x;
//				float transPosY = transform.localPosition.y;
//				float transPosZ = transform.localPosition.z;
		
					// Scale ///
				if (transScaleY != targetScale) {
					if (transScaleY < targetScale) {
						transScaleY += scaleGrowthRate;
					} else if (transScaleY > targetScale) {
						transScaleY -= scaleGrowthRate;
					}
				}
				float tempGrowth = scaleGrowthRate / 5;
				if (transScaleX != targetScale) {
					if (transScaleX < targetScale) {
						transScaleX += tempGrowth;
					} else if (transScaleX > targetScale) {
						transScaleX -= tempGrowth;
					}
				}
				if (transScaleZ != targetScale) {
					if (transScaleZ < targetScale) {
						transScaleZ += tempGrowth;
					} else if (transScaleZ > targetScale) {
						transScaleZ -= tempGrowth;
					}
				}
				Vector3 newSize = new Vector3 (transScaleX, transScaleY, transScaleZ);
				transform.localScale = newSize;
				////////

			}
		}
	}
}
