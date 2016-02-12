using UnityEngine;
using System.Collections;
using Crab;
using Crab.Components;

namespace Crab.Controllers
{
    [RequireComponent(typeof(CMovement))]
    public class PlayerController : EntityController
    {
        private CMovement movement;
        private new CameraMovement camera;
        private TouchManager touch = new TouchManager();
        private Vector3 touchPosition;

        void Awake()
        {
            me = GetComponent<Entity>();
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
            UnityEngine.Debug.Log("Move here!");
            Ray ray = Cache.Get.cameraController.cam.ScreenPointToRay(touchPosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, LayerMask.NameToLayer("Terrain")))
            {
                this.touchPosition = touchPosition;
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
        void OnDrawGizmosSelected()
        {
            if(touchPosition != Vector3.zero)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(transform.position, 0.25f);
            }
        }
    }
}