using System.Collections.Generic;
using System.Linq;
using APP;
using DG.Tweening;
using Managers;
using UniRx;
using UnityEngine;
using Utils;

namespace Environment
{
    public sealed class Collector : MonoBehaviour
    {
        private MWorld _world;
        
        [SerializeField] private Transform _stepTransform;
        [SerializeField] private Transform _itemsTransform;
        [SerializeField] private Renderer[] _models;

        public Transform GetItemTransform => _itemsTransform;

        [HideInInspector] public List<Step> steps;
        [HideInInspector] public Color color;

        public readonly ReactiveProperty<int> index = new ReactiveProperty<int>();
        public readonly ReactiveCommand<int> onShowRoad = new ReactiveCommand<int>();
        
        private readonly ReactiveCommand<Color> _onPaintRoad = new ReactiveCommand<Color>();

        private void OnEnable()
        {
            _world = APPCore.Instance.world;
        }

        private void OnDisable()
        {
            steps.Clear();
        }

        private void Start()
        {
            InitRoad();

            _onPaintRoad
                .First()
                .Subscribe(c =>
                {
                    gameObject.layer = Layers.Deactivate;
                    
                    _world.CollectorsColliders.Remove(this);

                    const float duration = 0.25f;

                    for (int i = 0; i < _models.Length; i++)
                    {
                        _models[i].material
                            .DOColor(c, duration)
                            .SetEase(Ease.Linear)
                            .SetLoops(9, LoopType.Yoyo);
                    }

                    for (int i = 0; i < steps.Count; i++)
                    {
                        steps[i].GetRenderer.material
                            .DOColor(c, duration)
                            .SetEase(Ease.Linear)
                            .SetLoops(9, LoopType.Yoyo);
                    }
                })
                .AddTo(this);
            
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
                    _onPaintRoad.Execute(color);
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