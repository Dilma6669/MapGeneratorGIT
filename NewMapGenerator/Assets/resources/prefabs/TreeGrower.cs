using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGrower : MonoBehaviour {

	private GameObject branchPrefab;
	private GameObject leavesPrefab;


	private Transform spawn;
	private Transform children;

	private float growthRate = 0.01f;
	private float maxGrowth = 1;

	private int maxGlobalBranches = 5;
	private int globalBranchCount = 0;

	private int maxLocalBranches = 2;
	private int localBranchCount = 0;

	private bool growYouGoodThing = false;

	// Use this for initialization
	void Start () {

		spawn = transform.Find ("Spawn");
		children = transform.Find ("Children");

		transform.localScale = Vector3.zero;
		BuildBranch ();
	}


	public void BuildBranch() {
		growYouGoodThing = true;
		StartCoroutine(GrowBranches(0.5f));
	}


	// Update is called once per frame
	void Update () {

		if (growYouGoodThing) {

			Vector3 newVect = transform.localScale;

			if (newVect.x <= maxGrowth) {

				newVect = new Vector3 (newVect.x += growthRate, newVect.y, newVect.z += growthRate);
			}
//
			if (newVect.y <= maxGrowth) {

				newVect = new Vector3 (newVect.x, newVect.y += growthRate, newVect.z);

			} else {
				growYouGoodThing = false;
			}

			transform.localScale = newVect;
		}

	}


	private IEnumerator GrowBranches(float time) {

		if (globalBranchCount <= maxGlobalBranches) {

			for (int i = 0; i < 3; i++) {

				yield return new WaitForSeconds (time);

				spawn.transform.localRotation = Quaternion.Euler (Random.Range (-50, 50), Random.Range (-360, 360), 0);

				GameObject prefab = (GameObject)Resources.Load ("prefabs/BranchPrefab", typeof(GameObject));
				GameObject branch = Instantiate (prefab, spawn.transform);

				branch.transform.SetParent (children.transform);

				branch.GetComponent<Branch> ().BuildBranch (growthRate, maxGrowth, maxGlobalBranches, globalBranchCount, maxLocalBranches);
			}

		} else {
			
			GameObject prefab = (GameObject)Resources.Load ("prefabs/LeavesPrefab", typeof(GameObject));
			GameObject leaves = Instantiate (prefab, spawn.transform);
			leaves.transform.localScale = Vector3.one;
			float randomSize = Random.Range (30, 60);
			leaves.transform.localScale = new Vector3(randomSize,randomSize,randomSize);

		}
	}
}
