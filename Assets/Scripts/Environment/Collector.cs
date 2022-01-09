using System.Collections.Generic;
using System.Linq;
using APP;
using BaseMonoBehaviour;
using DG.Tweening;
using Managers;
using UniRx;
using UnityEngine;
using Utils;

namespace Environment
{
    public sealed class Collector : BaseComponent
    {
        [SerializeField] private Transform _stepTransform;
        [SerializeField] private Transform _itemsTransform;
        [SerializeField] private Renderer[] _models;

        private MWorld _world;
        
        public Transform GetItemTransform => _itemsTransform;

        [HideInInspector] public List<Step> steps;
        [HideInInspector] public Color color;

        public readonly ReactiveProperty<int> index = new ReactiveProperty<int>();
        public readonly ReactiveCommand<int> onShowRoad = new ReactiveCommand<int>();

        protected override void Enable()
        {
            _world = APPCore.Instance.GetWorld;
        }

        protected override void Disable()
        {
            steps.Clear();
        }

        protected override void Initialize()
        {
            InitRoad();
            
            onShowRoad
                .Subscribe(i =>
                {
                    steps[i].gameObject.SetActive(true);
                })
                .AddTo(this);

            index
                .Where(value => value == steps.Count)
                .First()
                .Subscribe(_ =>
                {
                    gameObject.layer = Layers.Deactivate;
                    
                    _world.CollectorsColliders.Remove(this);

                    const float duration = 0.25f;

                    for (int i = 0; i < _models.Length; i++)
                    {
                        _models[i].material
                            .DOColor(color, duration)
                            .SetEase(Ease.Linear)
                            .SetLoops(9, LoopType.Yoyo);
                    }

                    for (int i = 0; i < steps.Count; i++)
                    {
                        steps[i].GetRenderer.material
                            .DOColor(color, duration)
                            .SetEase(Ease.Linear)
                            .SetLoops(9, LoopType.Yoyo);
                    }
                })
                .AddTo(this);
        }

        private void InitRoad()
        {
            steps = _stepTransform.GetComponentsInChildren<Step>().ToList();
            steps.ForEach(s => s.gameObject.SetActive(false));
        }
    }
}