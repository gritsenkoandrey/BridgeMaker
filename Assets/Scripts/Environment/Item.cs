using APP;
using BaseMonoBehaviour;
using DG.Tweening;
using Managers;
using UniRx;
using UnityEngine;
using Utils;

namespace Environment
{
    public sealed class Item : BaseComponent
    {
        [SerializeField] private Renderer _renderer;

        private MWorld _world;
        private Sequence _sequence;
        private Tweener _tween;
        
        public Renderer GetRenderer => _renderer;

        public readonly ReactiveCommand<Transform> onPick = new ReactiveCommand<Transform>();
        public readonly ReactiveCommand<Collector> onMove = new ReactiveCommand<Collector>();

        protected override void Enable()
        {
            _world = APPCore.Instance.GetWorld;
            
            _world.ItemsColliders.Add(this);
        }

        protected override void Disable()
        {
            _sequence.KillTween();
            _tween.KillTween();
        }

        protected override void Initialize()
        {
            Transform item = transform;

            _tween = item
                .DOMoveY(0.25f, U.Random(0.75f, 1f))
                .SetRelative()
                .SetEase(Ease.Linear)
                .SetDelay(U.Random(0f, 1f))
                .SetLoops(-1, LoopType.Yoyo);
            
            onPick
                .First()
                .Subscribe(parent =>
                {
                    _world.CharacterItems.Add(this);
                    _world.ItemsColliders.Remove(this);
                    
                    item.gameObject.layer = Layers.Deactivate;
                    item.SetParent(parent);

                    Vector3 pos = new Vector3(0f, _world.CharacterItems.Count / 5f, 0f);

                    _sequence = _sequence.RefreshSequence();
                    _tween.KillTween();

                    _sequence
                        .Append(item.DOLocalJump(pos, 1.5f, 1, 0.5f))
                        .Join(item.DOLocalRotate(Vector3.zero, 0.5f))
                        .SetEase(Ease.Linear);
                })
                .AddTo(this);
            
            onMove
                .First()
                .Subscribe(collector =>
                {
                    _world.CharacterItems.Remove(this);

                    const float offset = 0.25f;
                    
                    item.SetParent(collector.GetItemTransform);

                    int index = collector.index.Value;
                    
                    _sequence = _sequence.RefreshSequence();
                    _tween.KillTween();

                    _sequence
                        .Append(item.DOMove(collector.Steps[index].transform.position, offset * index))
                        .Join(item.DOLocalRotate(Vector3.zero, offset * index))
                        .Join(_renderer.transform.DOScale(collector.Steps[index].transform.localScale, offset * index))
                        .SetEase(Ease.Linear)
                        .AppendCallback(() => collector.onShowRoad.Execute(index));

                    collector.index.Value++;
                    
                    collector.onPaint.Execute(_renderer.material.color);
                })
                .AddTo(this);
        }
    }
}