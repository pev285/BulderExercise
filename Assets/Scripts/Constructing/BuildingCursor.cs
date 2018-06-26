using BuilderGame.DataBus;
using BuilderGame.Model;
using BuilderGame.Presenter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Constructing
{
    public class BuildingCursor : MonoBehaviour
    {

        [SerializeField]
        private ObjectsMapper objectsMapper;

        private bool isCursorOn = false;
        private bool canBuildHere = false;
        private MapObjectType objectType;
        private Transform positiveCursor;
        private Transform negativeCursor;
        private MapObjectDescription cursorDescription = null;

        private MapData mapData = null;

        public Vector3Int LastCursorPosition;


        public bool CanBuildNow
        {
            get
            {
                return isCursorOn && canBuildHere;
            }
        }
        public MapObjectType CurrentCursorType
        {
            get
            {
                return objectType;
            }
        }
        public Vector3 CurrentCursorPosition
        {
            get
            {
                return LastCursorPosition;
            }
        }



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

        ///////////////////////////////////////////////////////////////////////////////

        void LateUpdate()
        {
            if (isCursorOn)
            {
                RaycastHit hit = CommandKeeper.GetCameraRaycastPoint();

                if (hit.collider != null)
                {
                    BuildingBlockPassport pass = hit.collider.GetComponentInParent<BuildingBlockPassport>();
                    if (pass != null)
                    {
                        Vector3 hitPoint = pass.transform.position;

                        Vector3Int pos = new Vector3Int(
                            (int)Mathf.Floor(hitPoint.x),
                            (int)Mathf.Round(hitPoint.y),
                            (int)Mathf.Floor(hitPoint.z)
                            );


                        Dimensions dim = cursorDescription.Dimensions;
                        Dimensions.SimpleRect rect = dim.Rect;

                        int h;
                        ProbeBuildingPlace(pos, rect, out canBuildHere, out h);

                        LastCursorPosition = new Vector3Int(pos.x, pos.y + h, pos.z);
                        positiveCursor.position = LastCursorPosition;
                        negativeCursor.position = LastCursorPosition;

                        // !!!! TODO
                        if (h > 0)
                        {
                            MapObjectType type = mapData.GetObjectTypeAt(LastCursorPosition);
                            if (type != MapObjectType.AIR)
                            {
                                canBuildHere = false;
                            }
                        }

                        positiveCursor.gameObject.SetActive(canBuildHere);
                        negativeCursor.gameObject.SetActive(!canBuildHere);
                    }
                }
                else
                {
                    positiveCursor.gameObject.SetActive(false);
                    negativeCursor.gameObject.SetActive(false);
                }
            }

        } // LateUpdate() ////

        private void ProbeBuildingPlace(Vector3Int pos, Dimensions.SimpleRect rect, out bool canBuildHere, out int h)
        {


            Vector3Int p1 = new Vector3Int(pos.x + rect.MinX, pos.y, pos.z + rect.MinZ);
            Vector3Int p2 = new Vector3Int(pos.x + rect.MaxX - 1, pos.y, pos.z + rect.MinZ);
            Vector3Int p3 = new Vector3Int(pos.x + rect.MinX, pos.y, pos.z + rect.MaxZ - 1);
            Vector3Int p4 = new Vector3Int(pos.x + rect.MaxX - 1, pos.y, pos.z + rect.MaxZ - 1);

            MapObjectType type = mapData.GetObjectTypeAt(p1);
            canBuildHere = cursorDescription.CanBuildOn(type);
            MapObjectDescription desc = objectsMapper.GetDescriptionByType(type);
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

