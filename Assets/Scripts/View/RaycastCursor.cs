using BuilderGame.DataBus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.View
{
    public class RaycastCursor : MonoBehaviour
    {


        void LateUpdate()
        {
            RaycastHit hit = CommandKeeper.GetCameraRaycastHit();

            transform.position = hit.point;
        }
    } // end of class ///
}

