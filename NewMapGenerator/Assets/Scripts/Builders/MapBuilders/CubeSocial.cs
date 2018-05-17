using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

namespace Builders.MapBuilders {

	public class CubeSocial : MonoBehaviour {

		public void GetCubeNeighbours(List<TerrainData_Full> dataList) {

			// Get all cubes neighbours connections
			foreach (TerrainData_Full terrainScript in dataList) {
				terrainScript.GetNeighbourConnections ();
			}
		}



		public void GetCubeConnections(List<TerrainData_Full> dataList) {

			// Set relative neighbour booleans
			foreach (TerrainData_Full cubeScript in dataList) {

				int neighbourCount = 0;

				if (GameManager._GridManager.GridLocToGridObjLookup [cubeScript.neighVects [0]] != null) { // 
					cubeScript._01 = true;
					neighbourCount += 1;
				} else if (GameManager._GridManager.GridLocToWorldLocLookup [cubeScript.neighVects [0]] == null) { // fix for cubes at edge of maps
					cubeScript._01 = true;
				}

				if (GameManager._GridManager.GridLocToGridObjLookup [cubeScript.neighVects [1]] != null) { // 
					cubeScript._03 = true;
					neighbourCount += 1;
				} else if (GameManager._GridManager.GridLocToWorldLocLookup [cubeScript.neighVects [1]] == null) { // fix for cubes at edge of maps
					cubeScript._03 = true;
				}

				if (GameManager._GridManager.GridLocToGridObjLookup [cubeScript.neighVects [2]] != null) { // directy BELOW
					cubeScript._04 = true;
					neighbourCount += 1;
				} else if (GameManager._GridManager.GridLocToWorldLocLookup [cubeScript.neighVects [2]] == null) { // fix for cubes at edge of maps
					cubeScript._04 = true;
				}

				if (GameManager._GridManager.GridLocToGridObjLookup [cubeScript.neighVects [3]] != null) { // 
					cubeScript._05 = true;
					neighbourCount += 1;
				} else if (GameManager._GridManager.GridLocToWorldLocLookup [cubeScript.neighVects [3]] == null) { // fix for cubes at edge of maps
					cubeScript._05 = true;
				}

				if (GameManager._GridManager.GridLocToGridObjLookup [cubeScript.neighVects [4]] != null) { // 
					cubeScript._07 = true;
					neighbourCount += 1;
				} else if (GameManager._GridManager.GridLocToWorldLocLookup [cubeScript.neighVects [4]] == null) { // fix for cubes at edge of maps
					cubeScript._07 = true;
				}

				if (GameManager._GridManager.GridLocToGridObjLookup [cubeScript.neighVects [5]] != null) { // front left diagonal
					cubeScript._09 = true;
					neighbourCount += 1;
				} else if (GameManager._GridManager.GridLocToWorldLocLookup [cubeScript.neighVects [5]] == null) { // fix for cubes at edge of maps
					cubeScript._09 = true;
				}

				if (GameManager._GridManager.GridLocToGridObjLookup [cubeScript.neighVects [6]] != null) { // directly infront
					cubeScript._10 = true;
					neighbourCount += 1;
				} else if (GameManager._GridManager.GridLocToWorldLocLookup [cubeScript.neighVects [6]] == null) { // fix for cubes at edge of maps
					cubeScript._10 = true;
				}

				if (GameManager._GridManager.GridLocToGridObjLookup [cubeScript.neighVects [7]] != null) { // front right diagional
					cubeScript._11 = true;
					neighbourCount += 1;
				} else if (GameManager._GridManager.GridLocToWorldLocLookup [cubeScript.neighVects [7]] == null) { // fix for cubes at edge of maps
					cubeScript._11 = true;
				}

				if (GameManager._GridManager.GridLocToGridObjLookup [cubeScript.neighVects [8]] != null) { // directy left
					cubeScript._12 = true;
					neighbourCount += 1;
				} else if (GameManager._GridManager.GridLocToWorldLocLookup [cubeScript.neighVects [8]] == null) { // fix for cubes at edge of maps
					cubeScript._12 = true;
				}

				if (GameManager._GridManager.GridLocToGridObjLookup [cubeScript.neighVects [9]] != null) { // directy right
					cubeScript._14 = true;
					neighbourCount += 1;
				} else if (GameManager._GridManager.GridLocToWorldLocLookup [cubeScript.neighVects [9]] == null) { // fix for cubes at edge of maps
					cubeScript._14 = true;
				}

				if (GameManager._GridManager.GridLocToGridObjLookup [cubeScript.neighVects [10]] != null) { // back left diagonal
					cubeScript._15 = true;
					neighbourCount += 1;
				} else if (GameManager._GridManager.GridLocToWorldLocLookup [cubeScript.neighVects [10]] == null) { // fix for cubes at edge of maps
					cubeScript._15 = true;
				}

				if (GameManager._GridManager.GridLocToGridObjLookup [cubeScript.neighVects [11]] != null) { // directy behind
					cubeScript._16 = true;
					neighbourCount += 1;
				} else if (GameManager._GridManager.GridLocToWorldLocLookup [cubeScript.neighVects [11]] == null) { // fix for cubes at edge of maps
					cubeScript._16 = true;
				}

				if (GameManager._GridManager.GridLocToGridObjLookup [cubeScript.neighVects [12]] != null) { // back right diagonal
					cubeScript._17 = true;
					neighbourCount += 1;
				} else if (GameManager._GridManager.GridLocToWorldLocLookup [cubeScript.neighVects [12]] == null) { // fix for cubes at edge of maps
					cubeScript._17 = true;
				}

				if (GameManager._GridManager.GridLocToGridObjLookup [cubeScript.neighVects [13]] != null) { // 
					cubeScript._19 = true;
					neighbourCount += 1;
				} else if (GameManager._GridManager.GridLocToWorldLocLookup [cubeScript.neighVects [13]] == null) { // fix for cubes at edge of maps
					cubeScript._19 = true;
				}

				if (GameManager._GridManager.GridLocToGridObjLookup [cubeScript.neighVects [14]] != null) { // 
					cubeScript._21 = true;
					neighbourCount += 1;
				} else if (GameManager._GridManager.GridLocToWorldLocLookup [cubeScript.neighVects [14]] == null) { // fix for cubes at edge of maps
					cubeScript._21 = true;
				}

				if (GameManager._GridManager.GridLocToGridObjLookup [cubeScript.neighVects [15]] != null) { // directy ABOVE
					cubeScript._22 = true;
					neighbourCount += 1;
				} else if (GameManager._GridManager.GridLocToWorldLocLookup [cubeScript.neighVects [15]] == null) { // fix for cubes at edge of maps
					cubeScript._22 = true;
				}

				if (GameManager._GridManager.GridLocToGridObjLookup [cubeScript.neighVects [16]] != null) { // directy below
					cubeScript._23 = true;
					neighbourCount += 1;
				} else if (GameManager._GridManager.GridLocToWorldLocLookup [cubeScript.neighVects [16]] == null) { // fix for cubes at edge of maps
					cubeScript._23 = true;
				}

				if (GameManager._GridManager.GridLocToGridObjLookup [cubeScript.neighVects [17]] != null) { // directy below
					cubeScript._25 = true;
					neighbourCount += 1;
				} else if (GameManager._GridManager.GridLocToWorldLocLookup [cubeScript.neighVects [17]] == null) { // fix for cubes at edge of maps
					cubeScript._25 = true;
				}
			}
		}

		}


}
