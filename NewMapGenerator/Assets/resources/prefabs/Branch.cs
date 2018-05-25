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

		StartCoroutine(GrowBranches(1));
	}
	
	// Update is called once per frame
	void Update () {

		if (growYouGoodThing) {

			Vector3 newVect = transform.localScale;

//			if (newVect.x <= maxGrowth) {
//
//				newVect = new Vector3 (newVect.x += growthRate, newVect.y, newVect.z += growthRate);
//			}
			
			if (newVect.y <= maxGrowth) {

				newVect = new Vector3 (newVect.x += growthRate, newVect.y += growthRate, newVect.z += growthRate);

			} else {
				growYouGoodThing = false;
			}

			transform.localScale = newVect;
		}
	}


	private IEnumerator GrowBranches(float time) {

		if (globalBranchCount <= maxGlobalBranches) {

			int randomY = Random.Range (0, 360);

			for (int i = 0; i < maxLocalBranches; i++) {

				yield return new WaitForSeconds (time);

				randomY += 222;
				spawn.transform.localRotation = Quaternion.Euler (-45, randomY, 0);

				GameObject prefab = (GameObject)Resources.Load ("prefabs/BranchPrefab", typeof(GameObject));
				GameObject branch = Instantiate (prefab, spawn.transform);

				branch.transform.SetParent (transform);

				branch.GetComponent<Branch> ().BuildBranch (growthRate, maxGrowth, maxGlobalBranches, globalBranchCount, maxLocalBranches);

			}

		} else {

//			GameObject prefab = (GameObject)Resources.Load ("prefabs/LeavesPrefab", typeof(GameObject));
//			GameObject leaves = Instantiate (prefab, spawn.transform);
//			leaves.transform.SetParent (children.transform);
//			float randomSize = Random.Range (30, 60);
//			leaves.transform.localScale = new Vector3(randomSize,randomSize,randomSize);

		}
	}
}
