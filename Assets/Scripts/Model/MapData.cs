using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Model {


	public class MapData
	{

        private Vector3Int dimensions = new Vector3Int(20, 20, 20);

        private int[,] map = {
            // z --->
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, }, // x
            {1, 0, 2, 2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, }, // 
            {1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, }, // |
            {1, 1, 0, 2, 2, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, }, // |
            {1, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, }, // |
            {1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, }, // V
            {1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, },
            {1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, },
            {1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, },
            {1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, },
            {1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, },
            {1, 1, 0, 2, 2, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, },
            {1, 0, 0, 2, 2, 0, 0, 0, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 1, },
            {1, 0, 0, 1, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 1, 0, 0, 0, 0, 1, },
            {1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, },
            {1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, },
            {1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, },
            {1, 2, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, },
            {1, 0, 0, 1, 0, 0, 2, 2, 2, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, },
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, },
        };


        public int xSize
        {
            get
            {
                return dimensions.x;
            }
        }
        public int ySize
        {
            get
            {
                return dimensions.y;
            }
        }
        public int zSize
        {
            get
            {
                return dimensions.z;
            }
        }

        public MapObjectType GetObjectTypeAt(int x, int y, int z)
        {
            return GetObjectTypeAt(new Vector3Int(x, y, z));
        }

        public MapObjectType GetObjectTypeAt(Vector3Int position)
        {
            int x = Mathf.Clamp(position.x, 0, xSize);
            int z = Mathf.Clamp(position.z, 0, zSize);

            return (MapObjectType)map[x, z];
        }

        public bool TestBuildingPoint(Dimensions dimensions, Vector3 candidatePosition, out Vector3Int mapPosition)
        {
            mapPosition = Vector3Int.zero;

            return false;
        }
		
	} // End Of Class //

} // namespace ////



