using UnityEngine;
using Builders.MapBuilders;


namespace Managers {
	
	public class GameManager : MonoBehaviour {

		public static MapManager _MapManager;

		public static PlayerManager _PlayerManager;
		public static SunManager _SunManager;
		public static Notifications _Notification;


		public static GridManager _GridManager;
		public static TerrainDataSimple_Builder _TerrainDataSimple_Builder;
		public static TerrainDataFull_Builder _TerrainDataFull_Builder;
		public static TerrainCubes_Builder _TerrainCubes_Builder;
		public static CubeBuilder _CubeBuilder;
		public static CubeSocial _CubeSocial;

		void Awake() {
			
			_MapManager     = FindObjectOfType<MapManager>();
			_PlayerManager  = FindObjectOfType<PlayerManager>();
			_SunManager     = FindObjectOfType<SunManager>();
			_Notification   = FindObjectOfType<Notifications>();


			_GridManager    			= FindObjectOfType<GridManager> ();
			_TerrainDataSimple_Builder 	= FindObjectOfType<TerrainDataSimple_Builder> ();
			_TerrainDataFull_Builder 	= FindObjectOfType<TerrainDataFull_Builder> ();
			_TerrainCubes_Builder 		= FindObjectOfType<TerrainCubes_Builder> ();
			_CubeBuilder 				= FindObjectOfType<CubeBuilder> ();
			_CubeSocial 				= FindObjectOfType<CubeSocial> ();
		}


		void Start() {
			BuildTerrain();
		}



		public void BuildTerrain() {
			_MapManager.BuildMapPieces();
			_Notification.GameUpdates (0, "MAPS SUCCESSFULLY BUILT <<<<<<<---------------------");
			StartGame ();
		}
			

		public void StartGame() {
			if (_PlayerManager != null) {
				_PlayerManager.CreatePlayer ();
			}
			_Notification.GameUpdates (0, "Game has Started");
		}
	}
}
