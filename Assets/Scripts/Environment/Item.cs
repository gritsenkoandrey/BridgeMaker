using APP;
using DG.Tweening;
using Managers;
using UniRx;
using UnityEngine;
using Utils;

namespace Environment
{
    public sealed class Item : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;

        private MWorld _world;

        private Sequence _sequence;
        private Tweener _tween;
        public Renderer GetRenderer => _renderer;

        public readonly ReactiveCommand<Transform> onPick = new ReactiveCommand<Transform>();
        public readonly ReactiveCommand<Transform> onDrop = new ReactiveCommand<Transform>();
        public readonly ReactiveCommand<Collector> onMove = new ReactiveCommand<Collector>();

        private void OnEnable()
        {
            _world = APPCore.Instance.world;
        }
        
        private void OnDisable()
        {
            _sequence.KillTween();
            _tween.KillTween();
        }

        private void Start()
        {
            Transform item = gameObject.transform;

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

            onDrop
                .First()
                .Subscribe(parent =>
                {

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
                        .Append(item.DOMove(collector.steps[index].transform.position, offset * index))
                        .Join(item.DOLocalRotate(Vector3.zero, offset * index))
                        .Join(_renderer.transform.DOScale(collector.steps[index].transform.localScale, offset * index))
                        .SetEase(Ease.Linear)
                        .AppendCallback(() =>
                        {
                            _renderer.enabled = false;
                            
                            collector.onShowRoad.Execute(index);
                        });

                    collector.color = _renderer.material.color;
                    
                    collector.index.Value++;
                })
                .AddTo(this);
        }
    }
}