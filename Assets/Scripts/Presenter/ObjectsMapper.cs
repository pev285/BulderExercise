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
            public GameObject go;
        }

        [SerializeField]
        private List<MapTypeObjectPiar> objectsMapper;

        public GameObject GetPrefabOfType(MapObjectType type)
        {
            for (int i = 0; i < objectsMapper.Count; i++)
            {
                if (objectsMapper[i].type == type)
                {
                    return objectsMapper[i].go;
                }
            }
            return null;
        }
		
	} // End Of Class //

} // namespace ////



