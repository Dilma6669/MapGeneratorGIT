using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using Managers;

namespace Builders.MapBuilders {

	public class CubeBuilder : MonoBehaviour {

		public GameObject emptyNode;

		// LOW POLYS
		public List<GameObject> LOW_Cube;
		public List<GameObject> LOW_Slope_U;
		public List<GameObject> LOW_CrnIn_U;
		public List<GameObject> LOW_CrnOut_U;

		public List<GameObject> LOW_Slope_D;
		public List<GameObject> LOW_CrnIn_D;
		public List<GameObject> LOW_CrnOut_D;

		public List<GameObject> LOW_Cube_Cover;
		public List<GameObject> LOW_Slope_Cover;
		public List<GameObject> LOW_CrnIn_Cover;
		public List<GameObject> LOW_CrnOut_Cover;



		// HIGH POLYS
		public List<GameObject> HIGH_Cube;
		public List<GameObject> HIGH_Slope_U;
		public List<GameObject> HIGH_CrnIn_U;
		public List<GameObject> HIGH_CrnOut_U;

		public List<GameObject> HIGH_Slope_D;
		public List<GameObject> HIGH_CrnIn_D;
		public List<GameObject> HIGH_CrnOut_D;



		public List<GameObject> HIGH_Cube_Cover;
		public List<GameObject> HIGH_Slope_Cover;
		public List<GameObject> HIGH_CrnIn_Cover;
		public List<GameObject> HIGH_CrnOut_Cover;


		// High Poly Objects
		public List<GameObject> HIGH_Stone01;

		public List<GameObject> HIGH_Cube_Grass;
		public List<GameObject> HIGH_Slope_Grass;
		public List<GameObject> HIGH_CrnIn_Grass;
		public List<GameObject> HIGH_CrnOut_Grass;

		public List<GameObject> Cube_Select;


		private List<SelectObject> selectObjects = new List<SelectObject>();

		// Materials
	//	public Material base01;
	//	public Material base02;
	//	public Material base03;
	//
	//	public Material cover01;
	//	public Material cover02;
	//	public Material cover03;
	//
	//	public Material stone01;
	//	public Material stone02;
	//	public Material stone03;


		private int cubeSize;

		int RESLIST_CUBES = 0;

		int RESLIST_SLOPES_U = 1;
		int RESLIST_CORNER_INNER_U = 2;
		int RESLIST_CORNER_OUTTER_U = 3;

		int RESLIST_SLOPES_D = 4;
		int RESLIST_CORNER_INNER_D = 5;
		int RESLIST_CORNER_OUTTER_D = 6;



		public GameObject BuildMeAFuckingCube(TerrainData_Full data) {

			int landType = 0; //script.cubeLandType; 
			int objList = data.cubeList;
			int objType = data.cubeType;

			// GodNode --> Container --> LowPoly  --> Static  --> StaticContainer01 --> SimpleCube
			//									  --> Growers --> GrowerContainer01 -->
			//												  --> GrowerContainer02 -->
			//						 --> HighPoly --> Static  --> StaticContainer01 --> Cube
			//									  --> Growers --> GrowerContainer01 --> Cover
			//												  --> GrowerContainer02 --> Grass
			//         --> SelectContainer --> CubeSelect

			//GameObject GodNode = Instantiate (emptyNode, transform, false);

			GameObject Container = Instantiate (emptyNode, transform, false);
			Container.name = ("Container");

			GameObject CubeSelect = Instantiate (Cube_Select[0], Container.transform, false); // cube Select Object
			CubeSelect.name = ("CubeSelect");
			selectObjects.Add(CubeSelect.GetComponent<SelectObject>());

			// Low Polys
			GameObject LowPoly = Instantiate (emptyNode, Container.transform, false);
			LowPoly.name = ("LowPoly");
			AttachLowPolys (LowPoly, landType, objList, objType);

			// High Polys
			GameObject HighPoly = Instantiate (emptyNode, Container.transform, false);
			HighPoly.name = ("HighPoly");
			AttachHighPolys (HighPoly, landType, objList, objType);

			AddContainerComponents (Container, data);
			selectObjects.Clear ();
			return Container;
		}



		private void AttachLowPolys(GameObject LowPoly, int landType, int objList, int objType) {

			GameObject SimpleCube = Instantiate (GetLOWCube (landType, objList), LowPoly.transform, false); // Simple Cube Object
			SimpleCube.name = ("SimpleCube");

			//		GameObject Growers = Instantiate (emptyNode, LowPoly.transform, false);
			//		GameObject GrowerContainer01 = Instantiate (nodePrefab, Growers.transform, false);
			//		GrowerObject coverScript = GrowerContainer01.AddComponent<GrowerObject> ();
			//		coverScript.cover = true;
			//		GameObject GrowerContainer02 = Instantiate (nodePrefab, Growers.transform, false);
			//		GrowerObject grassScript = GrowerContainer02.AddComponent<GrowerObject> ();
			//		coverScript.grassObject = true;
		}

		private void AttachHighPolys(GameObject HighPoly, int landType, int objList, int objType) {

			GameObject Cube = Instantiate (GetHIGHCube (landType, objList), HighPoly.transform, false); // Cube
			Cube.name = ("Cube");

			PutRocksInCube (Cube.transform, objList);


			GameObject GrowerContainer01 = Instantiate (emptyNode, HighPoly.transform, false);
			GrowerContainer01.name = ("GrowerContainer01");
			GameObject Cover = Instantiate (GetHIGHCover (landType, objList), GrowerContainer01.transform, false); // Cover Object
			Cover.name = ("Cover");
			GrowerObject coverScript = GrowerContainer01.AddComponent<GrowerObject> ();
			coverScript.cover = true;
			coverScript.SetProperties ();

			GameObject GrowerContainer02 = Instantiate (emptyNode, HighPoly.transform, false);
			GrowerContainer02.name = ("GrowerContainer02");
			GameObject Grass = Instantiate (GetGrassObject (landType, objList), GrowerContainer02.transform, false); // Grass object
			Grass.name = ("Grass");
			GrowerObject grassScript = GrowerContainer02.AddComponent<GrowerObject> ();
			grassScript.grassObject = true;
			grassScript.SetProperties ();

		}


		private void AddContainerComponents(GameObject GodNode, TerrainData_Full data) {

			TerrainObject newTerrainScript = GodNode.AddComponent<TerrainObject>();

			SetScriptFields (data, newTerrainScript);

			foreach (SelectObject script in selectObjects) {
				script.terrainscript = newTerrainScript;
			}

			newTerrainScript.ConnectObjectLinks();

			BoxCollider boxColl = GodNode.AddComponent<BoxCollider> ();
			boxColl.isTrigger = true;
			Vector3 collPos = new Vector3 (0, 0.5f, 0);
			boxColl.center = collPos;
		}

		private void SetScriptFields(TerrainData_Full data, TerrainObject script) {
			
			script.cubeUniqueID		= data.cubeUniqueID;

			script.gridLocX 		= data.gridLocX;
			script.gridLocZ			= data.gridLocZ;
			script.gridLocY 		= data.gridLocY;

			script.cubeLandType 	= data.cubeLandType;
			script.cubeList 		= data.cubeList;
			script.cubeType 		= data.cubeType;
			script.cubeRot  		= data.cubeRot;
			script.cubeSlopeType 	= data.cubeSlopeType;

			script.objReplaceable 	= data.objReplaceable;
			script.objDestructable 	= data.objDestructable;

			script.objList 			= data.objList;
			script.objType 			= data.objType;
			script.objRot  			= data.objRot;

			script.health 			= data.health;
			script.Growth 			= data.Growth;

			script.surfaceTop 		= data.surfaceTop;
			script.surfaceBottom 	= data.surfaceBottom;

			script.cubeSetActive 	= data.cubeSetActive;

			script.inSight 			= data.inSight;
		
			script.neighVects 		= data.neighVects;

			script._01 = data._01;
			script._03 = data._03;
			script._04 = data._04;
			script._05 = data._05;
			script._07 = data._07;
			script._09 = data._09;
			script._10 = data._10;
			script._11 = data._11;
			script._12 = data._12;
			script._14 = data._14;
			script._15 = data._15;
			script._16 = data._16;
			script._17 = data._17;
			script._19 = data._19;
			script._21 = data._21;
			script._22 = data._22;
			script._23 = data._23;
			script._25 = data._25;

			script.sunRiseVect = GameManager._SunManager.sunRiseVect;
			script.sunMidVect = GameManager._SunManager.sunMidVect;
			script.sunSetVect = GameManager._SunManager.sunSetVect;

			script.terrainData = data;
		}



		private GameObject GetLOWCube(int landType, int objList) {

			switch (objList) {
			case 0:
				return LOW_Cube [landType];
			case 1:
				return LOW_Slope_U [landType];
			case 2:
				return LOW_CrnIn_U [landType];
			case 3:
				return LOW_CrnOut_U [landType];
			case 4:
				return LOW_Slope_D [landType];
			case 5:
				return LOW_CrnIn_D [landType];
			case 6:
				return LOW_CrnOut_D [landType];	
			}
			return null;
		}


		private GameObject GetHIGHCube(int landType, int objList) {
		
			switch (objList) {
			case 0:
				return HIGH_Cube[landType];
			case 1:
				return HIGH_Slope_U[landType];
			case 2:
				return HIGH_CrnIn_U[landType];
			case 3:
				return HIGH_CrnOut_U[landType];
			case 4:
				return HIGH_Slope_D[landType];
			case 5:
				return HIGH_CrnIn_D[landType];
			case 6:
				return HIGH_CrnOut_D[landType];	
			}
			return null;
		}


		private GameObject GetHIGHCover(int landType, int objList) {

			switch (objList) {
			case 0:
				return HIGH_Cube_Cover[landType];
			case 1:
				return HIGH_Slope_Cover[landType];
			case 2:
				return HIGH_CrnIn_Cover[landType];
			case 3:
				return HIGH_CrnOut_Cover[landType];
			case 4:
				return HIGH_Cube_Cover[landType];
			case 5:
				return HIGH_Cube_Cover[landType];
			case 6:
				return HIGH_Cube_Cover[landType];
			}
			return null;
		}

		private GameObject GetGrassObject(int landType, int objList) {

			switch (objList) {
			case 0:
				return HIGH_Cube_Grass[landType];
			case 1:
				return HIGH_Slope_Grass[landType];
			case 2:
				return HIGH_CrnIn_Grass[landType];
			case 3:
				return HIGH_CrnOut_Grass[landType];
			case 4:
				return HIGH_Cube_Grass[landType];
			case 5:
				return HIGH_Cube_Grass[landType];
			case 6:
				return HIGH_Cube_Grass[landType];
			}
			return null;
		}


		private void PutRocksInCube(Transform parent, int objList) {

			int numRocks = Random.Range (0, 2);
			int rockCount = HIGH_Stone01.Count;

			Vector3 rockPos;
			Vector3 rockSize;
			Vector3 selectPos;
			Vector3 selectSize;

			float boundary = 0.01f;
			float zOffset = 0.01f;

			float rockSizeMin = 0.5f;
			float rockSizeMax = 3f;

			for (int i = 0; i < numRocks; i++) {

				if (Random.Range (0, 6) == 1) {
					GameObject rock = Instantiate (HIGH_Stone01 [Random.Range (0, rockCount)], parent);
					rock.name = ("Stone");
					rock.transform.SetParent (parent);

					// add select object to rock before its re-sized
					GameObject CubeSelect = Instantiate (Cube_Select[0], rock.transform, false); // cube Select Object
					selectObjects.Add(CubeSelect.GetComponent<SelectObject>());
					CubeSelect.transform.SetParent(rock.transform);
					selectPos = new Vector3 (0, 0, 0);
					CubeSelect.transform.localPosition = selectPos;
					selectSize = new Vector3 (0.015f, 0.015f, 0.015f);
					CubeSelect.transform.localScale = selectSize;

					int cubeSide = Random.Range (0, 6);

					switch (cubeSide) {
					case 0:
						rockPos = new Vector3 (-boundary, Random.Range (-boundary, boundary), Random.Range (-boundary, boundary) + zOffset); 
						break;
					case 1:
						rockPos = new Vector3 (boundary, Random.Range (-boundary, boundary), Random.Range (-boundary, boundary) + zOffset); 
						break;
					case 2:
						rockPos = new Vector3 (Random.Range (-boundary, boundary), -boundary, Random.Range (-boundary, boundary) + zOffset); 
						break;
					case 3:
						rockPos = new Vector3 (Random.Range (-boundary, boundary), boundary, Random.Range (-boundary, boundary) + zOffset); 
						break;
					case 4:
						rockPos = new Vector3 (Random.Range (-boundary, boundary), Random.Range (-boundary, boundary), -boundary + zOffset); 
						break;
					case 5:
						rockPos = new Vector3 (Random.Range (-boundary, boundary), Random.Range (-boundary, boundary), boundary + zOffset); 
						break;
					default:
						rockPos = new Vector3 (boundary, boundary, boundary + zOffset);
						break;
					}

					rockSize = new Vector3 (
						Random.Range (rockSizeMin, rockSizeMax), 
						Random.Range (rockSizeMin, rockSizeMax), 
						Random.Range (rockSizeMin, rockSizeMax)
					);

					rock.transform.localPosition = rockPos;
					rock.transform.localRotation = Random.rotation;
					rock.transform.localScale = rockSize;

				}

			}
		}
		}
}
