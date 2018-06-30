using BuilderGame.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.DataBus
{
    public static class CommandKeeper
    {
        // input -> presenter //

        public static Func<RaycastHit> GetCameraRaycastHit;

        public static Action<float, float> OnPlayerMoveCommand;
        public static Action<float, float> OnPlayerRotateCommand;

        public static Action<MapObjectType> OnBuildingBlockChoose;
        public static Action OnBuildCommand;

        // player -> presenter ///

        public static Func<Vector3> GetPlayerPosition;
        public static Func<Vector3> GetPlayerForward;
        public static Func<float> GetPlayerRotationAngle;

        // presenter -> view //


        // presenter -> model //


    }
}
