using UnityEngine;
using System.Collections;
using Crab.Events;
using Crab.Controllers;

namespace Crab.Events
{

    public class EChest : Crab.Event
    {
        private PlayerController playerInside;

        void OnTriggerEnter(Collider col) {
            PlayerController player = col.GetComponent<PlayerController>();
            if (player)
            {
                playerInside = player;
            }
        }

        void OnTriggerExit(Collider col)
        {
            PlayerController player = col.GetComponent<PlayerController>();
            if (player)
            {
                playerInside = null;
            }

        }



        protected override void JustStarted()
        {
            
        }

    }
}
