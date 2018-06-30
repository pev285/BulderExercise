using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BuilderGame.Constructing
{
    public class BuildingBlockRotator : MonoBehaviour
    {
        private Transform rotationCenter = null;
        void Start()
        {
            rotationCenter = transform.FindChild("RotationCenter");
        }

        public void SetHorizontalRotation(float angle)
        {
            if (rotationCenter != null)
            {
                rotationCenter.rotation = Quaternion.AngleAxis(angle, Vector3.up);
            }
        }
    }
}

