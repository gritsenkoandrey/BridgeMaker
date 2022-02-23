using BaseMonoBehaviour;
using Managers;
using UnityEngine;

namespace Environment.Platforms
{
    public sealed class Platform : BaseComponent, IPlatform
    {
        [SerializeField] private int _index;
        private MWorld _world;

        public int Index => _index;
        public int Count { get; private set; }
        
        protected override void Init()
        {
            base.Init();
        }

        protected override void Enable()
        {
            base.Enable();
            
            _world = Manager.Resolve<MWorld>();
            
            _world.Platforms.Add(this);
        }

        protected override void Disable()
        {
            base.Disable();
        }

        public void SetCountItems(int count)
        {
            Count = count;
        }
    }
}