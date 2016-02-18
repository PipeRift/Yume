namespace Crab
{
    using UnityEngine;
    using UnityEngine.Events;
    using System.Collections;
    using Crab.Events;

    public class Event : MonoBehaviour
    {
        [SerializeField]
        new protected bool enabled = true;
        public bool disableWhenDone = false;

        public UnityEvent startEvent;

        void Start()
        {
            OnGameStart(SceneScript.Instance);
        }

        public virtual void Reset()
        {
            if (!gameObject.activeSelf)
                gameObject.SetActive(true);
        }
        
        public void StartEvent() {
            if (!enabled)
                return;

            if (disableWhenDone)
            {
                enabled = false;
            }
            JustStarted();
            startEvent.Invoke();
        }

        public void Enable() {
            enabled = true;
        }

        public bool IsEnabled() {
            return enabled;
        }

        //Events
        protected virtual void OnGameStart(SceneScript scene) { }

        protected virtual void JustStarted() {
        }
    }
}
