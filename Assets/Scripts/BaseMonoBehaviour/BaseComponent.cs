using UnityEngine;

namespace BaseMonoBehaviour
{
    public abstract class BaseComponent : MonoBehaviour
    {
        private void OnEnable()
        {
            Enable();
        }

        private void OnDisable()
        {
            Disable();
        }

        private void Start()
        {
            Init();
        }

        protected virtual void Init() {}
        protected virtual void Enable() {}
        protected virtual void Disable() {}
    }
}