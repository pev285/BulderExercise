using BuilderGame.Constructing;
using BuilderGame.DataBus;
using BuilderGame.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Presenter {

	public class Presenter : MonoBehaviour 
	{
        [SerializeField]
        private ObjectsMapper objMaper;

        [SerializeField]
        private Transform environmentParent;

        private MapData mapdata = new MapData();

        [SerializeField]
        private BuildingCursor buildingCursor;

		void Start () 
		{
            for (int xi = 0; xi < mapdata.xSize; xi++)
            {
                for (int zi = 0; zi < mapdata.zSize; zi++)
                {
                    MapObjectType type = mapdata.GetObjectTypeAt(xi, 0, zi);

                    GameObject prefab = objMaper.GetPrefabOfType(type);

                    Instantiate(prefab, new Vector3(xi, 0, zi), Quaternion.identity, environmentParent);
                }
            }

            CommandKeeper.OnBuildCommand += OnBuildCommand;
            CommandKeeper.OnBuildingBlockChoose += OnBuildingBlockChoose;


        } // Start() //
		

        private void OnBuildCommand()
        {
            if (buildingCursor.CanBuildNow)
            {
                //buildingCursor.SetCursorOff();
                MapObjectType type = buildingCursor.CurrentCursorType;
                Vector3 pos = buildingCursor.CurrentCursorPosition;
                GameObject prefab = objMaper.GetPrefabOfType(type);
                GameObject go = Instantiate(prefab, pos, Quaternion.identity, environmentParent);
                BuildingBlockRotator rotator = go.GetComponent<BuildingBlockRotator>();
                if(rotator != null)
                {
                    //Debug.Log("Rotator is found");
                    rotator.SetHorizontalRotation(buildingCursor.CurrentCursorRotationAngle);
                }
                //else
                //{
                //    Debug.Log("Rotator is not found");
                //}

                mapdata.SetObjectTypeAt((int)pos.x, (int)pos.y, (int)pos.z, type);
            }
        }

        private void OnBuildingBlockChoose(MapObjectType type)
        {
            buildingCursor.SetCursorOff();
            buildingCursor.SetCursorOn(type, mapdata);
        }


    } // End Of Class //

} // namespace ////



