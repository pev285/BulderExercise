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


        }

    } // End Of Class //

} // namespace ////



