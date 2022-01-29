using System;
using System.Collections.Generic;
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

        private Dictionary<ItemType, Action> _initItem;

        public readonly ReactiveCommand<Transform> onPick = new ReactiveCommand<Transform>();
        public readonly ReactiveCommand<Collector> onMove = new ReactiveCommand<Collector>();
        public readonly ReactiveCommand<Transform> onDrop = new ReactiveCommand<Transform>();

        protected override void Init()
        {
            base.Init();
            
            _initItem[ItemSettings.itemType].Invoke();
        }

        protected override void Enable()
        {
            base.Enable();

            world = Manager.Resolve<MWorld>();
            
            _initItem = new Dictionary<ItemType, Action>
            {
                { ItemType.None, () => {Debug.Log("None");}},
                { ItemType.Stay, () => {Debug.Log("Stay");}},
                { ItemType.Move, MoveAnimation}
            };
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