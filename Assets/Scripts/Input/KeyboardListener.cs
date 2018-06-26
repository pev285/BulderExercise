using BuilderGame.DataBus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.InputReading
{

	public class KeyboardListener : MonoBehaviour 
	{

        void Update()
        {
            float horizontalMouseAxis = Input.GetAxis("Mouse X");
            float verticalMouseAxis = Input.GetAxis("Mouse Y");
            CommandKeeper.OnPlayerRotateCommand(horizontalMouseAxis, verticalMouseAxis);

            float horizontalAxis = Input.GetAxis("Horizontal");
            float verticalAxis = Input.GetAxis("Vertical");
            CommandKeeper.OnPlayerMoveCommand(horizontalAxis, verticalAxis);

            if(Input.GetMouseButtonUp(0))
            {
                CommandKeeper.OnBuildCommand();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                CommandKeeper.OnBuildingBlockChoose(Model.MapObjectType.SMALL_PLATFORM);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                CommandKeeper.OnBuildingBlockChoose(Model.MapObjectType.SMALL_PILLAR);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                CommandKeeper.OnBuildingBlockChoose(Model.MapObjectType.PLATFORM);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                CommandKeeper.OnBuildingBlockChoose(Model.MapObjectType.PILLAR);
            }


        }

    } // End Of Class //

} // namespace ////



