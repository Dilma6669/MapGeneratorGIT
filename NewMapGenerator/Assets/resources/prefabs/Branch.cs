using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour {

	private GameObject branchPrefab;
	private GameObject leavesPrefab;

	private Transform spawn;
	private Transform children;


	private float growthRate;
	private float maxGrowth;

	private int maxGlobalBranches;
	private int globalBranchCount;

	private int maxLocalBranches;
	private int localBranchCount = 0;

	private bool growYouGoodThing = false;

	// Use this for initialization
	void Awake () {
		
		spawn = transform.Find ("Spawn");
		children = transform.Find ("Children");

		transform.localScale = Vector3.zero;
	}


	public void BuildBranch(float growthR, float maxG, int maxGlobalBs, int globalBranchC, int maxLocalB) {

		growthRate = growthR;
		maxGrowth = (maxG -= 0.1f);

		maxGlobalBranches = maxGlobalBs;
		globalBranchCount = (globalBranchC += 1);

		maxLocalBranches = maxLocalB;

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

			for (int i = 0; i < maxLocalBranches; i++) {

				yield return new WaitForSeconds (time);

				spawn.transform.localRotation = Quaternion.Euler (Random.Range (-100, 100), Random.Range (-360, 360), Random.Range (-100, 100));


				GameObject prefab = (GameObject)Resources.Load ("prefabs/BranchPrefab", typeof(GameObject));
				GameObject branch = Instantiate (prefab, spawn.transform);

				branch.transform.SetParent (children.transform);

				branch.GetComponent<Branch> ().BuildBranch (growthRate, maxGrowth, maxGlobalBranches, globalBranchCount, maxLocalBranches);

			}

		} else {

			GameObject prefab = (GameObject)Resources.Load ("prefabs/LeavesPrefab", typeof(GameObject));
			GameObject leaves = Instantiate (prefab, spawn.transform);
			leaves.transform.SetParent (children.transform);
			float randomSize = Random.Range (30, 60);
			leaves.transform.localScale = new Vector3(randomSize,randomSize,randomSize);

		}
	}
}
