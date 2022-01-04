using UnityEngine;

namespace Managers
{
    public sealed class MCamera : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        public Transform GetCameraTransform => _camera.transform;
        public Camera GetCamera => _camera;
    }
}