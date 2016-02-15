using UnityEngine;
using System.Collections;
using Crab.Events;
using Crab.Components;

namespace Crab.Controllers
{
    [RequireComponent(typeof(CMovement))]
    public class PlayerController : EntityController
    {
        private CMovement movement;
        private new CameraMovement camera;
        private TouchManager touch = new TouchManager();
        private Vector3 touchPos;
        private CameraController camControl;

        void Awake()
        {
            me = GetComponent<Entity>();
            camControl = Cache.Get.cameraController;
            movement = me.Movement;
            touch.OnSwipe += OnSwipe;
            touch.OnTouch += OnTouch;

        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0)) {
                OnTouch(Input.mousePosition);
            }

            //Key Camera rotation
            if (Input.GetKeyDown(KeyCode.A)) {
                GameStats.NextRotation();
            }
            else if (Input.GetKeyDown(KeyCode.D)) {
                GameStats.PreviousRotation();
            }
        }

        void FixedUpdate() {
            touch.Update();
        }

        //Touch Movement
        void OnTouch(Vector3 touchPosition) {
            Ray ray = camControl.cam.ScreenPointToRay(touchPosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, ~(1 << LayerMask.NameToLayer("Dissolved"))))
            {
                touchPos = hit.point;

                EDoor door = hit.collider.GetComponentInParent<EDoor>();
                if (door)
                {
                    if (door.IsEnabled())
                        door.StartEvent();
                    return;
                }

                movement.AIMove(hit.point);
            }
        }

        //Touch Swipe Camera Rotation
        void OnSwipe(SwipeType type) {
            if (type == SwipeType.RIGHT)
            {
                GameStats.NextRotation();
            }
            else if(type == SwipeType.LEFT)
            {
                GameStats.PreviousRotation();
            }
        }

        //DEBUG
        void OnDrawGizmos()
        {
            if(touchPos != Vector3.zero)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(touchPos, 0.25f);
            }
        }
    }
}