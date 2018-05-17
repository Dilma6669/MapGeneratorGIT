using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Builders.MapBuilders;


namespace Managers {

	public class GridManager : MonoBehaviour {

		MapSettings _mapSettings;

		public GameObject gridObjectPrefab; // Debugging purposes
		public GameObject spawnPrefab; // object that runs around world space creating the locations
		private GameObject spawn;

		public bool debugGridObjects; // debugging purposes

		public Hashtable GridLocToWorldLocLookup; 	// making a lookUp table for Grid locations matching world space locations
		public Hashtable GridLocToGridObjLookup; 	// making a lookUp table for objects located at Vector3 Grid locations


		public Vector3 sunRiseVect;
		public Vector3 sunMidVect;
		public Vector3 sunSetVect;


		// Use this for initialization
		void Start () {

			_mapSettings = GameManager._MapManager._mapSettings;

			GridLocToWorldLocLookup = new Hashtable ();
			GridLocToGridObjLookup = new Hashtable ();

			// SPAWN //
			spawn = Instantiate (spawnPrefab, transform, false);
			////////////////

		}
			

		public void BuildGridLocations(int startX, int startZ, int startY) {

			int gridLocX = startX;
			int gridLocZ = startZ;
			int gridLocY = startY;

			int sizeOfMapPieces = _mapSettings.sizeOfMapPieces;
			int numMapPiecesXZ = _mapSettings.numMapPiecesXZ;
			int heightOfMapPieces = _mapSettings.heightOfMapPieces;

			int worldSizeX = sizeOfMapPieces * numMapPiecesXZ;
			int worldSizeZ = sizeOfMapPieces * numMapPiecesXZ;
			int worldSizeY = startY + heightOfMapPieces;

			sunRiseVect = new Vector3 (gridLocX, worldSizeY, gridLocZ);
			sunMidVect = new Vector3 (worldSizeX/2, worldSizeY, worldSizeZ/2);
			sunSetVect = new Vector3 (worldSizeX, worldSizeY, worldSizeZ);

			float spawnPosX;
			float spawnPosZ;
			float spawnPosY;

			int sizeOfCubes = _mapSettings.sizeOfCubes;

			for (int y = startY; y < worldSizeY; y++) {

				spawnPosX = startX;
				spawnPosZ = startZ;
				spawnPosY = y * _mapSettings.sizeOfCubes;

				gridLocX = startX;
				gridLocZ = startZ;

				for (int z = 0; z < worldSizeZ; z++) {

					spawnPosX = startX;
					spawn.transform.localPosition = new Vector3 (spawnPosX, spawnPosY, spawnPosZ);

					gridLocX = startX;

					for (int x = 0; x < worldSizeX; x++) {

						// Create location positions
						///////////////////////////////
						// put vector location, eg, grid Location 0,0,0 and World Location 35, 0, 40 value pairs into hashmap for easy lookup
						Vector3 gridLoc = new Vector3 (gridLocX, gridLocZ, gridLocY);
						Vector3 worldLoc = new Vector3 (spawn.transform.localPosition.x, spawn.transform.localPosition.y, spawn.transform.localPosition.z);
						//Debug.Log ("Vector3 (gridLoc): x: " + gridLoc.x + " z: " +  gridLoc.z + " y: " +  gridLoc.y);
						//Debug.Log ("Vector3 (worldLoc): x: " + worldLoc.x + " y: " +  worldLoc.y + " z: " +  worldLoc.z);
						GridLocToWorldLocLookup.Add (gridLoc, worldLoc);
						/////////////////////////////////

						// Create empty objects at locations to see the locations (debugging purposes)
						////////////////////////////////////////
						if (debugGridObjects) {
							GameObject GridObject = Instantiate (gridObjectPrefab, spawn.transform, false);
							GridObject.transform.SetParent (this.gameObject.transform);
							GridObject.transform.localScale = new Vector3 (sizeOfCubes, sizeOfCubes, sizeOfCubes);
						}
						//////////////////////////////////////////

						spawnPosX += sizeOfCubes;
						spawn.transform.localPosition = new Vector3 (spawnPosX, spawnPosY, spawnPosZ);
						gridLocX += 1;
					}
					spawnPosZ += sizeOfCubes;
					gridLocZ += 1;
				}
				gridLocY += 1;
			}

			SetSunLocations ();
		}
	

		public void SetSunLocations() {

			GameManager._SunManager.SetSunPositions (new List<Vector3> {sunRiseVect, sunMidVect, sunSetVect});
		}
	}
		
}
