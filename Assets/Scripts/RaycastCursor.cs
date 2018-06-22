using BuilderGame.DataBus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCursor : MonoBehaviour {


    void LateUpdate () {
        RaycastHit hit = CommandKeeper.GetCameraRaycastPoint();

        transform.position = hit.point;
	}
} // end of class ///
