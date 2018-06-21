using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCursor : MonoBehaviour {

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();  
    }

    void LateUpdate () {
        RaycastHit hit = CommandKeeper.GetCameraRaycastPoint();

        transform.position = hit.point;
	}
} // end of class ///
