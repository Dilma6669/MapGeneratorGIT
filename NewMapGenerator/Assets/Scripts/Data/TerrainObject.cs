using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Data {

	public class TerrainObject : MonoBehaviour  {

		public int cubeUniqueID = -1;

		public int gridLocX = -1;
		public int gridLocZ = -1;
		public int gridLocY = -1;

		public int cubeLandType = -1;
		public int cubeList = -1;
		public int cubeType = -1;
		public int cubeRot  = -1;
		public int cubeSlopeType = -1;

		public int objReplaceable = -1;// this is if cube can be replaced by other cubes, 0 replaceable, 1 NOT -replaceable
		public int objDestructable = -1;// this is if cube can be removed either by code or player

		// objects on the cube
		public int objList = -1; // this is the attached objects to the cube
		public int objType = -1;
		public int objRot  = -1;

		// for destroying object
		public int health = 0;
		public int Growth = 0;

		public bool surfaceTop = false;
		public bool surfaceBottom = false;

		public bool cubeSetActive = true;

		public ScriptableObject terrainData;

		///////////////////////////////////


	//
	//	//rotate object 45 degrees 
	//	public bool rotation = false;
	//
	//	//spin object around by 90 degrees
	//	public bool spin = false;
	//
	//	//if the player can move onto this tile
	//	public bool traversable = false;
	//
	//	//if an event is possible to happen on moving onto this tile
	//	public bool Event = false;
	//
	//	//if there is an obstacle on terrain object to check
	//	public bool obstacleCheck = false;
	//
		//if this tile is in sight of the player this turn;
		public bool inSight = false;

	//
	//	//if this tile has been seen by the player
	//	public bool seen = false;
	//
	//	//if the player is on that tile
	//	public bool playerOnObject = false;
	//

		public List<Vector3> neighVects = new List<Vector3>();

		//	public bool _00 = false;
		public bool _01;
		//	public bool _02 = false;
		public bool _03;
		public bool _04;
		public bool _05;
		//	public bool _06 = false;
		public bool _07;
		//	public bool _08 = false;

		public bool _09;
		public bool _10;
		public bool _11;
		public bool _12;
		//public bool _13 = false; // our cube in the middle
		public bool _14;
		public bool _15;
		public bool _16;
		public bool _17;

		//	public bool _18 = false;
		public bool _19;
		//	public bool _20 = false;
		public bool _21;
		public bool _22;
		public bool _23;
		//	public bool _24 = false;
		public bool _25;
		//	public bool _26 = false;


		public Vector3 sunRiseVect;
		public Vector3 sunMidVect;
		public Vector3 sunSetVect;

		Vector3 posToShotRayFrom;

		Transform Container;

		Transform LowPoly;
		Transform HighPoly;

		//private MeshRenderer[] AllMeshes;
		//MeshRenderer[] LowPolyMeshes;
		//MeshRenderer[] HighPolyMeshes;


		GrowerObject[] GrowerObjects;


		//////////////////////////////////


		public void ConnectObjectLinks() {
			
			//// get all objects refernces to its own bits
			Container = transform;
			LowPoly = Container.transform.GetChild (1);
			HighPoly = Container.transform.GetChild (2);

			GrowerObjects = Container.transform.GetComponentsInChildren<GrowerObject> ();

			//// get all Meshes refernces to its own bits
			//AllMeshes = Container.transform.GetComponentsInChildren<MeshRenderer> ();
			//LowPolyMeshes = LowPoly.transform.GetComponentsInChildren<MeshRenderer> ();
			//HighPolyMeshes = HighPoly.transform.GetComponentsInChildren<MeshRenderer> ();

			// to turn off all high polys on startup
			StartCoroutine(RendererMeshes(0)); 
		}


			

		public void GetNeighbourConnections() {

			Vector3 ownVect = new Vector3(gridLocX, gridLocY, gridLocZ);

	//		neighVects.Add(new Vector3 (ownVect.x - 1, ownVect.z - 1, ownVect.y - 1));
			neighVects.Add(new Vector3 (ownVect.x + 0, ownVect.z - 1, ownVect.y - 1));
	//		neighVects.Add(new Vector3 (ownVect.x + 1, ownVect.z - 1, ownVect.y - 1));
	//
			neighVects.Add(new Vector3 (ownVect.x - 1, ownVect.z + 0, ownVect.y - 1));
			neighVects.Add(new Vector3 (ownVect.x + 0, ownVect.z + 0, ownVect.y - 1)); // directly below
			neighVects.Add(new Vector3 (ownVect.x + 1, ownVect.z + 0, ownVect.y - 1));
	//
	//		neighVects.Add(new Vector3 (ownVect.x - 1, ownVect.z + 1, ownVect.y - 1));
			neighVects.Add(new Vector3 (ownVect.x + 0, ownVect.z + 1, ownVect.y - 1));
	//		neighVects.Add(new Vector3 (ownVect.x + 1, ownVect.z + 1, ownVect.y - 1));

			/////////////////////////////////
			neighVects.Add(new Vector3 (ownVect.x - 1, ownVect.z - 1, ownVect.y + 0));
			neighVects.Add(new Vector3 (ownVect.x + 0, ownVect.z - 1, ownVect.y + 0)); // infront (south)
			neighVects.Add(new Vector3 (ownVect.x + 1, ownVect.z - 1, ownVect.y + 0));

			neighVects.Add(new Vector3 (ownVect.x - 1, ownVect.z + 0, ownVect.y + 0)); // side (west)
			//neighVects.Add(ownVect);
			neighVects.Add(new Vector3 (ownVect.x + 1, ownVect.z + 0, ownVect.y + 0)); // side (east)

			neighVects.Add(new Vector3 (ownVect.x - 1, ownVect.z + 1, ownVect.y + 0));
			neighVects.Add(new Vector3 (ownVect.x + 0, ownVect.z + 1, ownVect.y + 0)); // back (North)
			neighVects.Add(new Vector3 (ownVect.x + 1, ownVect.z + 1, ownVect.y + 0));
			/////////////////////////////////

	//		neighVects.Add(new Vector3 (ownVect.x - 1, ownVect.z - 1, ownVect.y + 1));
			neighVects.Add(new Vector3 (ownVect.x + 0, ownVect.z - 1, ownVect.y + 1));
	//		neighVects.Add(new Vector3 (ownVect.x + 1, ownVect.z - 1, ownVect.y + 1));
	//
			neighVects.Add(new Vector3 (ownVect.x - 1, ownVect.z + 0, ownVect.y + 1));
			neighVects.Add(new Vector3 (ownVect.x + 0, ownVect.z + 0, ownVect.y + 1)); // directly above
			neighVects.Add(new Vector3 (ownVect.x + 1, ownVect.z + 0, ownVect.y + 1));
	//
	//		neighVects.Add(new Vector3 (ownVect.x - 1, ownVect.z + 1, ownVect.y + 1));
			neighVects.Add(new Vector3 (ownVect.x + 0, ownVect.z + 1, ownVect.y + 1));
	//		neighVects.Add(new Vector3 (ownVect.x + 1, ownVect.z + 1, ownVect.y + 1));

			for( int i = 0; i < neighVects.Count; i++) {
				if (neighVects[i].x <= -1 || neighVects[i].y <= -1 || neighVects[i].z <= -1) {
					neighVects[i] = new Vector3 (- 1, - 1, - 1);
				}
			}
			/////////////////////////////////

		}


		public void CalculateCubeObjectGrowth() {

			int growthCount = 0;

			if (cubeSetActive && surfaceTop) {

				posToShotRayFrom = new Vector3 (neighVects [15].x, neighVects [15].z, neighVects [15].y);
			
				RaycastHit hit = new RaycastHit ();
				int layerMask = 1 << 8;
				if (!Physics.Raycast (posToShotRayFrom, sunRiseVect, out hit, Mathf.Infinity, layerMask)) {
					Debug.DrawLine (posToShotRayFrom, sunRiseVect, Color.green);
					growthCount += 1;
				}

				if (!Physics.Raycast (posToShotRayFrom, sunMidVect, out hit, Mathf.Infinity, layerMask)) {
					Debug.DrawLine (posToShotRayFrom, sunMidVect, Color.green);
					growthCount += 1;
				}

				if (!Physics.Raycast (posToShotRayFrom, sunSetVect, out hit, Mathf.Infinity, layerMask)) {
					Debug.DrawLine (posToShotRayFrom, sunSetVect, Color.green);
					growthCount += 1;
				}

				if (growthCount <= 0) {
					growthCount -= 1;
				}

				Growth += growthCount;
				CalculateGrowthInObjects (growthCount);
			}

		}


		private void CalculateGrowthInObjects(int growthCount) {

			foreach (GrowerObject obj in GrowerObjects) {
				obj.CalculateGrowth (growthCount);
			}
		}
			


		//////////
		void OnTriggerEnter(Collider other) {
			if (cubeSetActive) {
				if (other.tag == "Player") {
					inSight = true;
					StartCoroutine(RendererMeshes(0)); 			
				}
			}
		}

		void OnTriggerExit(Collider other) {
			if (cubeSetActive) {
				if (other.tag == "Player") {
					inSight = false;
					StartCoroutine(RendererMeshes(0)); 
				}
			}
		}

		private IEnumerator RendererMeshes(float time) {

			// High/Low poly optimization
			if (inSight) {
				// Grow the dirt and statics
				HighPoly.gameObject.SetActive(true);
	//			foreach (MeshRenderer mesh in HighPolyMeshes) {
	//				if (mesh.gameObject.tag != "SelectObject") {
	//					mesh.enabled = true;
	//				}
	//			}
				// turn off low poly meshes
				LowPoly.gameObject.SetActive(false);
	//			foreach (MeshRenderer mesh in LowPolyMeshes) {
	//				mesh.enabled = false;
	//			}
			} else {
				// Grow the dirt and statics
				LowPoly.gameObject.SetActive(true);
	//			foreach (MeshRenderer mesh in LowPolyMeshes) {
	//				if (mesh.gameObject.tag != "SelectObject") {
	//					mesh.enabled = true;
	//				}
	//			}
				// turn off high poly meshes
				HighPoly.gameObject.SetActive(false);
	//			foreach (MeshRenderer mesh in HighPolyMeshes) {
	//				if (mesh.gameObject.tag != "Stone") {
	//					mesh.enabled = false;
	//				}
	//			}
			}

			yield return new WaitForSeconds (time);
		}
	}

}
