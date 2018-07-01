using BuilderGame.DataBus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.View
{
    public class LaddersTrigger : MonoBehaviour
    {
        private const string PLAYER_TAG = "Player";

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == PLAYER_TAG)
            {
                CommandKeeper.SetPlayerOnALadder(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == PLAYER_TAG)
            {
                CommandKeeper.SetPlayerOnALadder(false);
            }
        }
    } // end of class //
} // end of namespace ///

