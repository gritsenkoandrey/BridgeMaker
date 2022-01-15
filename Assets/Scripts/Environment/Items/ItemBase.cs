using BaseMonoBehaviour;
using DG.Tweening;
using Environment.Collectors;
using Managers;
using UniRx;
using UnityEngine;
using Utils;

namespace Environment.Items
{
    public abstract class ItemBase : BaseComponent
    {
        public ItemSettings ItemSettings { get; set; }

        protected MWorld world;
        
        protected Tweener tween;
        protected Sequence sequence;
        
        public readonly ReactiveCommand<Transform> onPick = new ReactiveCommand<Transform>();
        public readonly ReactiveCommand<Collector> onMove = new ReactiveCommand<Collector>();

        protected override void Init()
        {
            base.Init();
            
            switch (ItemSettings.itemType)
            {
                case ItemType.None:
                    break;
                case ItemType.Stay:
                    break;
                case ItemType.Move:
                    MoveAnimation();
                    break;
            }
        }

        protected override void Enable()
        {
            base.Enable();

            world = Manager.Resolve<MWorld>();
        }

        protected override void Disable()
        {
            base.Disable();
            
            tween.KillTween();
            sequence.KillTween();
        }

        private void MoveAnimation()
        {
            tween = transform
                .DOMove(ItemSettings.vector, ItemSettings.duration)
                .SetRelative()
                .SetEase(Ease.Linear)
                .SetDelay(ItemSettings.delay)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}