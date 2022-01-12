using UnityEngine;

namespace Managers
{
    public sealed class MCamera : Manager
    {
        [SerializeField] private Camera _camera;

        public Transform GetCameraTransform => _camera.transform;
        public Camera GetCamera => _camera;

        protected override void Register()
        {
            RegisterManager(this);
        }
        
        protected override void Disable()
        {
            UnregisterManager(this);
        }
    }
}