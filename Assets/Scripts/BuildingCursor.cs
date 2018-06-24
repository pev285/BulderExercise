using BuilderGame.DataBus;
using BuilderGame.Model;
using BuilderGame.Presenter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame
{
    public class BuildingCursor : MonoBehaviour
    {

        [SerializeField]
        private ObjectsMapper objectsMapper;

        private bool isCursorOn = false;
        private MapObjectType objectType;
        private Transform positiveCursor;
        private Transform negativeCursor;
        private MapObjectDescription cursorDescription = null;

        private MapData mapData = null;

        public Vector3Int LastCursorPosition;




        public void SetCursorOff()
        {
            isCursorOn = false;
            if (positiveCursor != null && negativeCursor != null)
            {
                positiveCursor.gameObject.SetActive(false);
                negativeCursor.gameObject.SetActive(false);
            }
        }

        public void SetCursorOn(MapObjectType type, MapData mapData)
        {
            this.objectType = type;
            cursorDescription = objectsMapper.GetDescriptionByType(type);
            positiveCursor = cursorDescription.PositiveCursor;
            negativeCursor = cursorDescription.NegativeCursor;
            this.mapData = mapData;
            isCursorOn = true;
        }

        void LateUpdate()
        {
            if (isCursorOn)
            {
                RaycastHit hit = CommandKeeper.GetCameraRaycastPoint();
                Vector3 hitPoint = hit.point;
                LastCursorPosition = new Vector3Int(
                    (int)Mathf.Floor(hitPoint.x),
                    (int)Mathf.Round(hitPoint.y),
                    (int)Mathf.Floor(hitPoint.z)
                    );
/*                LastCursorPosition = new Vector3Int(
                    (int)Mathf.Round(hitPoint.x),
                    (int)Mathf.Round(hitPoint.y),
                    (int)Mathf.Round(hitPoint.z)
                    );*/

                positiveCursor.position = LastCursorPosition;
                negativeCursor.position = LastCursorPosition;

                MapObjectType type = mapData.GetObjectTypeAt(LastCursorPosition);
                bool canBuildHere = cursorDescription.CanBuildOn(type);
                positiveCursor.gameObject.SetActive(canBuildHere);
                negativeCursor.gameObject.SetActive(!canBuildHere);
            }

        } // LateUpdate() ////


    } // end of class ///
} // namespace ///

