using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Reflection;
using Managers;

namespace Builders.MapBuilders {

	public class TerrainDataFull_Builder : MonoBehaviour {

		private List<TerrainData_Full> SurfaceDataFullList = new List<TerrainData_Full> ();


		public List<TerrainData_Full> BuildFullData (List<Vector3> list) {

			List<Vector3> SurfaceDataSimpleList = list;

			int dataUniqueID = 0;
			// assign a new Full data object to each simple data object
				foreach (Vector3 dataLoc in SurfaceDataSimpleList) {

					TerrainData_Full fullData = ScriptableObject.CreateInstance<TerrainData_Full> ();

					fullData.cubeUniqueID = dataUniqueID += 1;

					fullData.gridLocX = (int)dataLoc.x;
					fullData.gridLocZ = (int)dataLoc.y;
					fullData.gridLocY = (int)dataLoc.z;
					Vector3 gridLocToPlace = new Vector3 (fullData.gridLocX, fullData.gridLocZ, fullData.gridLocY);;

					//fullData.cubeLandType = data.landType; // Have to work this out

					fullData.cubeList = 0; // defaults, just squares
					fullData.cubeType = 0; // defaults, just squares

					fullData.surfaceTop = true;

					RemoveDataFromWorld (gridLocToPlace);
					GameManager._GridManager.GridLocToGridObjLookup.Add (gridLocToPlace, fullData);
					SurfaceDataFullList.Add (fullData);
				}

			GameManager._CubeSocial.GetCubeNeighbours (SurfaceDataFullList);
			GameManager._CubeSocial.GetCubeConnections (SurfaceDataFullList);


				CalculateCubeSlopes ();

			return SurfaceDataFullList;

			}

		
		//////////////////////////////////////////
		/// This function is not properly complete.
		/// There are still niggly cube pieces that are not doing
		/// what I want them to be doing!
		public void CalculateCubeSlopes() {
		
			// Set all Slopes for cubes
			foreach (TerrainData_Full data in SurfaceDataFullList) {
		
				Vector3 dataLoc = new Vector3(data.gridLocX, data.gridLocZ, data.gridLocY);
		
				// skip the bottom floor layer
				if (data.gridLocY >= 1) {


					CalculateOutterSlopes (data);


					CalculateOutterCorners (data);


					CalculateInnerCorners (data); 

				}
			}
		}


		int RESLIST_CUBES = 0;

		int RESLIST_SLOPES_U = 1;
		int RESLIST_CORNER_INNER_U = 2;
		int RESLIST_CORNER_OUTTER_U = 3;

		int RESLIST_SLOPES_D = 4;
		int RESLIST_CORNER_INNER_D = 5;
		int RESLIST_CORNER_OUTTER_D = 6;


			////////////////////////////////////////
			// Making Inner Slope corners
		private bool CalculateInnerCorners(TerrainData_Full data) {
		
			bool replaceCube = false;
			// flat side facing ---SOUTH WEST
			if (data._10 == true && data._12 == true
				&& data._22 == false && data._09 == false && data._19 == false && data._21 == false) {
				data.cubeList = RESLIST_CORNER_INNER_U;
				data.cubeSlopeType = 0;
				data.cubeType = -1;
				data.cubeRot = 3;
				replaceCube = true;
			} 
			else if (data._10 == true && data._12 == true
				&& data._04 == false && data._09 == false) {
				data.cubeList = RESLIST_CORNER_INNER_D;
				data.cubeSlopeType = 1;
				data.cubeType = -1;
				data.cubeRot = 3;
				replaceCube = true;
			} 
			// flat side facing ---NORTH WEST
			else if (data._12 == true && data._16 == true
				&& data._22 == false && data._15 == false && data._21 == false && data._25 == false) {
				data.cubeList = RESLIST_CORNER_INNER_U;
				data.cubeSlopeType = 2;
				data.cubeType = -1;
				data.cubeRot = 0;
				replaceCube = true;
			}
			else if (data._12 == true && data._16 == true
				&& data._04 == false && data._15 == false) {
				data.cubeList = RESLIST_CORNER_INNER_D;
				data.cubeSlopeType = 3;
				data.cubeType = -1;
				data.cubeRot = 0;
				replaceCube = true;
			}
			// flat side facing ---NORTH EAST
			else if (data._14 == true && data._16 == true
				&& data._22 == false && data._17 == false && data._23 == false && data._25 == false) {
				data.cubeList = RESLIST_CORNER_INNER_U;
				data.cubeSlopeType = 4;
				data.cubeType = -1;
				data.cubeRot = 1;
				replaceCube = true;
			}
			else if (data._14 == true && data._16 == true
				&& data._04 == false && data._17 == false) {
				data.cubeList = RESLIST_CORNER_INNER_D;
				data.cubeSlopeType = 5;
				data.cubeType = -1;
				data.cubeRot = 1;
				replaceCube = true;
			}
			// flat side facing ---SOUTH EAST
			else if (data._10 == true && data._14 == true 
				&& data._22 == false && data._11 == false && data._19 == false && data._23 == false) {
				data.cubeList = RESLIST_CORNER_INNER_U;
				data.cubeSlopeType = 6;
				data.cubeType = -1;
				data.cubeRot = 2;
				replaceCube = true;
			} 
			else if (data._10 == true && data._14 == true 
				&& data._04 == false && data._11 == false) {
				data.cubeList = RESLIST_CORNER_INNER_D;
				data.cubeSlopeType = 7;
				data.cubeType = -1;
				data.cubeRot = 2;
				replaceCube = true;
			}
			return replaceCube;
		}
		
			///////////////////////////////////////////////////
			// Making OUTTER Slope corners
		private bool CalculateOutterCorners(TerrainData_Full data) {
		
			bool replaceCube = false;
			// flat side facing ---SOUTH WEST
			if (data._10 == true && data._12 == true 
				&& data._22 == false && data._19 == false && data._21 == false
				&& data._14 == false && data._15 == false && data._16 == false) {
				data.cubeList = RESLIST_CORNER_OUTTER_U;
				data.cubeSlopeType = 8;
				data.cubeType = 2;
				data.cubeRot = 1;
				replaceCube = true;
			}
			else if (data._10 == true && data._12 == true 
				&& data._04 == false && data._19 == false && data._21 == false
				&& data._14 == false && data._15 == false && data._16 == false) {
				data.cubeList = RESLIST_CORNER_OUTTER_D;
				data.cubeSlopeType = 9;
				data.cubeType = 3;
				data.cubeRot = 1;
				replaceCube = true;
			}
			// flat side facing ---NORTH WEST
			else if (data._12 == true && data._16 == true 
				&& data._22 == false && data._21 == false && data._25 == false
				&& data._10 == false && data._11 == false && data._14 == false) {
				data.cubeList = RESLIST_CORNER_OUTTER_U;
				data.cubeSlopeType = 10;
				data.cubeType = 2;
				data.cubeRot = 2;
				replaceCube = true;
			}
			else if (data._12 == true && data._16 == true 
				&& data._04 == false && data._21 == false && data._25 == false
				&& data._10 == false && data._11 == false && data._14 == false) {
				data.cubeList = RESLIST_CORNER_OUTTER_D;
				data.cubeSlopeType = 11;
				data.cubeType = 3;
				data.cubeRot = 2;
				replaceCube = true;
			}
			// flat side facing ---NORTH EAST
			else if (data._14 == true && data._16 == true 
				&& data._22 == false && data._23 == false && data._25 == false
				&& data._09 == false && data._10 == false && data._12 == false) {
				data.cubeList = RESLIST_CORNER_OUTTER_U;
				data.cubeSlopeType = 12;
				data.cubeType = 2;
				data.cubeRot = 3;
				replaceCube = true;
			}
			else if (data._14 == true && data._16 == true 
				&& data._04 == false && data._23 == false && data._25 == false
				&& data._09 == false && data._10 == false && data._12 == false) {
				data.cubeList = RESLIST_CORNER_OUTTER_D;
				data.cubeSlopeType = 13;
				data.cubeType = 3;
				data.cubeRot = 3;
				replaceCube = true;
			}
			// flat side facing ---SOUTH EAST
			else if (data._11 == true && data._10 == true 
				&& data._22 == false && data._19 == false && data._23 == false
				&& data._09 == false && data._12 == false && data._15 == false && data._16 == false && data._17 == false) {
				data.cubeList = RESLIST_CORNER_OUTTER_U;
				data.cubeSlopeType = 14;
				data.cubeType = 2;
				data.cubeRot = 0;
				replaceCube = true;
			} else if (data._11 == true && data._10 == true
				&& data._04 == false && data._19 == false && data._23 == false
				&& data._09 == false && data._12 == false && data._15 == false && data._16 == false && data._17 == false) {
				data.cubeList = RESLIST_CORNER_OUTTER_D;
				data.cubeSlopeType = 15;
				data.cubeType = 3;
				data.cubeRot = 0;
				replaceCube = true;
			}

			return replaceCube;
		}
			

			///////////////////////////////////////////////////
			// Making All Slopes
		private bool CalculateOutterSlopes(TerrainData_Full data) {
		
			bool replaceCube = false;
		
			// flat side facing 
			// ---SOUTH and DOWN
			if (data._10 == true
				&& data._16 == false && data._22 == false && data._25 == false) { // if cube has front(south) but no back(north)
				data.cubeList = RESLIST_SLOPES_U;
				data.cubeSlopeType = 16;
				data.cubeType = 0;
				data.cubeRot = 0;
				if (data._14 == false && data._19 == false) { // weird edge case, slopes on ends of rows
					data.cubeRot = 1;
					data.cubeSlopeType = 17;
				}
				if (data._12 == false && data._19 == false) { // weird edge case, slopes on ends of rows
					data.cubeRot = 3;
					data.cubeSlopeType = 18;
				}
				replaceCube = true;
			}
			// --SOUTH and UP
			else if (data._10 == true
				&& data._16 == false && data._04 == false && data._07 == false) { // if cube has top, no bottm, no diag front bottom
				data.cubeList = RESLIST_SLOPES_D;
				data.cubeSlopeType = 19;
				data.cubeType = 1;
				data.cubeRot = 0;
				if (data._14 == false && data._19 == false) { // weird edge case, slopes on ends of rows
					data.cubeRot = 1;
					data.cubeSlopeType = 20;
				}
				replaceCube = true;
			}
		
			// flat side facing 
			// ---WEST and DOWN
			else if (data._12 == true
				&& data._14 == false && data._22 == false && data._23 == false) { // if cube has front(west) but no back(east)
				data.cubeList = RESLIST_SLOPES_U;
				data.cubeSlopeType = 21;
				data.cubeType = 0;
				data.cubeRot = 1;
				if (data._10 == false && data._21 == false) { // weird edge case, slopes on ends of rows
					data.cubeRot = 2;
					data.cubeSlopeType = 22;
				}
				replaceCube = true;
			}
			// --WEST and UP
			else if (data._12 == true
				&& data._14 == false && data._04 == false && data._05 == false) { // if cube has top, no bottm, no diag front bottom
				data.cubeList = RESLIST_SLOPES_D;
				data.cubeSlopeType = 23;
				data.cubeType = 1;
				data.cubeRot = 1;
				if (data._10 == false && data._21 == false) { // weird edge case, slopes on ends of rows
					data.cubeRot = 2;
					data.cubeSlopeType = 24;
				}
				replaceCube = true;
			}
		
			// flat side facing 
			//---NORTH and DOWN
			else if (data._16 == true
				&& data._10 == false && data._22 == false && data._19 == false) { // if cube has front(north) but no back(soth)
				data.cubeList = RESLIST_SLOPES_U;
				data.cubeSlopeType = 25;
				data.cubeType = 0;
				data.cubeRot = 2;
				if (data._12 == false && data._25 == false) { // weird edge case, slopes on ends of rows
					data.cubeRot = 3;
					data.cubeSlopeType = 26;
				}
				replaceCube = true;
			}
			// --NORTH and UP
			else if (data._16 == true
				&& data._10 == false && data._04 == false && data._01 == false) { // if cube has top, no bottm, no diag front bottom
				data.cubeList = RESLIST_SLOPES_D;
				data.cubeSlopeType = 27;
				data.cubeType = 1;
				data.cubeRot = 2;
				if (data._12 == false && data._25 == false) { // weird edge case, slopes on ends of rows
					data.cubeRot = 3;
					data.cubeSlopeType = 28;
				}
				replaceCube = true;
			}
		
			// flat side facing ---EAST 
			// ---EAST and DOWN
			else if (data._14 == true
				&& data._12 == false && data._22 == false && data._21 == false) { // if cube has front(east) but no back(west)
				data.cubeList = RESLIST_SLOPES_U;
				data.cubeSlopeType = 29;
				data.cubeType = 0;
				data.cubeRot = 3;
				if (data._10 == false && data._23 == false) { // weird edge case, slopes on ends of rows
					data.cubeRot = 0;
					data.cubeSlopeType = 30;
				}
				replaceCube = true;
			} 
			// --EAST and UP
			else if (data._14 == true
				&& data._12 == false && data._04 == false && data._03 == false) { // if cube has top, no bottm, no diag front bottom
				data.cubeList = RESLIST_SLOPES_D;
				data.cubeSlopeType = 31;
				data.cubeType = 1;
				data.cubeRot = 3;
				if (data._10 == false && data._23 == false) { // weird edge case, slopes on ends of rows
					data.cubeRot = 0;
					data.cubeSlopeType = 32;
				}
				replaceCube = true;
			}
			return replaceCube;
		}


		// A general funtion to remove data and its connections
		public void RemoveDataFromWorld(Vector3 dataLoc) {

			Destroy (GameManager._GridManager.GridLocToGridObjLookup[dataLoc] as GameObject);

			GameManager._GridManager.GridLocToGridObjLookup.Remove (dataLoc);
		}
		//////////////////////////
	}
}
