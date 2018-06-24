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
        private Transform positiveCursor = null;
        public Transform PositiveCursor
        {
            get
            {
                if (positiveCursor == null)
                {
                    positiveCursor = Instantiate(positiveCursorPrefab, Vector3.zero, Quaternion.identity).GetComponent<Transform>();
                    positiveCursor.gameObject.SetActive(false);
                }
                return positiveCursor;
            }
        }

        [SerializeField]
        private GameObject negativeCursorPrefab;
        private Transform negativeCursor = null;
        public Transform NegativeCursor
        {
            get
            {
                if (negativeCursor == null)
                {
                    negativeCursor = Instantiate(negativeCursorPrefab, Vector3.zero, Quaternion.identity).GetComponent<Transform>();
                    negativeCursor.gameObject.SetActive(false);
                }
                return negativeCursor;
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
