using System.Linq;
using Environment.Collectors;
using Environment.Items;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Utils;

namespace Characters
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class CharacterCollision : CharacterBase
    {
        [SerializeField] private Transform _root;

        protected override void Init()
        {
            base.Init();

            characterController
                .OnTriggerEnterAsObservable()
                .Where(c => c.gameObject.layer == Layers.Item)
                .Subscribe(col =>
                {
                    Item item = world.ItemsColliders
                        .FirstOrDefault(i => i.gameObject.Equals(col.gameObject));
                    
                    if (!item) return;

                    item.onPick.Execute(_root);
                })
                .AddTo(lifetimeDisposable);
            
            characterController
                .OnTriggerEnterAsObservable()
                .Where(c => c.gameObject.layer == Layers.Collector)
                .Subscribe(col =>
                {
                    Collector collector = world.CollectorsColliders
                        .FirstOrDefault(c => c.gameObject.Equals(col.gameObject));
                    
                    if (!collector) return;

                    collector.disableNeighbors.Execute();

                    int count = world.CharacterItems.Count;
                    
                    if (count == 0) return;

                    int current = collector.Steps.Length - collector.index.Value;

                    if (count > current)
                    {
                        count = current;
                    }

                    for (int i = 0; i < count; i++)
                    {
                        Item item = world.CharacterItems.GetLast();

                        item.onMove.Execute(collector);
                    }
                })
                .AddTo(lifetimeDisposable);

            characterController
                .OnTriggerEnterAsObservable()
                .Where(c => c.gameObject.layer == Layers.Trap)
                .First()
                .Subscribe(col =>
                {
                    game.OnRoundEnd.Execute(false);
                })
                .AddTo(lifetimeDisposable);
        }

        protected override void Enable()
        {
            base.Enable();

            characterController = GetComponent<CharacterController>();
        }
    }
}