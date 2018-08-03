using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NONAME {

	public class UniverseController : MonoBehaviour 
	{
        private void Awake()
        {
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }
		
	} // End Of Class //

} // namespace ////



