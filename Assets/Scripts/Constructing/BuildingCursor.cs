﻿using BuilderGame.DataBus;
using BuilderGame.Model;
using BuilderGame.Presenter;
using System;
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
        private MapObjectType currentCursorType;
        private MapObjectDescription cursorDescription = null;

        private Transform positiveCursor;
        private BuildingBlockRotator positiveRotator;
        private Transform negativeCursor;
        private BuildingBlockRotator negativeRotator;

        private MapData mapData = null;

        private  Vector3Int lastCursorPosition;
        private float lastCursorRotationAngle;
        private bool canBuildOnLastCursorPosition = false;


        public bool CanBuildNow
        {
            get
            {
                return isCursorOn && canBuildOnLastCursorPosition;
            }
        }
        public MapObjectType CurrentCursorType
        {
            get
            {
                return currentCursorType;
            }
        }
        public Vector3 CurrentCursorPosition
        {
            get
            {
                return lastCursorPosition;
            }
        }
        public float CurrentCursorRotationAngle
        {
            get
            {
                return lastCursorRotationAngle;
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
            this.currentCursorType = type;
            cursorDescription = objectsMapper.GetDescriptionByType(type);
            positiveCursor = cursorDescription.PositiveCursor;
            positiveRotator = positiveCursor.GetComponent<BuildingBlockRotator>();
            negativeCursor = cursorDescription.NegativeCursor;
            negativeRotator = negativeCursor.GetComponent<BuildingBlockRotator>();
            this.mapData = mapData;
            isCursorOn = true;
        }

        ///////////////////////////////////////////////////////////////////////////////

        void LateUpdate()
        {
            if (isCursorOn)
            {
                RaycastHit hit = CommandKeeper.GetCameraRaycastHit();

                if (hit.collider != null)
                {
                    BuildingBlockPassport pass = hit.collider.GetComponentInParent<BuildingBlockPassport>();
                    if (pass != null)
                    {
                        /////

                        Vector3 hitObjPosition = pass.transform.position;

                        Vector3Int hitObjPositionInt = new Vector3Int(
                            (int)Mathf.Floor(hitObjPosition.x),
                            (int)Mathf.Round(hitObjPosition.y),
                            (int)Mathf.Floor(hitObjPosition.z)
                            );


                        ////////
                        
                        lastCursorRotationAngle = CalculateCursorRotation(CommandKeeper.GetPlayerRotationAngle());

                        canBuildOnLastCursorPosition = CanIBuildAndWhere(hitObjPositionInt, hit.point, out lastCursorPosition);

                        positiveCursor.position = lastCursorPosition;
                        negativeCursor.position = lastCursorPosition;

                        if (positiveRotator != null)
                        {
                            positiveRotator.SetHorizontalRotation(lastCursorRotationAngle);
                        }
                        if (negativeRotator != null)
                        {
                            negativeRotator.SetHorizontalRotation(lastCursorRotationAngle);
                        }

                        positiveCursor.gameObject.SetActive(canBuildOnLastCursorPosition);
                        negativeCursor.gameObject.SetActive(!canBuildOnLastCursorPosition);

                        ////////////////////////

                        //Dimensions dim = cursorDescription.Dimensions;
                        //Dimensions.SimpleRect rect = dim.Rect;

                        //int h;
                        //ProbeBuildingPlace(hitObjPositionInt, rect, out canBuildOnLastCursorPosition, out h);

                        //LastCursorPosition = new Vector3Int(hitObjPositionInt.x, hitObjPositionInt.y + h, hitObjPositionInt.z);
                        //positiveCursor.position = LastCursorPosition;
                        //negativeCursor.position = LastCursorPosition;

                        //// !!!! TODO
                        //if (h > 0)
                        //{
                        //    MapObjectType type = mapData.GetObjectTypeAt(LastCursorPosition);
                        //    if (type != MapObjectType.AIR)
                        //    {
                        //        canBuildOnLastCursorPosition = false;
                        //    }
                        //}

                        //positiveCursor.gameObject.SetActive(canBuildOnLastCursorPosition);
                        //negativeCursor.gameObject.SetActive(!canBuildOnLastCursorPosition);

                        ///////////////

                    } // if (pass != null) ///
                }
                else // raycasted collider == null ///
                {
                    positiveCursor.gameObject.SetActive(false);
                    negativeCursor.gameObject.SetActive(false);
                }
            } // if isCurosrOn ///

        } // LateUpdate() ////

        private float CalculateCursorRotation(float playerViewAngle)
        {
            float discreteRotationAngle = playerViewAngle % 360;
            discreteRotationAngle = Mathf.Round(discreteRotationAngle / 90);
            discreteRotationAngle *= 90;

            return discreteRotationAngle;
        }

        private bool CanIBuildAndWhere(Vector3Int parentPosition, Vector3 raycastPosition, out Vector3Int newCursorPosition)
        {
            MapObjectType parentType = mapData.GetObjectTypeAt(parentPosition);
            MapObjectDescription parentDescription = objectsMapper.GetDescriptionByType(parentType);
            BuildCondition.RelativePosition relativePosition;
            bool canBuildHere = parentDescription.CanBeBuildNearMe(CurrentCursorType, out relativePosition);

            newCursorPosition = parentPosition;


            if (!canBuildHere)
            {
                newCursorPosition = parentPosition;
                return canBuildHere;
            }
            else
            {   /// If maybe can build ////
                switch (relativePosition)
                {
                    case BuildCondition.RelativePosition.OVER:
                        newCursorPosition.y += parentDescription.Dimensions.Height;
                        break;
                    case BuildCondition.RelativePosition.ON_THE_SIDE:
                        // Let's fing appropriate position on side of the selected (raycast hit) map object ///
                        // So far implementation for 1x1 objects only //
                        Vector3Int frontPos = CalculateOnTheSidePosition(parentPosition, raycastPosition);

                        newCursorPosition = frontPos;
                        break;
                    case BuildCondition.RelativePosition.INSTEAD:
                        newCursorPosition = parentPosition;
                        break;
                }

                if (relativePosition != BuildCondition.RelativePosition.INSTEAD)
                {
                    //for (int ix = )   TODO: for building objects greater than 1x1x1?
                    //    for(int iy=)
                    //        for(int iz = )

                    MapObjectType type = mapData.GetObjectTypeAt(newCursorPosition);
                    if (type != MapObjectType.AIR)
                    {
                        canBuildHere = false;
                    }
                }
            } // if (maybe can build) ///

            return canBuildHere;
        } // CanIBuildAndWhere() /////

        private static Vector3Int CalculateOnTheSidePosition(Vector3Int parentPosition, Vector3 raycastPosition)
        {
            Vector3Int frontPos = new Vector3Int(parentPosition.x, parentPosition.y, parentPosition.z);

            float deltaX = raycastPosition.x - parentPosition.x;
            float deltaZ = raycastPosition.z - parentPosition.z;
            if (deltaX > deltaZ && deltaX + deltaZ < 1)
            {
                frontPos.z -= 1;
            }
            else if (deltaX > deltaZ && deltaX + deltaZ > 1)
            {
                frontPos.x += 1;
            }
            else if (deltaX < deltaZ && deltaX + deltaZ < 1)
            {
                frontPos.x -= 1;
            }
            else
            {
                frontPos.z += 1;
            }

            return frontPos;
        }



        //private void ProbeBuildingPlace(Vector3Int pos, Dimensions.SimpleRect rect, out bool canBuildHere, out int h)
        //{


        //    Vector3Int p1 = new Vector3Int(pos.x + rect.MinX, pos.y, pos.z + rect.MinZ);
        //    Vector3Int p2 = new Vector3Int(pos.x + rect.MaxX - 1, pos.y, pos.z + rect.MinZ);
        //    Vector3Int p3 = new Vector3Int(pos.x + rect.MinX, pos.y, pos.z + rect.MaxZ - 1);
        //    Vector3Int p4 = new Vector3Int(pos.x + rect.MaxX - 1, pos.y, pos.z + rect.MaxZ - 1);

        //    MapObjectType type = mapData.GetObjectTypeAt(p1);
        //    canBuildHere = cursorDescription.CanBuildOn(type);
        //    MapObjectDescription desc = objectsMapper.GetDescriptionByType(type);
        //    h = desc.Dimensions.Height;

        //    type = mapData.GetObjectTypeAt(p2);
        //    canBuildHere = canBuildHere && cursorDescription.CanBuildOn(type);
        //    desc = objectsMapper.GetDescriptionByType(type);
        //    int h2 = desc.Dimensions.Height;
        //    canBuildHere = canBuildHere && (h == h2);

        //    type = mapData.GetObjectTypeAt(p3);
        //    canBuildHere = canBuildHere && cursorDescription.CanBuildOn(type);
        //    desc = objectsMapper.GetDescriptionByType(type);
        //    int h3 = desc.Dimensions.Height;
        //    canBuildHere = canBuildHere && (h == h3);

        //    type = mapData.GetObjectTypeAt(p4);
        //    canBuildHere = canBuildHere && cursorDescription.CanBuildOn(type);
        //    desc = objectsMapper.GetDescriptionByType(type);
        //    int h4 = desc.Dimensions.Height;
        //    canBuildHere = canBuildHere && (h == h4);
        //} // probe building place() ///



    } // end of class ///
} // namespace ///

