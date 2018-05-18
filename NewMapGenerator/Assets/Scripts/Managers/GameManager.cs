using UnityEngine;



namespace Managers {
	
	public class GameManager : MonoBehaviour {

		public static MapManager _MapManager;
		public static GridManager _GridManager;
		public static PlayerManager _PlayerManager;
		public static SunManager _SunManager;
		public static Notifications _Notification;

		void Awake() {
			
			_MapManager     = FindObjectOfType<MapManager>();
			_PlayerManager  = FindObjectOfType<PlayerManager>();
			_SunManager     = FindObjectOfType<SunManager>();
			_Notification   = FindObjectOfType<Notifications>();

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
