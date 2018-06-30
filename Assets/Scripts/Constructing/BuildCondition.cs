using BuilderGame.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BuilderGame.Constructing
{
    [Serializable]
    public class BuildCondition 
    {
        public enum RelativePosition
        {
            OVER,
            ON_THE_SIDE,
            INSTEAD,
        }

        public MapObjectType type;
        public RelativePosition position;


    }
}
