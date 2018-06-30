using BuilderGame.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Presenter {

    [CreateAssetMenu]
	public class ObjectsMapper : ScriptableObject 
	{
        [Serializable]
        public class MapTypeObjectPiar
        {
            public MapObjectType type;
            public MapObjectDescription objectDescription;
        }

        [SerializeField]
        private List<MapTypeObjectPiar> objectsMapper;

        public MapObjectDescription GetDescriptionByType(MapObjectType type)
        {
            return objectsMapper[GetIndexByType(type)].objectDescription;
        }

        public GameObject GetPrefabOfType(MapObjectType type)
        {
            return objectsMapper[GetIndexByType(type)].objectDescription.Prefab;
        }
        public Transform GetPositiveCursorOfType(MapObjectType type)
        {
            return objectsMapper[GetIndexByType(type)].objectDescription.PositiveCursor;
        }
        public Transform GetNegativeCursorOfType(MapObjectType type)
        {
            return objectsMapper[GetIndexByType(type)].objectDescription.NegativeCursor;
        }


        private int GetIndexByType(MapObjectType type)
        {
            for (int i = 0; i < objectsMapper.Count; i++)
            {
                if (objectsMapper[i].type == type)
                {
                    return i;
                }
            }
            return -1;
        }


    } // End Of Class //

} // namespace ////



