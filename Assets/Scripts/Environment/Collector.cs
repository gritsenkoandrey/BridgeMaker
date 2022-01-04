using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace Environment
{
    public sealed class Collector : MonoBehaviour
    {
        [SerializeField] private Transform _stepTransform;
        [SerializeField] private Transform _itemsTransform;
        [SerializeField] private Renderer[] _models;

        public Transform GetItemTransform => _itemsTransform;

        [HideInInspector] public int index;
        [HideInInspector] public List<Step> steps;

        public readonly ReactiveCommand<int> OnActivateRoad = new ReactiveCommand<int>();
        public readonly ReactiveCommand<Color> OnPaintRoad = new ReactiveCommand<Color>();

        private void Start()
        {
            InitRoad();

            OnActivateRoad
                .Subscribe(i =>
                {
                    steps[i].gameObject.SetActive(true);
                })
                .AddTo(this);

            OnPaintRoad
                .First()
                .Subscribe(color =>
                {
                    foreach (Renderer r in _models)
                    {
                        r.material
                            .DOColor(color, 0.25f)
                            .SetEase(Ease.Linear)
                            .SetLoops(9, LoopType.Yoyo);
                    }

                    foreach (Step s in steps)
                    {
                        s.GetRenderer.material
                            .DOColor(color, 0.25f)
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