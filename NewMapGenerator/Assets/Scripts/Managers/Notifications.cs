using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers {
	
	public class Notifications : MonoBehaviour {

		public bool GameUpdates(int update=default(int),
								string p1Str=default(string), int p1=default(int),
								string p2Str=default(string), int p2=default(int),
								string p3Str=default(string), int p3=default(int)) {

			string str = "::::::: GAME UPDATE ::::::: ";

			switch (update) {
			case 0:
				MakeString (str + " " + p1Str + " " + p2Str + " " + p3Str);
				return true;
			case 1:
				MakeString (str + " Building Grid locations size of X:  " + p1 + "   Z:   " + p2 + "   Y:   " + p3);
				return true;
			case 2:
				MakeString (str + p1Str + " >>>>> COMPLETE");
				break;
			case 3:
				MakeString (str + p1Str + " " + p1 + " >>>>> COMPLETE");
				break;
			case 4:
				MakeString (str + p1Str + " " + p1 );
				break;
			default:
				return false;
			}
			return false;
		}


		public void GameUpdates (int update = default(int), int p1 = default(int), int p2 = default(int), int p3 = default(int)) {

			GameUpdates (update, "", p1, "", p2, "", p3);
		}



		public bool ErrorCheck(int error=default(int),							
								string p1Str=default(string), int p1=default(int),
								string p2Str=default(string), int p2=default(int),
								string p3Str=default(string), int p3=default(int)) {

			string str = ":::::::  ERROR  :::::: ";

			switch (error) {
			case 0:
				MakeString (str + " " + p1Str + " " + p2Str + " " + p3Str);
				break;
			case 1:
				if (p1 != p2) {
					MakeString(str + " The size of the 'Map Piece Selection' array must be (X2) of the 'Num Map Pieces' setting.");
					return false;
				} else {
					return true;
				}
			case 2:
	//			if (p1 > p2) {
	//				MakeString(str + " The height of the 'Max Terrain Height' must be equal to or less of 'Height Of Map Pieces' setting.");
	//				return false;
	//			} else {
	//				return true;
	//			}
			default:
				return false;
			}
			return false;
		}

		public void ErrorCheck (int update = default(int), int p1 = default(int), int p2 = default(int), int p3 = default(int)) {

			ErrorCheck (update, "", p1, "", p2, "", p3);
		}


		private void MakeString(string str) {

			str = "----------------------------------- \n" + str;
			Debug.Log (str);

		}
	}
}

