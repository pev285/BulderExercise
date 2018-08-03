using BuilderGame.DataBus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NONAME {

	public class TwinCameraController : MonoBehaviour 
	{
        [SerializeField]
        Camera _hiddenCamera;
        [SerializeField]
        Camera _activeCamera;

        private void Awake()
        {
            var rt = new RenderTexture(Screen.width, Screen.height, 24);
            Shader.SetGlobalTexture("_TimeCrackTexture", rt);
            _hiddenCamera.targetTexture = rt;

            CommandKeeper.SwitchCamera += SwapCameras;
        }

        public void SwapCameras()
        {
            _activeCamera.targetTexture = _hiddenCamera.targetTexture;
            _hiddenCamera.targetTexture = null;

            var swapCamera = _activeCamera;
            _activeCamera = _hiddenCamera;
            _hiddenCamera = swapCamera;
        }

    } // End Of Class //

} // namespace ////



