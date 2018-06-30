using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Model {


	public class MapData
	{
        private const float EPSILON = 0.01f;

        private readonly Vector3Int areaDim = new Vector3Int(20, 20, 20);
        private MapObjectType[,,] map;


        private int[,] initialMap = {
            // z --->
            {22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, }, // x
            {22, 10, 33, 33, 22, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 22, }, // 
            {22, 10, 10, 22, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 22, 10, 10, 10, 10, 22, }, // |
            {22, 22, 10, 33, 33, 10, 22, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 22, }, // |
            {22, 10, 10, 33, 33, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 22, }, // |
            {22, 10, 10, 22, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 22, 10, 10, 10, 10, 22, }, // V
            {22, 33, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 22, 10, 10, 10, 22, 10, 22, },
            {22, 33, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 22, 10, 10, 10, 10, 10, 22, },
            {22, 10, 10, 22, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 22, 10, 10, 10, 22, 22, },
            {22, 10, 10, 22, 22, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 22, },
            {22, 10, 10, 22, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 22, 10, 10, 10, 10, 22, },
            {22, 22, 10, 33, 33, 10, 22, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 22, },
            {22, 10, 10, 33, 33, 10, 10, 10, 33, 33, 33, 10, 10, 10, 10, 10, 10, 10, 10, 22, },
            {22, 10, 10, 22, 10, 10, 10, 10, 33, 33, 33, 33, 33, 10, 22, 10, 10, 10, 10, 22, },
            {22, 33, 10, 10, 10, 10, 10, 33, 10, 33, 33, 33, 10, 22, 10, 10, 10, 22, 10, 22, },
            {22, 33, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 22, 10, 10, 10, 10, 10, 22, },
            {22, 10, 10, 22, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 22, 10, 10, 10, 22, 22, },
            {22, 33, 10, 10, 10, 10, 10, 33, 10, 10, 10, 10, 10, 22, 22, 10, 10, 10, 10, 22, },
            {22, 10, 10, 22, 10, 10, 33, 33, 33, 10, 10, 10, 10, 10, 22, 10, 10, 10, 22, 22, },
            {22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, },
        };


        public MapData()
        {
            map = new MapObjectType[areaDim.x, areaDim.y, areaDim.z];

            for (int xi = 0; xi < initialMap.Length && xi < xSize; xi++)
            {
                for (int zi = 0; zi < initialMap.LongLength && zi < zSize; zi++)
                {
                    map[xi, 0, zi] = (MapObjectType)initialMap[xi, zi];
                }
            }
        }


        public int xSize
        {
            get
            {
                return areaDim.x;
            }
        }
        public int ySize
        {
            get
            {
                return areaDim.y;
            }
        }
        public int zSize
        {
            get
            {
                return areaDim.z;
            }
        }

        public void SetObjectTypeAt(Vector3Int position, MapObjectType type)
        {
            int x = Mathf.Clamp(position.x, 0, xSize - 1);
            int y = Mathf.Clamp(position.y, 0, ySize - 1);
            int z = Mathf.Clamp(position.z, 0, zSize - 1);

            map[x, y, z] = type;
        }

        public MapObjectType GetObjectTypeAt(Vector3Int position)
        {
            int x = Mathf.Clamp(position.x, 0, xSize-1);
            int y = Mathf.Clamp(position.y, 0, ySize-1);
            int z = Mathf.Clamp(position.z, 0, zSize-1);

            return map[x, y, z];
        }

        public void SetObjectTypeAt(int x, int y, int z, MapObjectType type)
        {
            SetObjectTypeAt(new Vector3Int(x, y, z), type);
        }

        public MapObjectType GetObjectTypeAt(int x, int y, int z)
        {
            return GetObjectTypeAt(new Vector3Int(x, y, z));
        }


    } // End Of Class //

} // namespace ////



