using UnityEngine;
using System.Collections;

namespace Crab.Events
{
    public class ECameraTarget : Crab.Event
    {
        protected override void JustStarted()
        {
            Cache.Get.cameraController.target = this;
        }
    }
}
