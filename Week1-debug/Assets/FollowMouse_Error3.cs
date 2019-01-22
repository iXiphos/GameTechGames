using UnityEngine;
using System.Collections;

public class FollowMouse_Error3 : MonoBehaviour {

	void Update () {

		Follow ();
	}


	void Follow(){
		Vector3 mousePosition = Input.mousePosition;//Get the mouse position in screen pixels
		//convert screen coordinates to world space
		Vector3 adjustedPosition = Camera.main.ScreenToWorldPoint (mousePosition);
		//set the z of the player to 0
		adjustedPosition.z = 0f;
		//reposition the player
		transform.position = adjustedPosition;
	}
}
