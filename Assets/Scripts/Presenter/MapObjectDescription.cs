using BuilderGame.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BuilderGame.Presenter
{

    [CreateAssetMenu]
    public class MapObjectDescription: ScriptableObject
    {
        [SerializeField]
        private GameObject prefab;
        public GameObject Prefab
        {
            get
            {
                return prefab;
            }
        }

        [SerializeField]
        private GameObject positiveCursorPrefab;
        public GameObject PositiveCursor
        {
            get
            {
                return positiveCursorPrefab;
            }
        }

        [SerializeField]
        private GameObject negativeCursorPrefab;
        public GameObject NegativeCursor
        {
            get
            {
                return negativeCursorPrefab;
            }
        }

        [SerializeField]
        Dimensions dim;
        public Dimensions Dimensions
        {
            get
            {
                return dim;
            }
        }

        [SerializeField]
        List<MapObjectType> canBuildOnTypes;

        public bool CanBuildOn(MapObjectType type)
        {
            if (canBuildOnTypes.Contains(type))
            {
                return true;
            }
            return false;
        }
        
    } // end of class ///
} // namespace ///
