using System.Linq;
using BaseMonoBehaviour;
using DG.Tweening;
using Managers;
using UniRx;
using UnityEngine;
using Utils;

namespace Environment.Collectors
{
    public sealed class Collector : BaseComponent
    {
        [SerializeField] private Transform _stepTransform;
        [SerializeField] private Transform _itemsTransform;
        [SerializeField] private Renderer[] _models;

        private MWorld _world;
        
        public Transform GetItemTransform => _itemsTransform;
        public Step[] Steps { get; private set; }

        public readonly ReactiveProperty<int> index = new ReactiveProperty<int>();
        public readonly ReactiveCommand<Color> onPaint = new ReactiveCommand<Color>();
        public readonly ReactiveCommand<int> onShowRoad = new ReactiveCommand<int>();
        public readonly ReactiveCommand disableNeighbors = new ReactiveCommand();

        protected override void Init()
        {
            base.Init();
            
            Steps = _stepTransform.GetComponentsInChildren<Step>();
            
            onShowRoad
                .Subscribe(i =>
                {
                    Steps[i].GetCollider.enabled = true;
                })
                .AddTo(lifetimeDisposable);

            onPaint
                .First()
                .Subscribe(color =>
                {
                    const float duration = 0.25f;

                    _models
                        .ForEach(m => m.material
                            .DOColor(color, duration)
                            .SetEase(Ease.Linear));
                })
                .AddTo(lifetimeDisposable);

            disableNeighbors
                .First()
                .Subscribe(_ =>
                {
                    _world.CollectorsColliders
                        .Where(collector => collector != this)
                        .ToList()
                        .ForEach(c => c.gameObject.layer = Layers.Deactivate);
                })
                .AddTo(lifetimeDisposable);
            
            index
                .Where(value => value == Steps.Length)
                .First()
                .Subscribe(_ =>
                {
                    gameObject.layer = Layers.Deactivate;
                    
                    _world.CollectorsColliders.Remove(this);
                    
                    _world.CollectorsColliders
                        .ForEach(c => c.gameObject.layer = Layers.Collector);
                })
                .AddTo(lifetimeDisposable);
        }

        protected override void Enable()
        {
            base.Enable();
            
            _world = Manager.Resolve<MWorld>();
            
            _world.CollectorsColliders.Add(this);
        }
    }
}