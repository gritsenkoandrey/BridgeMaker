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
        
        private MGame _game;
        
        private Sequence _sequence;
        private Tweener _tween;

        public Renderer GetRenderer => _renderer;
        
        public readonly ReactiveCommand<Transform> OnPick = new ReactiveCommand<Transform>();
        public readonly ReactiveCommand<Transform> OnDrop = new ReactiveCommand<Transform>();
        public readonly ReactiveCommand<Collector> OnMove = new ReactiveCommand<Collector>();

        private void OnEnable()
        {
            _game = APPCore.Instance.game;
        }

        private void Start()
        {
            Transform item = gameObject.transform;
            
            OnPick
                .First()
                .Subscribe(parent =>
                {
                    _game.Items.Add(this);
                    
                    item.gameObject.layer = Layers.Deactivate;
                    item.SetParent(parent);

                    Vector3 pos = new Vector3(0f, _game.Items.Count / 5f, 0f);

                    _sequence = DoTweenExtension.RefreshSequence(_sequence);

                    _sequence
                        .Append(item.DOLocalJump(pos, 1.5f, 1, 0.5f))
                        .Join(item.DOLocalRotate(Vector3.zero, 0.5f))
                        .SetEase(Ease.Linear);
                })
                .AddTo(this);

            OnDrop
                .First()
                .Subscribe(parent =>
                {

                })
                .AddTo(this);
            
            OnMove
                .First()
                .Subscribe(collector =>
                {
                    _game.Items.Remove(this);

                    const float offset = 0.25f;
                    
                    item.SetParent(collector.GetItemTransform);

                    int index = collector.index;
                    
                    _sequence = DoTweenExtension.RefreshSequence(_sequence);

                    _sequence
                        .Append(item.DOMove(collector.steps[index].transform.position, offset * index))
                        .Join(item.DOLocalRotate(Vector3.zero, offset * index))
                        .Join(_renderer.transform.DOScale(collector.steps[index].transform.localScale, offset * index))
                        .SetEase(Ease.Linear)
                        .AppendCallback(() =>
                        {
                            _renderer.enabled = false;
                            collector.OnActivateRoad.Execute(index);
                        });

                    collector.index++;
                })
                .AddTo(this);
        }
    }
}