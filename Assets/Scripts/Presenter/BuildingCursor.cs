using BuilderGame.DataBus;
using BuilderGame.Model;
using BuilderGame.Presenter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Presenter
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

                //Debug.Log("hitpoint = " + hitPoint);

                Vector3Int pos = new Vector3Int(
                    (int)Mathf.Floor(hitPoint.x),
                    (int)Mathf.Round(hitPoint.y),
                    (int)Mathf.Floor(hitPoint.z)
                    );
                /*                LastCursorPosition = new Vector3Int(
                                    (int)Mathf.Round(hitPoint.x),
                                    (int)Mathf.Round(hitPoint.y),
                                    (int)Mathf.Round(hitPoint.z)
                                    );*/

                Dimensions dim = cursorDescription.Dimensions;
                Dimensions.SimpleRect rect = dim.Rect;

                bool canBuildHere;
                int h;
                ProbeBuildingPlace(rect, out canBuildHere, out h);

                Debug.Log("h = " + h);
                LastCursorPosition = new Vector3Int(pos.x, pos.y + h, pos.z);
                Debug.Log("LastCursorPosition = " + LastCursorPosition.ToString());
                positiveCursor.position = LastCursorPosition;
                negativeCursor.position = LastCursorPosition;


                positiveCursor.gameObject.SetActive(canBuildHere);
                negativeCursor.gameObject.SetActive(!canBuildHere);
            }

        } // LateUpdate() ////

        private void ProbeBuildingPlace(Dimensions.SimpleRect rect, out bool canBuildHere, out int h)
        {
            Vector3Int p1 = new Vector3Int(LastCursorPosition.x + rect.MinX, LastCursorPosition.y, LastCursorPosition.z + rect.MinZ);
            Vector3Int p2 = new Vector3Int(LastCursorPosition.x + rect.MaxX - 1, LastCursorPosition.y, LastCursorPosition.z + rect.MinZ);
            Vector3Int p3 = new Vector3Int(LastCursorPosition.x + rect.MinX, LastCursorPosition.y, LastCursorPosition.z + rect.MaxZ - 1);
            Vector3Int p4 = new Vector3Int(LastCursorPosition.x + rect.MaxX - 1, LastCursorPosition.y, LastCursorPosition.z + rect.MaxZ - 1);

            MapObjectType type = mapData.GetObjectTypeAt(p1);
            canBuildHere = cursorDescription.CanBuildOn(type);
            MapObjectDescription desc = objectsMapper.GetDescriptionByType(type);
            Debug.Log("p1 = " + p1 + ", type = " + type.ToString());
            h = desc.Dimensions.Height;

            type = mapData.GetObjectTypeAt(p2);
            canBuildHere = canBuildHere && cursorDescription.CanBuildOn(type);
            desc = objectsMapper.GetDescriptionByType(type);
            int h2 = desc.Dimensions.Height;
            canBuildHere = canBuildHere && (h == h2);

            type = mapData.GetObjectTypeAt(p3);
            canBuildHere = canBuildHere && cursorDescription.CanBuildOn(type);
            desc = objectsMapper.GetDescriptionByType(type);
            int h3 = desc.Dimensions.Height;
            canBuildHere = canBuildHere && (h == h3);

            type = mapData.GetObjectTypeAt(p4);
            canBuildHere = canBuildHere && cursorDescription.CanBuildOn(type);
            desc = objectsMapper.GetDescriptionByType(type);
            int h4 = desc.Dimensions.Height;
            canBuildHere = canBuildHere && (h == h4);
        }
    } // end of class ///
} // namespace ///

