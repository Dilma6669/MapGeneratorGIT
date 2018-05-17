using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using Managers;


namespace Builders.MapBuilders {

	public class TerrainCubes_Builder : MonoBehaviour {

		MapSettings _mapSettings;

		// the different surfaces to add objects blah blah
		public Dictionary<Vector3, TerrainObject> surfacesTop = new Dictionary<Vector3, TerrainObject>();

		void Start() {
			_mapSettings = GameManager._MapManager._mapSettings;
		}

		public void InstantiateCubes(List<TerrainData_Full> list) {

			List<TerrainData_Full> SurfaceDataFullList = list;

			foreach (TerrainData_Full data in SurfaceDataFullList) {

				Vector3 cubeLoc = new Vector3 (data.gridLocX, data.gridLocZ, data.gridLocY);

				if (!surfacesTop.ContainsKey(cubeLoc)) {

					GameObject GodNode = GameManager._CubeBuilder.BuildMeAFuckingCube (data);

					TerrainObject newScript = GodNode.GetComponent<TerrainObject> ();

					GodNode.transform.eulerAngles = new Vector3 (GodNode.transform.eulerAngles.x, GetAngle (newScript.cubeRot), 0);
					GodNode.transform.SetParent (this.transform);
					int sizeOfCubes = _mapSettings.sizeOfCubes;
					GodNode.transform.localScale = new Vector3 (sizeOfCubes, sizeOfCubes, sizeOfCubes);
					Vector3 worldLoc = (Vector3)GameManager._GridManager.GridLocToWorldLocLookup [cubeLoc];
					GodNode.transform.position = worldLoc;

					surfacesTop.Add (cubeLoc, newScript);
				}
			}
		}


		private int GetAngle(int angle) {

			if (angle == -1) {
				angle = Random.Range (0, 4);
			}

			switch (angle) {
			case 0:
				angle = 0;
				break;
			case 1:
				angle = 90;
				break;
			case 2:
				angle = 180;
				break;
			case 3:
				angle = 270;
				break;
			default:
				angle = 0;
				break;
			}
			return angle;
		}
	}
}
