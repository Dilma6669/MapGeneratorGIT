using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Data {
		
	public class TerrainData_Full : ScriptableObject {

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

		//if this tile is in sight of the player this turn;
		public bool inSight = false;

		//
		//if this tile has been seen by the player
		public bool cubeSeen = false;
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


		//////////////////////////////////


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
	}

}
