using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour {

    private new Camera camera;
    private RaycastHit lastHitInfo;
    private int maxDistance = 100;

    private Vector3 raycastPoint = new Vector3(0.5F, 0.5F, 0);

    void Awake () {
        camera = GetComponent<Camera>();

        CommandKeeper.GetCameraRaycastPoint += GetLastHitInfo;
	}

    private RaycastHit GetLastHitInfo()
    {
        return lastHitInfo;
    }

    void Update () {
        Ray ray = camera.ViewportPointToRay(raycastPoint);

        Physics.Raycast(ray, out lastHitInfo, maxDistance);
	}
}
