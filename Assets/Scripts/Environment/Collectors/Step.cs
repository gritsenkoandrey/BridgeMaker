using BaseMonoBehaviour;
using UnityEngine;

namespace Environment.Collectors
{
    public sealed class Step : BaseComponent
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Collider _collider;

        public Collider GetCollider => _collider;

        protected override void Init()
        {
            base.Init();
            
            _renderer.enabled = false;
            _collider.enabled = false;
        }
    }
}