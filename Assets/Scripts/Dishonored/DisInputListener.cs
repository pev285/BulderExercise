using BuilderGame.DataBus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NONAME {

	public class DisInputListener : MonoBehaviour 
	{
        private void Start()
        {
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                CommandKeeper.SwitchCamera();
            }
        }

    } // End Of Class //

} // namespace ////



