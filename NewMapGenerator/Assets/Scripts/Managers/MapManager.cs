using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Builders;
using Data;

namespace Managers {

	public class MapManager : GameManager {

		public MapSettings _mapSettings;

		public static TerrainDataSimple_Builder _TerrainDataSimple_Builder;
		public static TerrainDataFull_Builder _TerrainDataFull_Builder;
		public static TerrainCubes_Builder _TerrainCubes_Builder;
		public static CubeBuilder _CubeBuilder;
		public static CubeSocial _CubeSocial;

		protected int numMapPiecesXZ;
		protected int numMapPiecesY;
		protected int sizeOfMapPieces;
		protected int heightOfMapPieces;
		protected int totalXZCubes;
		protected int totalYCubes;
		protected int sizeOfCubes;
		protected int terrainDensityPercent;
		protected int waterLayerLevel;
		protected int hillMountainRatio;

		private List<Vector3> MapNodeGridLocLookup = new List<Vector3>();

		private int[] mapLandTypeArray;


		void Awake() {
			
			_mapSettings = FindObjectOfType<MapSettings> ();

			_GridManager    			= FindObjectOfType<GridManager> ();
			_TerrainDataSimple_Builder 	= FindObjectOfType<TerrainDataSimple_Builder> ();
			_TerrainDataFull_Builder 	= FindObjectOfType<TerrainDataFull_Builder> ();
			_TerrainCubes_Builder 		= FindObjectOfType<TerrainCubes_Builder> ();
			_CubeBuilder 				= FindObjectOfType<CubeBuilder> ();
			_CubeSocial 				= FindObjectOfType<CubeSocial> ();
		}

		void Start() {
			
			numMapPiecesXZ = _mapSettings.numMapPiecesXZ;
			numMapPiecesY = _mapSettings.numMapPiecesY;
			sizeOfMapPieces = _mapSettings.sizeOfMapPieces;
			heightOfMapPieces = _mapSettings.heightOfMapPieces;

			totalXZCubes = numMapPiecesXZ * sizeOfMapPieces;
			totalYCubes = numMapPiecesY * heightOfMapPieces;

			sizeOfCubes = _mapSettings.sizeOfCubes;
			terrainDensityPercent = _mapSettings.terrainDensityPercent;
			waterLayerLevel = _mapSettings.waterLayerLevel;
			hillMountainRatio = _mapSettings.hillMountainRatio;

			// creating map array (makes one node at top grass surface)
			int layerSize = (numMapPiecesXZ * numMapPiecesXZ);
			int mapSize = (layerSize * numMapPiecesY);
			int nodeStart = Random.Range(mapSize - (layerSize*2) , mapSize - layerSize) ;
			mapLandTypeArray = new int[mapSize];
			mapLandTypeArray [nodeStart] = 1; // 0 is nothing, 1 is Dirt, 2 is Dessert.. etc.
			//mapLandTypeArray [235] = 1; // 0 is nothing, 1 is Dirt, 2 is Dessert.. etc.
			//mapLandTypeArray [345] = 1; // 0 is nothing, 1 is Dirt, 2 is Dessert.. etc.
			//mapLandTypeArray [265] = 1; // 0 is nothing, 1 is Dirt, 2 is Dessert.. etc.
			//mapLandTypeArray [275] = 1; // 0 is nothing, 1 is Dirt, 2 is Dessert.. etc.





	//		for (int i = layerSize; i <= (layerSize * 2); i++) {
	//			mapLandTypeArray [i] = 1;
	//		}


		}
			


		public void BuildMapPieces() {

			//////////
			//////////////////
			/// Section 1. Builds the entire GRID for the whole map. using GridManager
			/////////////////
			////////


			// these are the bottom left corner axis for EACH map node
			int startGridLocX = 0;
			int startGridLocZ = 0;
			int startGridLocY = 0; // starting height of each new layer, 0, 10, 20. etc

			_Notification.GameUpdates (1, totalXZCubes,  totalXZCubes, totalYCubes);

			// Load each Y layer of grids in a loop, not nessacary but just did it this way for some reason
			for (int mapLayer = 1; mapLayer <= numMapPiecesY; mapLayer++) {

				// build map Layer grid locations
				MapNodeGridLocLookup.Clear ();
				_GridManager.BuildGridLocations (startGridLocX, startGridLocZ, startGridLocY);

				startGridLocX = 0;
				startGridLocZ = 0;
				startGridLocY = heightOfMapPieces * mapLayer;

				_Notification.GameUpdates (3, "Grid Level:", mapLayer);
			}

			_Notification.GameUpdates (2, "ALL GRID LAYERS LOADING:");


			//////////
			//////////////////
			/// Section 2. Put multiple nodes in GRID (to attach map pieces too)
			/////////////////
			////////


			// put nodes in center points of where map pieces will be placed in map Layer, on bottom 'floor' cube layer. e.g. 5, 5, 0. 15, 5, 10.
			BuildMapPieceNodes ();

			_Notification.GameUpdates (2, "Build Map Piece Nodes:");


			//////////
			//////////////////
			/// Section 3. This builds the data for the entire landscape (All map pieces)
			/// It runs in 3 stages:
			/// 1. Creates absolute bare bones 'simple' scripts with only GRID locations, and calculate if each cube is 'surface'
			/// 2. Replace all 'surface' cubes with more complex 'full' scripts that handle better functionality.
			/// 3. Create actually inGame cube objects from these 'full' scripts.
			/////////////////
			////////


			// Build the Full Data
			BuildTerrain ();

			_Notification.GameUpdates (2, "Data setup for Terrain:");


			// Place objects on terrain
			//PlaceTerrainObjects ();

			//GameManager._Notification.GameUpdates (2, "Placing Terrain objects:");

		}


		private void BuildMapPieceNodes() {
			
			int nodeGridLocX;
			int nodeGridLocZ;
			int nodeGridLocY;

			int halfMapSize = (sizeOfMapPieces / 2);

			for (int y = 0; y < numMapPiecesY; y++) {
				
				nodeGridLocY = halfMapSize + (sizeOfMapPieces * y);

				for (int z = 0; z < numMapPiecesXZ; z++) {

					nodeGridLocZ = halfMapSize + (sizeOfMapPieces * z);

					for (int x = 0; x < numMapPiecesXZ; x++) {

						nodeGridLocX = halfMapSize + (sizeOfMapPieces * x);
						Vector3 gridLoc = new Vector3 (nodeGridLocX, nodeGridLocY, nodeGridLocZ);
						//Debug.Log("vector: X: " + nodeGridLocX + " Y: " + nodeGridLocY + " Z: " + nodeGridLocZ);

						MapNodeGridLocLookup.Add (gridLoc);
					}
				}
			}
		}


		private void BuildTerrain() {

			// Build Simple data
			BuildTerrainDataSimple ();
			List<Vector3> SurfaceDataSimpleList = _TerrainDataSimple_Builder.GetSimpleSurfaceDataList ();

			// Build Full data
			List<TerrainData_Full> SurfaceDataFullList = _TerrainDataFull_Builder.BuildFullData (SurfaceDataSimpleList);

			// Build Cubes
			_TerrainCubes_Builder.InstantiateCubes (SurfaceDataFullList);
		}


		private void BuildTerrainDataSimple() {

			// Build the terrain Data ( the hills ontop of map node) in map pieces (chunks)
			int startX = 0;
			int startZ = 0;
			int startY = 0;

			int finishX = 0;
			int finishZ = 0;
			int finishY = 0;

			int halfMapSize = (sizeOfMapPieces / 2);

			for (int i = 0; i < MapNodeGridLocLookup.Count; i++) {
				
				int nodeCount = i;
				int landType = mapLandTypeArray [nodeCount];

				if (landType == 1) {

					Vector3 gridLoc = MapNodeGridLocLookup [nodeCount];

					startX = (int)gridLoc.x - halfMapSize;
					startZ = (int)gridLoc.z - halfMapSize;
					startY = (int)gridLoc.y - halfMapSize;

					finishX = (int)gridLoc.x + halfMapSize;
					finishZ = (int)gridLoc.z + halfMapSize;
					finishY = (int)gridLoc.y + halfMapSize;

					_TerrainDataSimple_Builder.BuildSimpleData (landType, gridLoc, startX, startZ, startY, finishX, finishZ, finishY);
				}
			}
		}
			
	}
}
