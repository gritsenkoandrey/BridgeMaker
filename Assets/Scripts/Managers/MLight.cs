using UnityEngine;

namespace Managers
{
    public sealed class MLight : Manager
    {
        [SerializeField] private Light _light;

        public Light GetLight => _light;

        protected override void Register()
        {
            RegisterManager(this);
        }
        
        protected override void Disable()
        {
            base.Disable();

            UnregisterManager(this);
        }
    }
}