using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour {

	// Globals
	public List<List<List<GameObject>>> resAllLists = new List<List<List<GameObject>>>();

	public List<List<GameObject>> DEFAULT_NOTHING_PREFABS = new List<List<GameObject>>(); // this is because landType = 0 means air, so 1 = dirt, etc (Dont like this)


	// DIRT ////////////
	public List<List<GameObject>> DIRT_OBJECT_PREFABS = new List<List<GameObject>>();

	public List<GameObject> DIRT_CUBES = new List<GameObject> ();
	public List<GameObject> DIRT_SLOPES_D = new List<GameObject> ();
	public List<GameObject> DIRT_CORNER_OUTTER_D = new List<GameObject> ();
	public List<GameObject> DIRT_CORNER_INNER_D = new List<GameObject> ();
	public List<GameObject> DIRT_SLOPES_U = new List<GameObject> ();
	public List<GameObject> DIRT_CORNER_OUTTER_U = new List<GameObject> ();
	public List<GameObject> DIRT_CORNER_INNER_U = new List<GameObject> ();

	public List<GameObject> DIRT_SURFACE_OBJECTS_D = new List<GameObject> ();
	public List<GameObject> DIRT_CAVE_OBJECTS_D = new List<GameObject> ();
	public List<GameObject> DIRT_CAVE_OBJECTS_U = new List<GameObject> ();

	public List<GameObject> DIRT_UNDERWATER_OBJECTS_D = new List<GameObject> ();
	public List<GameObject> DIRT_UNDERWATER_OBJECTS_U = new List<GameObject> ();
	//////////////


	// DESSERT ////////////
//	public List<List<GameObject>> DESSERT_OBJECT_PREFABS = new List<List<GameObject>>();
//
//	public List<GameObject> DESSERT_CUBES = new List<GameObject> ();
//	public List<GameObject> DESSERT_SLOPES_D = new List<GameObject> ();
//	public List<GameObject> DESSERT_CORNER_OUTTER_D = new List<GameObject> ();
//	public List<GameObject> DESSERT_CORNER_INNER_D = new List<GameObject> ();
//	public List<GameObject> DESSERT_SLOPES_U = new List<GameObject> ();
//	public List<GameObject> DESSERT_CORNER_OUTTER_U = new List<GameObject> ();
//	public List<GameObject> DESSERT_CORNER_INNER_U = new List<GameObject> ();
//
//	public List<GameObject> DESSERT_SURFACE_OBJECTS_D = new List<GameObject> ();
//	public List<GameObject> DESSERT_CAVE_OBJECTS_D = new List<GameObject> ();
//	public List<GameObject> DESSERT_CAVE_OBJECTS_U = new List<GameObject> ();
	//////////////


	// Use this for initialization
	void Awake () {

		resAllLists.Add (DEFAULT_NOTHING_PREFABS); // this is because landType = 0 means air, so 1 = dirt, etc (Dont like this)


		// all all resource Lists into a giant list for easier access
		DIRT_OBJECT_PREFABS.Add (DIRT_CUBES);

		DIRT_OBJECT_PREFABS.Add (DIRT_SLOPES_D);
		DIRT_OBJECT_PREFABS.Add (DIRT_CORNER_OUTTER_D);
		DIRT_OBJECT_PREFABS.Add (DIRT_CORNER_INNER_D);

		DIRT_OBJECT_PREFABS.Add (DIRT_SLOPES_U);
		DIRT_OBJECT_PREFABS.Add (DIRT_CORNER_OUTTER_U);
		DIRT_OBJECT_PREFABS.Add (DIRT_CORNER_INNER_U);

		DIRT_OBJECT_PREFABS.Add (DIRT_SURFACE_OBJECTS_D);
		DIRT_OBJECT_PREFABS.Add (DIRT_CAVE_OBJECTS_D);
		DIRT_OBJECT_PREFABS.Add (DIRT_CAVE_OBJECTS_U);

		DIRT_OBJECT_PREFABS.Add (DIRT_UNDERWATER_OBJECTS_D);
		DIRT_OBJECT_PREFABS.Add (DIRT_UNDERWATER_OBJECTS_U);

		resAllLists.Add (DIRT_OBJECT_PREFABS);
	}

}
