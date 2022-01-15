using DG.Tweening;
using UniRx;
using UnityEngine;
using Utils;

namespace Environment.Items
{
    public sealed class Item : ItemBase
    {
        [SerializeField] private Renderer _renderer;
        
        protected override void Init()
        {
            base.Init();
            
            _renderer.material.color = ItemSettings.color;

            Transform item = transform;

            onPick
                .First()
                .Subscribe(parent =>
                {
                    world.CharacterItems.Add(this);
                    world.ItemsColliders.Remove(this);
                    
                    item.gameObject.layer = Layers.Deactivate;
                    item.SetParent(parent);

                    Vector3 pos = new Vector3(0f, world.CharacterItems.Count / 5f, 0f);

                    tween.KillTween();
                    sequence = sequence.RefreshSequence();

                    sequence
                        .Append(item.DOLocalJump(pos, 1.5f, 1, 0.5f))
                        .Join(item.DOLocalRotate(Vector3.zero, 0.5f))
                        .SetEase(Ease.Linear);
                })
                .AddTo(lifetimeDisposable);
            
            onMove
                .First()
                .Subscribe(collector =>
                {
                    world.CharacterItems.Remove(this);

                    const float offset = 0.25f;
                    
                    item.SetParent(collector.GetItemTransform);

                    int index = collector.index.Value;
                    
                    tween.KillTween();
                    sequence = sequence.RefreshSequence();

                    sequence
                        .Append(item.DOMove(collector.Steps[index].transform.position, offset * index))
                        .Join(item.DOLocalRotate(Vector3.zero, offset * index))
                        .Join(_renderer.transform.DOScale(collector.Steps[index].transform.localScale, offset * index))
                        .SetEase(Ease.Linear)
                        .AppendCallback(() => collector.onShowRoad.Execute(index));

                    collector.index.Value++;
                    
                    collector.onPaint.Execute(ItemSettings.color);
                })
                .AddTo(lifetimeDisposable);
        }

        protected override void Enable()
        {
            base.Enable();

            world.ItemsColliders.Add(this);
        }

        protected override void Disable()
        {
            base.Disable();
        }
    }
}