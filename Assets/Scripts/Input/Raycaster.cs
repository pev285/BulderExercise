using BuilderGame.DataBus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.InputReading
{
    public class Raycaster : MonoBehaviour
    {

        private new Camera camera;
        private RaycastHit lastHitInfo;
        private int maxDistance = 100;
        private Vector3 infinityVector = new Vector3(1000, 1000, 1000);

        private Vector3 raycastPoint = new Vector3(0.5F, 0.5F, 0);

        void Awake()
        {
            camera = GetComponent<Camera>();

            CommandKeeper.GetCameraRaycastHit += GetLastHitInfo;
        }

        private RaycastHit GetLastHitInfo()
        {
            return lastHitInfo;
        }

        void Update()
        {
            Ray ray = camera.ViewportPointToRay(raycastPoint);

            bool isHit = Physics.Raycast(ray, out lastHitInfo, maxDistance);

            if (!isHit)
            {
                lastHitInfo.point = infinityVector;
            }
            //Debug.Log(lastHitInfo.point);
        } // Update() /////
    } // end of class ///
} // namespace ///

