using UnityEngine;

namespace Managers
{
    public sealed class MLight : MonoBehaviour
    {
        [SerializeField] private Light _light;

        public Light GetLight => _light;
    }
}