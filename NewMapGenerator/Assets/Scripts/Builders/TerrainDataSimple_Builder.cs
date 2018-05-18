using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using Managers;

namespace Builders {

	public class TerrainDataSimple_Builder : MapManager {


		private List<Vector3> SurfaceDataSimpleList = new List<Vector3>();

		// stores the objects to be built
		private List<List<int[]>> hillPreDataList = new List<List<int[]>>();


		public List<Vector3> GetSimpleSurfaceDataList() {
			return SurfaceDataSimpleList;
		}



		public void BuildSimpleData(int landType, Vector3 nodeGridLoc, int startX, int startZ, int startY, int finishX, int finishZ, int finishY) {

			BuildHillDataOnMapPiece (landType, startX, startZ, startY, finishX, finishZ, finishY);

			//RemoveDataDensity ();

			CreateSurfaceList ();

		}



		private void BuildHillDataOnMapPiece(int landType, int startX, int startZ, int startY, int finishX, int finishZ, int finishY) {

			int offset = 0;

			int peaksCount = Random.Range (1, 2);//Random.Range (0, halfSizeMap + 1);

			_Notification.GameUpdates (4, "Number hills/mountains in node:", peaksCount );

			Debug.Log ("startY: " + startY + " FinishY: " + finishY);

			for (int i = 0; i < peaksCount; i++) {

				// create the first random node and beginng height
				int randomX = (int)Random.Range (startX, finishX);
				int randomZ = (int)Random.Range (startZ, finishZ);
				int randomY = Random.Range (startY, finishY);
				Debug.Log ("hilll being added  randoms X " + randomX + " Z " + randomZ + "  Y " + randomY);

				BuildHillData (randomX, randomZ, randomY);
			}

			for (int i = 0; i < hillPreDataList.Count; i++) {
				ExtractHillData (landType, hillPreDataList [i]);
			}
			hillPreDataList.Clear ();
		}


		private void BuildHillData(int posX, int posZ, int posY) {

			// whole hill container
			List<int[]> obj = new List<int[]>();

			int spreadMax = 0;

			int startX = 0, finishX = 0, startZ = 0, finishZ = 0;
			int lastStartX = 0, lastFinishX = 0, lastStartZ = 0, lastFinishZ = 0;

			if ((posY * 10) >= _mapSettings.hillMountainRatio) {
				Debug.Log ("Mountain");
				spreadMax = 1;
			} else {
				Debug.Log ("Hill");
				spreadMax = 3;
				if (posY > 1) {
					posY = (posY / 2);
				}
			}

			int spread = Random.Range (1, spreadMax + 1);

			int[] cubePreDataFirst = new int[4];
			cubePreDataFirst [0] = posX;	//posX
			cubePreDataFirst [1] = posZ;	//posZ
			cubePreDataFirst [2] = posY; 	//posY
			cubePreDataFirst [3] = 1; 		//surface

			obj.Add (cubePreDataFirst); // start, all following pos's are realtive to this starting point


			posY *= -1;

			// -1 is under initial beginnging node, y IS NOT the grid position, its the relative number to the original hill node
			for (int y = 0; y >= posY; y--) {

				// check to make sure layers always expand outward not inward
				startX = -spread;
				if (startX > lastStartX) {
					startX = lastStartX;
				}
				finishX = spread;
				if (finishX < lastFinishX) {
					finishX = lastFinishX;
				}
				startZ = -spread;
				if (startZ > lastStartZ) {
					startZ = lastStartZ;
				}
				finishZ = spread;
				if (finishZ < lastFinishZ) {
					finishZ = lastFinishZ;
				}

				// spread bottom layer across whole map
				if (y == posY) {
					startX = -totalXZCubes;
					startZ = -totalXZCubes;
					finishX = totalXZCubes;
					finishZ = totalXZCubes;

					// OR shifting layers to make cliffs, dont affect top layer
				} else if (y != 0) {

					int cliffs = Random.Range (0, 10);
					if (cliffs == 10) {

						// cliffs for X
						cliffs = Random.Range (0, 2);
						if (cliffs == 0) {
							startX = lastStartX;
							finishX = lastFinishX + spread;
						}
						if (cliffs == 1) {
							startX = lastStartX - spread;
							finishX = lastFinishX;
						}

						// cliffs for Z
						cliffs = Random.Range (0, 2);
						if (cliffs == 0) {
							startZ = lastStartZ;
							finishZ = lastFinishZ + spread;
						}
						if (cliffs == 1) {
							startZ = lastStartZ - spread;
							finishZ = lastFinishZ;
						}
					}
					//////
				}

				for (int z = startZ; z <= finishZ; z++) {
					for (int x = startX; x <= finishX; x++) {

						// Make all dirt cubes
						int[] cubePreDataNext = new int[4];
						cubePreDataNext [0] = x;		//posX
						cubePreDataNext [1] = z;		//posZ
						cubePreDataNext [2] = y; 		//posY
						cubePreDataNext [3] = 1; 		//make all surface

						if (x > lastStartX && x < lastFinishX && z > lastStartZ && z < lastFinishZ && y != posY) {

							cubePreDataNext [3] = 0; 		//make under surface cubes, not surface
						}

						obj.Add (cubePreDataNext);

					}
				}

				lastStartX = startX;
				lastFinishX = finishX;
				lastStartZ = startZ;
				lastFinishZ = finishZ;

				spread = spread + Random.Range (1, spreadMax + 1);
			}
			hillPreDataList.Add (obj);
		}


		public void ExtractHillData(int landType, List<int[]> obj) {

			// starting position
			int[] firstEntryData = obj [0];
			Vector3 startingPos =  new Vector3 (firstEntryData [0], firstEntryData [1], firstEntryData [2]);

			if (_GridManager.GridLocToWorldLocLookup [startingPos] != null) {

				int[] cubeDataFirst = new int[4];
				cubeDataFirst [0] = (int)startingPos.x;		//gridLocToPlace
				cubeDataFirst [1] = (int)startingPos.y;		//gridLocToPlace
				cubeDataFirst [2] = (int)startingPos.z;		//gridLocToPlace
				cubeDataFirst [3] = firstEntryData [3];		//surface

				CreateTerrainData_Simple (cubeDataFirst);

				// each object pos after first starting point
				for (int j = 1; j < obj.Count; j++) {
					int[] newEntryData = obj [j];

					Vector3 gridLocToPlace = new Vector3 (startingPos.x + newEntryData [0], startingPos.y + newEntryData [1], startingPos.z + newEntryData [2]);
					if (_GridManager.GridLocToWorldLocLookup [gridLocToPlace] != null) {

						int[] cubeDataNext = new int[5];
						cubeDataNext [0] = (int)gridLocToPlace.x;	//gridLocToPlace
						cubeDataNext [1] = (int)gridLocToPlace.y;	//gridLocToPlace
						cubeDataNext [2] = (int)gridLocToPlace.z;	//gridLocToPlace
						cubeDataNext [3] = newEntryData [3];		//surface

						CreateTerrainData_Simple (cubeDataNext);

					} 
				}
			}
		}



		public void CreateTerrainData_Simple(int[] cubeData) {

			Vector3 gridLocToPlace = new Vector3(cubeData [0], cubeData [1], cubeData [2]);

			// if any cubes already exist at this location, return
			if (_GridManager.GridLocToGridObjLookup [gridLocToPlace] != null) {
				return;
			}

			if (cubeData [3] == 1) {

				TerrainData_Simple newData = ScriptableObject.CreateInstance<TerrainData_Simple> ();

				newData.gridLocX = (int)gridLocToPlace.x;
				newData.gridLocZ = (int)gridLocToPlace.y;
				newData.gridLocY = (int)gridLocToPlace.z;

				newData.surface = cubeData [3];

				_GridManager.GridLocToGridObjLookup.Add (gridLocToPlace, newData);
			}
		}





		// Go through and randomly remove cubes to give the terrain natural crevases and caves
		public void RemoveDataDensity() {

			// Temporary list to put cubes in that need to be deleted
			List<Vector3> dataToRemoveList = new List<Vector3>();

			int numMapPiecesXZ = _mapSettings.numMapPiecesXZ;
			int numMapPiecesY = _mapSettings.numMapPiecesY;
			int totalXZCubes = _mapSettings.totalXZCubes;
			int totalYCubes = _mapSettings.totalYCubes;

			int holeCount = (numMapPiecesXZ * numMapPiecesXZ) * numMapPiecesY;

			for (int i = 0; i < holeCount; i++) {

				int locX = (Random.Range (0, totalXZCubes));
				int locZ = (Random.Range (0, totalXZCubes));
				int locY = (Random.Range (0, totalYCubes));

				Vector3 dataLoc = new Vector3 (locX, locZ, locY);

				int spread = Random.Range (2, 4);

				int	startX = (int)dataLoc.x - spread;
				int	startZ = (int)dataLoc.y - spread;
				int	startY = (int)dataLoc.z - spread;

				int	finishX = (int)dataLoc.x + spread;
				int	finishZ = (int)dataLoc.y + spread;
				int	finishY = (int)dataLoc.z + spread;


				int randomHoleShape = Random.Range (0, 2);


				for (int y = startY; y <= finishY; y++) {

					if (y == startY || y == finishY) {

						if (randomHoleShape == 0) {

							// Round shape
							startZ = startZ + 1;
							finishZ = finishZ - 1;
							startX = startX + 1;
							finishX = finishX - 1;

						} else if (randomHoleShape == 1) {

							// diagonal shape
							startZ = startZ - 1;
							finishZ = finishZ - 1;
							startX = startX - 1;
							finishX = finishX - 1;

						}
					} else {
						startZ = (int)dataLoc.y - spread;
						finishZ = (int)dataLoc.y + spread;
						startX = (int)dataLoc.x - spread;
						finishX = (int)dataLoc.x + spread;
					}

					for (int z = startZ; z <= finishZ; z++) {
						for (int x = startX; x <= finishX; x++) {

							// dont remove bottom layer cubes
							if( y != 0) {
							Vector3 dataToRemoveLoc = new Vector3 (x, z, y);
								if (_GridManager.GridLocToGridObjLookup [dataToRemoveLoc] != null) {
									TerrainData_Simple dataSimple = (TerrainData_Simple)_GridManager.GridLocToGridObjLookup [dataToRemoveLoc];

									if (z == startZ || z == finishZ || x == startX || x == finishX || y == startY || y == finishY) {
										dataSimple.surface = 1;
									} else {
										dataToRemoveList.Add (dataToRemoveLoc);
									}
								}
							}
						}
					}
				}
			}

			Debug.Log ("Hole.count: " + holeCount);

			foreach (Vector3 dataLoc in dataToRemoveList) {
				RemoveDataFromWorld (dataLoc);
			}
			dataToRemoveList.Clear ();
		}
		///////////////////////////


		// Put all relevent simple surface data into surface list to be made in ful scripts
		public void CreateSurfaceList() {

			foreach (TerrainData_Simple dataSimple in _GridManager.GridLocToGridObjLookup.Values) {
				if (dataSimple.surface == 1) {
					SurfaceDataSimpleList.Add (new Vector3 (dataSimple.gridLocX, dataSimple.gridLocZ, dataSimple.gridLocY));
				}
			}
		}
		//////////////////////////
		/// 



		// A general funtion to remove data and its connections
		public void RemoveDataFromWorld(Vector3 dataLoc) {

			_GridManager.GridLocToGridObjLookup.Remove (dataLoc);
		}
		//////////////////////////
		/// 
	}
}
