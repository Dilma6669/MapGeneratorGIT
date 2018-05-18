using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers {

	public class PlayerScript : MonoBehaviour {

		public GameObject CameraBox;
		public GameObject HeadBox;

		public int terrainColliderSize = 0;

		public float mouseRotationSpeed = 10f;                         // Horizontal turn speed.
		public float keysMovementSpeed = 10f;                           // Vertical turn speed.

		public float zoomMinFOV = 15f;
		public float zoomMaxFOV = 90f;
		public float zoomSensitivity = 10f;

		public float smooth = 10f;                                         // Speed of camera responsiveness.

		public float maxVerticalAngle;                               // Camera max clamp angle. 
		public float minVerticalAngle;  								 // Camera min clamp angle.

		private float h;                                
		private float v;    
		private float x;                                
		private float y;  

		private float angleH = 0;                                          // Float to store camera horizontal angle related to mouse movement.
		private float angleV = 0;                                          // Float to store camera vertical angle related to mouse movement.


		void Start() {
			GetComponentInChildren<SphereCollider> ().radius = terrainColliderSize;
		}

		
		// Update is called once per frame
		void Update () {

				h = Input.GetAxis ("Horizontal") * Time.deltaTime * 150.0f;
				v = Input.GetAxis ("Vertical") * Time.deltaTime * 3.0f;

				transform.Rotate (0, h, 0);
				transform.Translate (0, 0, v);

				// mouse look around
				x = Input.GetAxis ("Mouse X");
				y = Input.GetAxis ("Mouse Y");

				// Get mouse movement to orbit the camera.
				angleH += Mathf.Clamp (x, -1, 1) * mouseRotationSpeed;
				angleV += Mathf.Clamp (y, -1, 1) * mouseRotationSpeed;

				// clamp the camera
				angleH = Mathf.Clamp (angleH, minVerticalAngle, maxVerticalAngle);
				angleV = Mathf.Clamp (angleV, minVerticalAngle, maxVerticalAngle);

				// Set camera orientation..
				Quaternion aimRotation = Quaternion.Euler (-angleV, angleH, 0);
				CameraBox.transform.localRotation = aimRotation;
				HeadBox.transform.localRotation = aimRotation;
			}
		}
}
	