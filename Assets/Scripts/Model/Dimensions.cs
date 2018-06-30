using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Model
{

    [Serializable]
    public class Dimensions
    {
        [Serializable]
        public class SimpleRect
        {
            public int MinX;
            public int MinZ;
            public int MaxX;
            public int MaxZ;
        }

        public int Height;
        public SimpleRect Rect;

    } // end of class ///

} // namespace ///

