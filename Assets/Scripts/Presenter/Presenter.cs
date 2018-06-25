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

                    if ((int)type == 103)
                    {
                        Debug.Log("103 = " + type.ToString());
                    }

                    GameObject prefab = objMaper.GetPrefabOfType(type);

                    Instantiate(prefab, new Vector3(xi, 0, zi), Quaternion.identity, environmentParent);
                }
            }

            buildingCursor.SetCursorOn(MapObjectType.SMALL_PLATFORM, mapdata);
		} // Start() //
		
	} // End Of Class //

} // namespace ////



