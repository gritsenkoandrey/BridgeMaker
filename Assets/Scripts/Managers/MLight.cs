using UnityEngine;

namespace Managers
{
    public sealed class MLight : Manager
    {
        [SerializeField] private Light _light;

        public Light GetLight => _light;

        protected override void First()
        {
            Container.Add(typeof(MLight), this);
        }
    }
}