using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BuilderGame.Constructing
{
    public class BuildingBlockRotator : MonoBehaviour
    {
        private Transform rotationCenter = null;
        private bool alreadyLookedForRC = false;
        private Transform RotationCenter
        {
            get
            {
                if (!alreadyLookedForRC)
                {
                    rotationCenter = transform.Find("RotationCenter");
                    alreadyLookedForRC = true;
                }
                return rotationCenter;
            }
        }


        public void SetHorizontalRotation(float angle)
        {
            //if (rotationCenter == null)
            //{
            //    Debug.Log("Rotation center is null");
            //}
            //else
            //{
            //    Debug.Log("Rotation center is ok");
            //}
            if (RotationCenter != null)
            {
                RotationCenter.rotation = Quaternion.AngleAxis(angle, Vector3.up);
            }
        }
    }
}

