using BuilderGame.Constructing;
using BuilderGame.DataBus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.View
{
    public class RaycastSquareCursor : MonoBehaviour
    {


        void LateUpdate()
        {
            RaycastHit hit = CommandKeeper.GetCameraRaycastHit();


            if (hit.collider != null)
            {
                BuildingBlockPassport pass = hit.collider.GetComponentInParent<BuildingBlockPassport>();

                if (pass != null)
                {
                    /////

                    Vector3 hitObjPosition = pass.transform.position;

                    Vector3Int hitObjPositionInt = new Vector3Int(
                        (int)Mathf.Floor(hitObjPosition.x),
                        (int)Mathf.Round(hitObjPosition.y),
                        (int)Mathf.Floor(hitObjPosition.z)
                        );

                    transform.position = hitObjPositionInt;
                    //Debug.Log("hit pos=" + transform.position);
                }
            }

        } // LateUpdate() ///
    } // end of class ///
}

