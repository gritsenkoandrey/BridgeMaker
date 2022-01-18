using Cinemachine;
using UnityEngine;

namespace Levels
{
    public sealed class Level : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;

        public CinemachineVirtualCamera GetCamera => _cinemachineVirtualCamera;
    }
}