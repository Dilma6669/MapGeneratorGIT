using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Managers {

		public class SunManager : MonoBehaviour {

			public Vector3 sunRiseVect;
			public Vector3 sunMidVect;
			public Vector3 sunSetVect;
		//
		//	public float SunMoonMoveSpeed;
		//	public float intensChangeRate;
		//	public float RAY_INTERVALS;
		//
		//	public float DayDuration;
		//	public float timer;
		//
		//	public Light sunLight;
		//	public LensFlare sunLensFlare;
		//	public Light moonLight;
		//	public LensFlare moonLensFlare;
		//
		//	public GameObject sunPosPrefab;
		//	private Transform sunStaticPostions;
		//	private Transform orbitObject;
		//
		//	public float sunIntensityNow;
		//	public float sunIntensityNext;
		//
		//	public float moonsRotation;
		//
		//	public float sunsRotation;
		//
		//	private int[] sunAngles;
		//	private float[] sunIntensity;
		//
		//	public bool sunrise    = true;
		//	public bool morning    = false;
		//	public bool dayStart   = false;
		//	public bool evening    = false;
		//	public bool sunset     = false;
		//
		//	public bool dusk       = false;
		//	public bool nightTwil  = false;
		//	public bool nightStart = false;
		//	public bool mornTwil   = false;
		//	public bool dawn       = false;
		//
		//
		//	public bool DAY = true;
		//	public bool NIGHT = false;
		//
		//	public int CURRENT_STATE = 0;
		//
		//
		//	// Use this for initialization
		//	void Awake () {
		//		
		//		gameManager = FindObjectOfType<GameManager> ();
		//
		//		sunStaticPostions = transform.GetChild(0).transform;
		//		orbitObject = transform.GetChild(1).transform;
		//
		//		sunAngles = new int[11];
		//		sunAngles[0] = 0;
		//		sunAngles[1] = 10;
		//		sunAngles[2] = 20;
		//		sunAngles[3] = 160;
		//		sunAngles[4] = 170;
		//		sunAngles[5] = 180;
		//		sunAngles[6] = 190;
		//		sunAngles[7] = 200;
		//		sunAngles[8] = 340;
		//		sunAngles[9] = 350;
		//		sunAngles[10] = 360;
		//
		//		sunIntensity = new float[11];
		//		sunIntensity[0] = 0.10f;
		//		sunIntensity[1] = 0.25f;
		//		sunIntensity[2] = 0.5f;
		//		sunIntensity[3] = 0.25f;
		//		sunIntensity[4] = 0.10f;
		//		sunIntensity[5] = 0.10f;
		//		sunIntensity[6] = 0.05f;
		//		sunIntensity[7] = 0.0f;
		//		sunIntensity[8] = 0.05f;
		//		sunIntensity[9] = 0.10f;
		//		sunIntensity[10] = 0.10f;
		//
		//		DayDuration = SunMoonMoveSpeed * 2;
		//
		//		sunLight.intensity = sunIntensity[0];
		//		sunLensFlare.brightness  = sunIntensity[0];
		//	
		//	}
		
			public void SetSunPositions(IList<Vector3> list) {
				
				sunRiseVect = list[0];
				sunMidVect = list[1];
				sunSetVect = list[2];

			}


		//	// Update is called once per frame
		//	void Update () {
		//		sunsRotation = orbitObject.rotation.eulerAngles.z;
		//		moonsRotation = -orbitObject.rotation.eulerAngles.z;
		//		MoveSun ();
		//		CheckState();
		//
		//		timer += Time.deltaTime;
		//	}
		//
		//	private void MoveSun() {
		//
		//		orbitObject.transform.Rotate (new Vector3 (0, 0, 1) * Time.deltaTime * SunMoonMoveSpeed);
		//	}
		//
		//
		//	// DONT FUCKEN TOUCH THIS
		//	private void CheckState() {
		//		if (sunsRotation >= sunAngles [0] && sunsRotation <= sunAngles [1]) {
		//			CURRENT_STATE = 0;
		//		}
		//		if (sunsRotation >= sunAngles[CURRENT_STATE]) {
		//			ChangeState (CURRENT_STATE);
		//			CURRENT_STATE += 1;
		//		}
		//	}
		//
		//
		//	private void ChangeState(int state) {
		//		ResetAllStates ();
		//		switch (state) {
		//		case 0:
		//			NIGHT = false;
		//			DAY = true;
		//			sunrise = true;
		//			sunLight.intensity = sunIntensity[state];
		//			sunLensFlare.brightness = sunIntensity[state];
		//			break;
		//		case 1:
		//			morning = true;
		//			sunLight.intensity = sunIntensity[state];
		//			sunLensFlare.brightness = sunIntensity[state];
		//			break;
		//		case 2:
		//			dayStart = true;
		//			sunLight.intensity = sunIntensity [state];
		//			sunLensFlare.brightness = sunIntensity [state];
		//			//// send out sun rays here
		//			Debug.Log("SendOutCubeRays");
		//			StartCoroutine(SendOutCubeRays(RAY_INTERVALS)); 
		//			break;
		//		case 3:
		//			evening = true;
		//			sunLight.intensity = sunIntensity[state];
		//			sunLensFlare.brightness = sunIntensity[state];
		//			break;
		//		case 4:
		//			sunset = true;
		//			sunLight.intensity = sunIntensity[state];
		//			sunLensFlare.brightness = sunIntensity[state];
		//			break;
		//		case 5:
		//			DAY = false;
		//			NIGHT = true;
		//			dusk = true;
		//			sunLight.intensity = sunIntensity[state];
		//			sunLensFlare.brightness = sunIntensity[state];
		//			break;
		//		case 6:
		//			nightTwil = true;
		//			sunLight.intensity = sunIntensity[state];
		//			sunLensFlare.brightness = sunIntensity[state];
		//			break;
		//		case 7:
		//			nightStart = true;
		//			sunLight.intensity = sunIntensity[state];
		//			sunLensFlare.brightness = sunIntensity[state];
		//			break;		
		//		case 8:
		//			mornTwil = true;
		//			sunLight.intensity = sunIntensity[state];
		//			sunLensFlare.brightness = sunIntensity[state];
		//			break;
		//		case 9:
		//			dawn = true;
		//			sunLight.intensity = sunIntensity[state];
		//			sunLensFlare.brightness = sunIntensity[state];
		//			break;
		//		case 10:
		//			CURRENT_STATE = 0;
		//			break;
		//		}
		//	}
		//
		//
		//	private void ResetAllStates() {
		//		sunrise    = false;
		//		morning    = false;
		//		dayStart   = false;
		//		evening    = false;
		//		sunset     = false;
		//		dusk       = false;
		//		nightTwil  = false;
		//		nightStart = false;
		//		mornTwil   = false;
		//		dawn       = false;
		//	}
		//
		//	public void CreateSunLocations() {
		//
		//		GameObject sunPosObject1 = Instantiate (sunPosPrefab, transform, false);
		//		sunPosObject1.transform.SetParent (sunStaticPostions);
		//		sunPosObject1.transform.position = gameManager.gridManager.sunRiseVect;
		//
		//		GameObject sunPosObject2 = Instantiate (sunPosPrefab, transform, false);
		//		sunPosObject2.transform.SetParent (sunStaticPostions);
		//		sunPosObject2.transform.position = gameManager.gridManager.sunMidVect;
		//
		//		GameObject sunPosObject3 = Instantiate (sunPosPrefab, transform, false);
		//		sunPosObject3.transform.SetParent (sunStaticPostions);
		//		sunPosObject3.transform.position = gameManager.gridManager.sunSetVect;
		//
		//
		//		orbitObject.transform.position = gameManager.gridManager.sunMidVect;
		//	}
		//		
		//
		//	private IEnumerator SendOutCubeRays(float time) {
		//
		//		foreach (TerrainObject cubeScript in gameManager.cubeManager.surfacesTop.Values) {
		//
		//			cubeScript.CalculateCubeObjectGrowth ();
		//
		//			yield return new WaitForSeconds (time);
		//		}
		//			
		//	}
	}
}

