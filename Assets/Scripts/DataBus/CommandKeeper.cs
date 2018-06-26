using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.DataBus
{
    public static class CommandKeeper
    {
        // input -> presenter //

        public static Func<RaycastHit> GetCameraRaycastPoint;

        public static Action OnBuildCommand;

        // presenter -> view //


        // presenter -> model //


    }
}
