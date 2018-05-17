using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers {

	public class PlayerManager : MonoBehaviour {

		public GameObject testPlayer;



		public void CreatePlayer() {

			GameObject player = Instantiate (testPlayer, transform, false);

			//player.transform.SetParent (this.gameObject.transform);
			player.transform.position = new Vector3(10,100,10);
		}
	}

}
